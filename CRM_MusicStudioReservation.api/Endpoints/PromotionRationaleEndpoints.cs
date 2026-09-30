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
    public static class PromotionRationaleEndpoints
    {
        public static void MapPromotionRationaleEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/promotions/{promotionId:int}/rationale");

            // ==================== GET "" — get rationale for a promotion ====================
            group.MapGet("", async (
                int companyId,
                int promotionId,
                ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var rationale = await db.PromotionRationales
                    .AsNoTracking()
                    .Include(r => r.Segments)
                    .FirstOrDefaultAsync(r => r.PromotionId == promotionId);

                if (rationale == null)
                    return Results.NotFound();

                return Results.Ok(MapToResponse(rationale));
            }).RequireAuthorization();

            // ==================== POST "" — create rationale ====================
            group.MapPost("", async (
                int companyId,
                int promotionId,
                PromotionRationaleCreateDto createDto,
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

                var promotion = await db.Promotions.FirstOrDefaultAsync(p => p.PromotionId == promotionId);
                if (promotion == null)
                    return Results.NotFound(new { message = "Promotion not found." });

                var existing = await db.PromotionRationales.AnyAsync(r => r.PromotionId == promotionId);
                if (existing)
                    return Results.Conflict(new { message = "A rationale already exists for this promotion." });

                var rationale = new PromotionRationale
                {
                    PromotionId = promotionId,
                    PurposeType = createDto.PurposeType,
                    TargetAudience = createDto.TargetAudience,
                    TriggerCondition = createDto.TriggerCondition,
                    ExpectedKpi = createDto.ExpectedKpi,
                    Budget = createDto.Budget,
                    MaxRedemptions = createDto.MaxRedemptions,
                    RationaleNotes = createDto.RationaleNotes,
                    WorkflowStatus = "Draft",
                    ActualRedemptions = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                foreach (var segName in createDto.Segments.Where(s => !string.IsNullOrWhiteSpace(s)))
                {
                    rationale.Segments.Add(new PromotionSegment { SegmentName = segName });
                }

                db.PromotionRationales.Add(rationale);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "PromotionRationale",
                    entityId: rationale.PromotionRationaleId,
                    newValue: $"PromotionId={promotionId}, PurposeType={rationale.PurposeType}, Status=Draft");

                // Reload with segments for response
                await db.Entry(rationale).Collection(r => r.Segments).LoadAsync();

                return Results.Created(
                    $"/tenant/{companyId}/promotions/{promotionId}/rationale",
                    MapToResponse(rationale));
            }).RequireAuthorization();

            // ==================== PUT /{id} — update (Draft only) ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int promotionId,
                int id,
                PromotionRationaleUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var rationale = await db.PromotionRationales
                    .Include(r => r.Segments)
                    .FirstOrDefaultAsync(r => r.PromotionRationaleId == id && r.PromotionId == promotionId);

                if (rationale == null)
                    return Results.NotFound();

                if (rationale.WorkflowStatus != "Draft")
                    return Results.BadRequest(new { message = "Only Draft rationales can be updated." });

                var oldSnapshot = $"PurposeType={rationale.PurposeType}, Status={rationale.WorkflowStatus}";

                if (!string.IsNullOrWhiteSpace(updateDto.PurposeType))
                    rationale.PurposeType = updateDto.PurposeType;
                if (updateDto.TargetAudience != null)
                    rationale.TargetAudience = updateDto.TargetAudience;
                if (updateDto.TriggerCondition != null)
                    rationale.TriggerCondition = updateDto.TriggerCondition;
                if (updateDto.ExpectedKpi != null)
                    rationale.ExpectedKpi = updateDto.ExpectedKpi;
                if (updateDto.Budget.HasValue)
                    rationale.Budget = updateDto.Budget;
                if (updateDto.MaxRedemptions.HasValue)
                    rationale.MaxRedemptions = updateDto.MaxRedemptions;
                if (updateDto.RationaleNotes != null)
                    rationale.RationaleNotes = updateDto.RationaleNotes;

                // Replace segments if provided
                if (updateDto.Segments != null)
                {
                    db.PromotionSegments.RemoveRange(rationale.Segments);
                    rationale.Segments.Clear();
                    foreach (var segName in updateDto.Segments.Where(s => !string.IsNullOrWhiteSpace(s)))
                    {
                        rationale.Segments.Add(new PromotionSegment { SegmentName = segName });
                    }
                }

                rationale.UpdatedAt = DateTime.UtcNow;
                db.PromotionRationales.Update(rationale);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Update",
                    entityName: "PromotionRationale",
                    entityId: rationale.PromotionRationaleId,
                    oldValue: oldSnapshot,
                    newValue: $"PurposeType={rationale.PurposeType}, Status={rationale.WorkflowStatus}");

                return Results.Ok(MapToResponse(rationale));
            }).RequireAuthorization();

            // ==================== POST /{id}/submit — Draft -> PendingApproval ====================
            group.MapPost("/{id:int}/submit", async (
                int companyId,
                int promotionId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var rationale = await db.PromotionRationales
                    .FirstOrDefaultAsync(r => r.PromotionRationaleId == id && r.PromotionId == promotionId);

                if (rationale == null)
                    return Results.NotFound();

                if (rationale.WorkflowStatus != "Draft")
                    return Results.BadRequest(new { message = "Only Draft rationales can be submitted." });

                rationale.WorkflowStatus = "PendingApproval";
                rationale.UpdatedAt = DateTime.UtcNow;

                db.PromotionRationales.Update(rationale);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Submit",
                    entityName: "PromotionRationale",
                    entityId: rationale.PromotionRationaleId,
                    newValue: $"PromotionId={promotionId}, Status=PendingApproval");

                return Results.Ok(new { message = "Rationale submitted for approval.", rationale.PromotionRationaleId });
            }).RequireAuthorization();

            // ==================== POST /{id}/approve — PendingApproval -> Active ====================
            group.MapPost("/{id:int}/approve", async (
                int companyId,
                int promotionId,
                int id,
                PromotionRationaleApproveDto approveDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var rationale = await db.PromotionRationales
                    .FirstOrDefaultAsync(r => r.PromotionRationaleId == id && r.PromotionId == promotionId);

                if (rationale == null)
                    return Results.NotFound();

                if (rationale.WorkflowStatus != "PendingApproval")
                    return Results.BadRequest(new { message = "Only PendingApproval rationales can be approved." });

                var approverName = user.FindFirst(ClaimTypes.Name)?.Value
                                ?? user.FindFirst(ClaimTypes.Email)?.Value
                                ?? "Unknown";
                var approverId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                rationale.WorkflowStatus = "Active";
                rationale.ApprovedByUserId = approverId;
                rationale.ApprovedByName = approverName;
                rationale.ApprovedAt = DateTime.UtcNow;
                rationale.UpdatedAt = DateTime.UtcNow;

                db.PromotionRationales.Update(rationale);

                // Also activate the promotion
                var promotion = await db.Promotions.FirstOrDefaultAsync(p => p.PromotionId == promotionId);
                if (promotion != null)
                {
                    promotion.IsActive = true;
                    db.Promotions.Update(promotion);
                }

                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Approve",
                    entityName: "PromotionRationale",
                    entityId: rationale.PromotionRationaleId,
                    newValue: $"PromotionId={promotionId}, Status=Active, ApprovedBy={approverName}");

                return Results.Ok(new { message = "Rationale approved. Promotion is now active.", rationale.PromotionRationaleId });
            }).RequireAuthorization();

            // ==================== POST /{id}/reject — PendingApproval -> Draft ====================
            group.MapPost("/{id:int}/reject", async (
                int companyId,
                int promotionId,
                int id,
                PromotionRationaleRejectDto rejectDto,
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
                var rationale = await db.PromotionRationales
                    .FirstOrDefaultAsync(r => r.PromotionRationaleId == id && r.PromotionId == promotionId);

                if (rationale == null)
                    return Results.NotFound();

                if (rationale.WorkflowStatus != "PendingApproval")
                    return Results.BadRequest(new { message = "Only PendingApproval rationales can be rejected." });

                rationale.WorkflowStatus = "Draft";
                rationale.RejectionReason = rejectDto.Reason;
                rationale.UpdatedAt = DateTime.UtcNow;

                db.PromotionRationales.Update(rationale);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Reject",
                    entityName: "PromotionRationale",
                    entityId: rationale.PromotionRationaleId,
                    newValue: $"PromotionId={promotionId}, Status=Draft, Reason={rejectDto.Reason}");

                return Results.Ok(new { message = "Rationale rejected, returned to Draft.", rationale.PromotionRationaleId });
            }).RequireAuthorization();

            // ==================== PUT /{id}/roi — update ROI (any status) ====================
            group.MapPut("/{id:int}/roi", async (
                int companyId,
                int promotionId,
                int id,
                PromotionRationaleRoiDto roiDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validationContext = new ValidationContext(roiDto);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(roiDto, validationContext, validationResults, true))
                    return Results.BadRequest(validationResults.Select(r => r.ErrorMessage));

                await using var db = await tenantFactory.CreateAsync(companyId);
                var rationale = await db.PromotionRationales
                    .FirstOrDefaultAsync(r => r.PromotionRationaleId == id && r.PromotionId == promotionId);

                if (rationale == null)
                    return Results.NotFound();

                var oldSnapshot = $"ActualRevenueDelta={rationale.ActualRevenueDelta}, RoiSummary={rationale.RoiSummary}";

                rationale.ActualRevenueDelta = roiDto.ActualRevenueDelta;
                rationale.RoiSummary = roiDto.RoiSummary;
                rationale.UpdatedAt = DateTime.UtcNow;

                db.PromotionRationales.Update(rationale);
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "UpdateRoi",
                    entityName: "PromotionRationale",
                    entityId: rationale.PromotionRationaleId,
                    oldValue: oldSnapshot,
                    newValue: $"ActualRevenueDelta={roiDto.ActualRevenueDelta}, RoiSummary={roiDto.RoiSummary}");

                return Results.Ok(new { message = "ROI updated.", rationale.PromotionRationaleId });
            }).RequireAuthorization();
        }

        private static PromotionRationaleResponseDto MapToResponse(PromotionRationale r) => new PromotionRationaleResponseDto
        {
            PromotionRationaleId = r.PromotionRationaleId,
            PromotionId = r.PromotionId,
            PurposeType = r.PurposeType,
            TargetAudience = r.TargetAudience,
            TriggerCondition = r.TriggerCondition,
            ExpectedKpi = r.ExpectedKpi,
            Budget = r.Budget,
            MaxRedemptions = r.MaxRedemptions,
            ActualRedemptions = r.ActualRedemptions,
            WorkflowStatus = r.WorkflowStatus,
            ApprovedByName = r.ApprovedByName,
            ApprovedAt = r.ApprovedAt,
            RejectionReason = r.RejectionReason,
            RationaleNotes = r.RationaleNotes,
            ActualRevenueDelta = r.ActualRevenueDelta,
            RoiSummary = r.RoiSummary,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt,
            Segments = r.Segments.Select(s => s.SegmentName).ToList()
        };
    }
}
