using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.api.Helpers;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class TandCEndpoints
    {
        public static void MapTandCEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/terms");

            // ==================== GET "" — list all ====================
            group.MapGet("", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                string? type = null,
                string? status = null,
                string? search = null,
                int page = 1,
                int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.TermsAndConditions.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(type))
                    query = query.Where(t => t.TandCType == type);
                if (!string.IsNullOrWhiteSpace(status))
                    query = query.Where(t => t.Status == status);
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(t => t.Title.Contains(search) || t.TandCCode.Contains(search));

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(t => t.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(t => new TandCListItemDto
                    {
                        TandCId = t.TandCId,
                        TandCCode = t.TandCCode,
                        TandCType = t.TandCType,
                        Title = t.Title,
                        Version = t.Version,
                        Status = t.Status,
                        AuthorName = t.AuthorName,
                        CreatedAt = t.CreatedAt,
                        UpdatedAt = t.UpdatedAt
                    })
                    .ToListAsync();

                return Results.Ok(new PagingResponse<TandCListItemDto>
                {
                    Items = items,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            }).RequireAuthorization();

            // ==================== GET /{id} ====================
            group.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var tc = await db.TermsAndConditions.AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TandCId == id);

                if (tc == null)
                    return Results.NotFound();

                return Results.Ok(MapToResponse(tc));
            }).RequireAuthorization();

            // ==================== GET /published/{type} — public ====================
            group.MapGet("/published/{type}", async (int companyId, string type, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var tc = await db.TermsAndConditions.AsNoTracking()
                    .Where(t => t.TandCType == type && t.Status == "Published")
                    .OrderByDescending(t => t.MajorVersion)
                    .ThenByDescending(t => t.MinorVersion)
                    .FirstOrDefaultAsync();

                if (tc == null)
                    return Results.NotFound();

                return Results.Ok(MapToResponse(tc));
            });

            // ==================== POST "" — create draft ====================
            group.MapPost("", async (
                int companyId,
                TandCCreateDto createDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validationContext = new ValidationContext(createDto);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createDto, validationContext, validationResults, true))
                    return Results.BadRequest(validationResults.Select(r => r.ErrorMessage));

                await using var db = await tenantFactory.CreateAsync(companyId);

                // Determine next version
                var latest = await db.TermsAndConditions.AsNoTracking()
                    .Where(t => t.TandCType == createDto.TandCType)
                    .OrderByDescending(t => t.MajorVersion)
                    .ThenByDescending(t => t.MinorVersion)
                    .FirstOrDefaultAsync();

                int major = 1, minor = 0;
                if (latest != null)
                {
                    major = latest.MajorVersion;
                    minor = latest.MinorVersion + 1;
                    if (minor >= 10)
                    {
                        major += 1;
                        minor = 0;
                    }
                }

                var version = $"{major}.{minor}";

                // Determine code prefix
                var prefix = createDto.TandCType switch
                {
                    "GeneralTerms" => "GT",
                    "BookingPolicy" => "BP",
                    "CancellationPolicy" => "CP",
                    "PrivacyPolicy" => "PP",
                    "StudioUsageRules" => "SU",
                    _ => createDto.TandCType[..Math.Min(2, createDto.TandCType.Length)].ToUpper()
                };

                var code = $"{prefix}-v{version}";

                var authorName = user.FindFirst(ClaimTypes.Name)?.Value
                              ?? user.FindFirst(ClaimTypes.Email)?.Value
                              ?? "Unknown";
                var authorId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var tc = new TermsAndConditions
                {
                    TandCCode = code,
                    TandCType = createDto.TandCType,
                    Title = createDto.Title,
                    Content = createDto.Content,
                    Version = version,
                    MajorVersion = major,
                    MinorVersion = minor,
                    Status = "Draft",
                    AuthorUserId = authorId,
                    AuthorName = authorName,
                    RequiresReAcceptance = createDto.RequiresReAcceptance,
                    ChangeNotes = createDto.ChangeNotes,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                db.TermsAndConditions.Add(tc);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "TermsAndConditions",
                    entityId: tc.TandCId,
                    newValue: $"Code={tc.TandCCode}, Type={tc.TandCType}, Version={tc.Version}, Status=Draft");

                return Results.Created($"/tenant/{companyId}/terms/{tc.TandCId}", MapToResponse(tc));
            }).RequireAuthorization();

            // ==================== PUT /{id} — update draft ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                TandCUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var tc = await db.TermsAndConditions.FirstOrDefaultAsync(t => t.TandCId == id);

                if (tc == null)
                    return Results.NotFound();

                if (tc.Status != "Draft")
                    return Results.BadRequest(new { message = "Only Draft terms can be updated." });

                var oldSnapshot = $"Title={tc.Title}, Version={tc.Version}, Status={tc.Status}";

                if (!string.IsNullOrWhiteSpace(updateDto.Title))
                    tc.Title = updateDto.Title;
                if (updateDto.Content != null)
                    tc.Content = updateDto.Content;
                if (updateDto.RequiresReAcceptance.HasValue)
                    tc.RequiresReAcceptance = updateDto.RequiresReAcceptance.Value;
                if (updateDto.ChangeNotes != null)
                    tc.ChangeNotes = updateDto.ChangeNotes;

                tc.UpdatedAt = DateTime.UtcNow;

                db.TermsAndConditions.Update(tc);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Update",
                    entityName: "TermsAndConditions",
                    entityId: tc.TandCId,
                    oldValue: oldSnapshot,
                    newValue: $"Title={tc.Title}, Version={tc.Version}, Status={tc.Status}");

                return Results.Ok(MapToResponse(tc));
            }).RequireAuthorization();

            // ==================== POST /{id}/submit — Draft -> PendingApproval ====================
            group.MapPost("/{id:int}/submit", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var tc = await db.TermsAndConditions.FirstOrDefaultAsync(t => t.TandCId == id);

                if (tc == null)
                    return Results.NotFound();

                if (tc.Status != "Draft")
                    return Results.BadRequest(new { message = "Only Draft terms can be submitted for approval." });

                tc.Status = "PendingApproval";
                tc.UpdatedAt = DateTime.UtcNow;

                db.TermsAndConditions.Update(tc);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Submit",
                    entityName: "TermsAndConditions",
                    entityId: tc.TandCId,
                    newValue: $"Code={tc.TandCCode}, Status=PendingApproval");

                return Results.Ok(new { message = "Submitted for approval.", tc.TandCId });
            }).RequireAuthorization();

            // ==================== POST /{id}/publish — PendingApproval -> Published ====================
            group.MapPost("/{id:int}/publish", async (
                int companyId,
                int id,
                TandCPublishDto publishDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var tc = await db.TermsAndConditions.FirstOrDefaultAsync(t => t.TandCId == id);

                if (tc == null)
                    return Results.NotFound();

                if (tc.Status != "PendingApproval")
                    return Results.BadRequest(new { message = "Only PendingApproval terms can be published." });

                // Archive existing Published of same type
                var previousPublished = await db.TermsAndConditions
                    .Where(t => t.TandCType == tc.TandCType && t.Status == "Published" && t.TandCId != id)
                    .ToListAsync();

                foreach (var prev in previousPublished)
                {
                    prev.Status = "Archived";
                    prev.UpdatedAt = DateTime.UtcNow;
                    db.TermsAndConditions.Update(prev);
                }

                var approverName = user.FindFirst(ClaimTypes.Name)?.Value
                                ?? user.FindFirst(ClaimTypes.Email)?.Value
                                ?? "Unknown";
                var approverId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                tc.Status = "Published";
                tc.ApprovedByUserId = approverId;
                tc.ApprovedByName = approverName;
                tc.ApprovedAt = DateTime.UtcNow;
                tc.PublishedAt = DateTime.UtcNow;

                if (!string.IsNullOrWhiteSpace(publishDto?.ApproverNote))
                    tc.ChangeNotes = string.IsNullOrWhiteSpace(tc.ChangeNotes)
                        ? publishDto.ApproverNote
                        : $"{tc.ChangeNotes} | Approver: {publishDto.ApproverNote}";

                tc.UpdatedAt = DateTime.UtcNow;

                db.TermsAndConditions.Update(tc);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Publish",
                    entityName: "TermsAndConditions",
                    entityId: tc.TandCId,
                    newValue: $"Code={tc.TandCCode}, Version={tc.Version}, Status=Published, ApprovedBy={approverName}");

                return Results.Ok(new { message = "Terms published successfully.", tc.TandCId });
            }).RequireAuthorization();

            // ==================== POST /{id}/reject — PendingApproval -> Draft ====================
            group.MapPost("/{id:int}/reject", async (
                int companyId,
                int id,
                TandCRejectDto rejectDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validationContext = new ValidationContext(rejectDto);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(rejectDto, validationContext, validationResults, true))
                    return Results.BadRequest(validationResults.Select(r => r.ErrorMessage));

                await using var db = await tenantFactory.CreateAsync(companyId);
                var tc = await db.TermsAndConditions.FirstOrDefaultAsync(t => t.TandCId == id);

                if (tc == null)
                    return Results.NotFound();

                if (tc.Status != "PendingApproval")
                    return Results.BadRequest(new { message = "Only PendingApproval terms can be rejected." });

                tc.Status = "Draft";
                tc.ChangeNotes = string.IsNullOrWhiteSpace(tc.ChangeNotes)
                    ? $"Rejected: {rejectDto.Reason}"
                    : $"{tc.ChangeNotes} | Rejected: {rejectDto.Reason}";
                tc.UpdatedAt = DateTime.UtcNow;

                db.TermsAndConditions.Update(tc);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Reject",
                    entityName: "TermsAndConditions",
                    entityId: tc.TandCId,
                    newValue: $"Code={tc.TandCCode}, Status=Draft, Reason={rejectDto.Reason}");

                return Results.Ok(new { message = "Terms returned to Draft.", tc.TandCId });
            }).RequireAuthorization();

            // ==================== POST /{id}/archive ====================
            group.MapPost("/{id:int}/archive", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var tc = await db.TermsAndConditions.FirstOrDefaultAsync(t => t.TandCId == id);

                if (tc == null)
                    return Results.NotFound();

                var oldStatus = tc.Status;
                tc.Status = "Archived";
                tc.UpdatedAt = DateTime.UtcNow;

                db.TermsAndConditions.Update(tc);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Archive",
                    entityName: "TermsAndConditions",
                    entityId: tc.TandCId,
                    oldValue: $"Status={oldStatus}",
                    newValue: "Status=Archived");

                return Results.Ok(new { message = "Terms archived.", tc.TandCId });
            }).RequireAuthorization();

            // ==================== POST /acknowledge ====================
            group.MapPost("/acknowledge", async (
                int companyId,
                TandCAcknowledgeDto acknowledgeDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validationContext = new ValidationContext(acknowledgeDto);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(acknowledgeDto, validationContext, validationResults, true))
                    return Results.BadRequest(validationResults.Select(r => r.ErrorMessage));

                await using var db = await tenantFactory.CreateAsync(companyId);

                var tc = await db.TermsAndConditions.AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TandCId == acknowledgeDto.TandCId);

                if (tc == null)
                    return Results.NotFound(new { message = "Terms and Conditions not found." });

                var ip = httpContext.Connection.RemoteIpAddress?.ToString();
                var ua = httpContext.Request.Headers["User-Agent"].ToString();

                var ack = new UserTandCAcknowledgment
                {
                    TandCId = tc.TandCId,
                    CustomerId = acknowledgeDto.CustomerId,
                    TandCVersion = tc.Version,
                    TandCType = tc.TandCType,
                    AcknowledgmentContext = acknowledgeDto.Context,
                    AcknowledgedAt = DateTime.UtcNow,
                    IpAddress = ip,
                    UserAgent = string.IsNullOrWhiteSpace(ua) ? null : ua[..Math.Min(ua.Length, 500)]
                };

                db.UserTandCAcknowledgments.Add(ack);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Acknowledge",
                    entityName: "UserTandCAcknowledgment",
                    entityId: ack.AcknowledgmentId,
                    newValue: $"TandCId={tc.TandCId}, CustomerId={acknowledgeDto.CustomerId}, Version={tc.Version}, Context={acknowledgeDto.Context}");

                return Results.Created($"/tenant/{companyId}/terms/acknowledgments",
                    new TandCAcknowledgmentResponseDto
                    {
                        AcknowledgmentId = ack.AcknowledgmentId,
                        TandCId = tc.TandCId,
                        TandCCode = tc.TandCCode,
                        TandCVersion = tc.Version,
                        TandCType = tc.TandCType,
                        CustomerId = ack.CustomerId,
                        Context = ack.AcknowledgmentContext,
                        AcknowledgedAt = ack.AcknowledgedAt
                    });
            }).RequireAuthorization();

            // ==================== GET /acknowledgments ====================
            group.MapGet("/acknowledgments", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                int? customerId = null,
                int? tandCId = null,
                int page = 1,
                int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);

                var query = db.UserTandCAcknowledgments
                    .AsNoTracking()
                    .Include(a => a.TermsAndConditions);

                IQueryable<UserTandCAcknowledgment> filtered = query;

                if (customerId.HasValue)
                    filtered = filtered.Where(a => a.CustomerId == customerId.Value);
                if (tandCId.HasValue)
                    filtered = filtered.Where(a => a.TandCId == tandCId.Value);

                var totalItems = await filtered.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await filtered
                    .OrderByDescending(a => a.AcknowledgedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(a => new TandCAcknowledgmentResponseDto
                {
                    AcknowledgmentId = a.AcknowledgmentId,
                    TandCId = a.TandCId,
                    TandCCode = a.TermsAndConditions?.TandCCode ?? string.Empty,
                    TandCVersion = a.TandCVersion,
                    TandCType = a.TandCType,
                    CustomerId = a.CustomerId,
                    Context = a.AcknowledgmentContext,
                    AcknowledgedAt = a.AcknowledgedAt
                }).ToList();

                return Results.Ok(new PagingResponse<TandCAcknowledgmentResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            }).RequireAuthorization();
        }

        private static TandCResponseDto MapToResponse(TermsAndConditions tc) => new TandCResponseDto
        {
            TandCId = tc.TandCId,
            TandCCode = tc.TandCCode,
            TandCType = tc.TandCType,
            Title = tc.Title,
            Content = tc.Content,
            Version = tc.Version,
            Status = tc.Status,
            AuthorName = tc.AuthorName,
            ApprovedByName = tc.ApprovedByName,
            ApprovedAt = tc.ApprovedAt,
            PublishedAt = tc.PublishedAt,
            RequiresReAcceptance = tc.RequiresReAcceptance,
            ChangeNotes = tc.ChangeNotes,
            CreatedAt = tc.CreatedAt,
            UpdatedAt = tc.UpdatedAt
        };
    }
}
