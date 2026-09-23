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
    public static class CustomerInquiryEndpoints
    {
        private static readonly string[] ValidStatuses = { "Open", "InProgress", "Resolved", "Closed" };
        private static readonly string[] ValidPriorities = { "Low", "Normal", "High", "Urgent" };

        public static void MapCustomerInquiryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/customer-inquiries");

            // ==================== LIST ====================
            group.MapGet("", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                int page = 1,
                int pageSize = 100,
                string? status = null,
                string? priority = null) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.CustomerInquiries.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(status) && status != "All")
                    query = query.Where(i => i.Status == status);

                if (!string.IsNullOrWhiteSpace(priority) && priority != "All")
                    query = query.Where(i => i.Priority == priority);

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(i => i.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(i => new CustomerInquiryResponseDto
                {
                    CustomerInquiryId = i.CustomerInquiryId,
                    CustomerId = i.CustomerId,
                    Subject = i.Subject,
                    Message = i.Message,
                    Status = i.Status,
                    Priority = i.Priority,
                    Response = i.Response,
                    RespondedAt = i.RespondedAt,
                    RespondedBy = i.RespondedBy,
                    CreatedAt = i.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<CustomerInquiryResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // ==================== LIST BY CUSTOMER (for future client view) ====================
            group.MapGet("/customer/{customerId:int}", async (
                int companyId,
                int customerId,
                ITenantDbContextFactory tenantFactory,
                int page = 1,
                int pageSize = 50) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.CustomerInquiries.AsNoTracking().Where(i => i.CustomerId == customerId);

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(i => i.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(i => new CustomerInquiryResponseDto
                {
                    CustomerInquiryId = i.CustomerInquiryId,
                    CustomerId = i.CustomerId,
                    Subject = i.Subject,
                    Message = i.Message,
                    Status = i.Status,
                    Priority = i.Priority,
                    Response = i.Response,
                    RespondedAt = i.RespondedAt,
                    RespondedBy = i.RespondedBy,
                    CreatedAt = i.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<CustomerInquiryResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // ==================== GET ONE ====================
            group.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var i = await db.CustomerInquiries.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CustomerInquiryId == id);

                if (i == null) return Results.NotFound();

                return Results.Ok(new CustomerInquiryResponseDto
                {
                    CustomerInquiryId = i.CustomerInquiryId,
                    CustomerId = i.CustomerId,
                    Subject = i.Subject,
                    Message = i.Message,
                    Status = i.Status,
                    Priority = i.Priority,
                    Response = i.Response,
                    RespondedAt = i.RespondedAt,
                    RespondedBy = i.RespondedBy,
                    CreatedAt = i.CreatedAt
                });
            });

            // ==================== CREATE ====================
            group.MapPost("", async (
                int companyId,
                CustomerInquiryCreateDto createDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var ctx = new ValidationContext(createDto);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createDto, ctx, results, true))
                    return Results.BadRequest(results.Select(r => r.ErrorMessage));

                if (!ValidPriorities.Contains(createDto.Priority))
                    return Results.BadRequest($"Priority must be one of: {string.Join(", ", ValidPriorities)}");

                await using var db = await tenantFactory.CreateAsync(companyId);

                var customerExists = await db.Customers.AnyAsync(c => c.CustomerId == createDto.CustomerId);
                if (!customerExists)
                    return Results.BadRequest("Customer not found");

                var inquiry = new CustomerInquiry
                {
                    CustomerId = createDto.CustomerId,
                    Subject = createDto.Subject,
                    Message = createDto.Message,
                    Priority = createDto.Priority,
                    Status = "Open",
                    CreatedAt = DateTime.UtcNow
                };

                db.CustomerInquiries.Add(inquiry);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "CustomerInquiry",
                    entityId: inquiry.CustomerInquiryId,
                    newValue: $"CustomerId={inquiry.CustomerId}, Subject={inquiry.Subject}, Priority={inquiry.Priority}");

                return Results.Created($"/tenant/{companyId}/customer-inquiries/{inquiry.CustomerInquiryId}",
                    new CustomerInquiryResponseDto
                    {
                        CustomerInquiryId = inquiry.CustomerInquiryId,
                        CustomerId = inquiry.CustomerId,
                        Subject = inquiry.Subject,
                        Message = inquiry.Message,
                        Status = inquiry.Status,
                        Priority = inquiry.Priority,
                        Response = inquiry.Response,
                        RespondedAt = inquiry.RespondedAt,
                        RespondedBy = inquiry.RespondedBy,
                        CreatedAt = inquiry.CreatedAt
                    });
            });

            // ==================== UPDATE ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                CustomerInquiryUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                if (!string.IsNullOrWhiteSpace(updateDto.Priority) && !ValidPriorities.Contains(updateDto.Priority))
                    return Results.BadRequest($"Priority must be one of: {string.Join(", ", ValidPriorities)}");

                await using var db = await tenantFactory.CreateAsync(companyId);
                var inquiry = await db.CustomerInquiries.FirstOrDefaultAsync(i => i.CustomerInquiryId == id);
                if (inquiry == null) return Results.NotFound();

                var oldSnapshot = $"Subject={inquiry.Subject}, Priority={inquiry.Priority}";

                if (!string.IsNullOrWhiteSpace(updateDto.Subject))
                    inquiry.Subject = updateDto.Subject;
                if (!string.IsNullOrWhiteSpace(updateDto.Message))
                    inquiry.Message = updateDto.Message;
                if (!string.IsNullOrWhiteSpace(updateDto.Priority))
                    inquiry.Priority = updateDto.Priority;

                db.CustomerInquiries.Update(inquiry);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Update",
                    entityName: "CustomerInquiry",
                    entityId: inquiry.CustomerInquiryId,
                    oldValue: oldSnapshot,
                    newValue: $"Subject={inquiry.Subject}, Priority={inquiry.Priority}");

                return Results.Ok(new CustomerInquiryResponseDto
                {
                    CustomerInquiryId = inquiry.CustomerInquiryId,
                    CustomerId = inquiry.CustomerId,
                    Subject = inquiry.Subject,
                    Message = inquiry.Message,
                    Status = inquiry.Status,
                    Priority = inquiry.Priority,
                    Response = inquiry.Response,
                    RespondedAt = inquiry.RespondedAt,
                    RespondedBy = inquiry.RespondedBy,
                    CreatedAt = inquiry.CreatedAt
                });
            });

            // ==================== RESPOND ====================
            group.MapPost("/{id:int}/respond", async (
                int companyId,
                int id,
                CustomerInquiryRespondDto respondDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                if (string.IsNullOrWhiteSpace(respondDto.Response))
                    return Results.BadRequest("Response cannot be empty");

                var newStatus = string.IsNullOrWhiteSpace(respondDto.Status) ? "Resolved" : respondDto.Status!;
                if (!ValidStatuses.Contains(newStatus))
                    return Results.BadRequest($"Status must be one of: {string.Join(", ", ValidStatuses)}");

                await using var db = await tenantFactory.CreateAsync(companyId);
                var inquiry = await db.CustomerInquiries.FirstOrDefaultAsync(i => i.CustomerInquiryId == id);
                if (inquiry == null) return Results.NotFound();

                var respondedBy = user.FindFirst(ClaimTypes.Email)?.Value
                               ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                               ?? "unknown";

                inquiry.Response = respondDto.Response.Trim();
                inquiry.RespondedAt = DateTime.UtcNow;
                inquiry.RespondedBy = respondedBy;
                inquiry.Status = newStatus;

                db.CustomerInquiries.Update(inquiry);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Respond",
                    entityName: "CustomerInquiry",
                    entityId: inquiry.CustomerInquiryId,
                    newValue: $"Status={inquiry.Status}, RespondedBy={respondedBy}");

                return Results.Ok(new CustomerInquiryResponseDto
                {
                    CustomerInquiryId = inquiry.CustomerInquiryId,
                    CustomerId = inquiry.CustomerId,
                    Subject = inquiry.Subject,
                    Message = inquiry.Message,
                    Status = inquiry.Status,
                    Priority = inquiry.Priority,
                    Response = inquiry.Response,
                    RespondedAt = inquiry.RespondedAt,
                    RespondedBy = inquiry.RespondedBy,
                    CreatedAt = inquiry.CreatedAt
                });
            });

            // ==================== STATUS CHANGE ====================
            group.MapPost("/{id:int}/status", async (
                int companyId,
                int id,
                CustomerInquiryStatusDto statusDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                if (!ValidStatuses.Contains(statusDto.Status))
                    return Results.BadRequest($"Status must be one of: {string.Join(", ", ValidStatuses)}");

                await using var db = await tenantFactory.CreateAsync(companyId);
                var inquiry = await db.CustomerInquiries.FirstOrDefaultAsync(i => i.CustomerInquiryId == id);
                if (inquiry == null) return Results.NotFound();

                var oldStatus = inquiry.Status;
                inquiry.Status = statusDto.Status;

                db.CustomerInquiries.Update(inquiry);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: $"StatusChange:{oldStatus}->{inquiry.Status}",
                    entityName: "CustomerInquiry",
                    entityId: inquiry.CustomerInquiryId,
                    oldValue: $"Status={oldStatus}",
                    newValue: $"Status={inquiry.Status}");

                return Results.Ok(new { inquiry.CustomerInquiryId, inquiry.Status });
            });

            // ==================== DELETE ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var inquiry = await db.CustomerInquiries.FirstOrDefaultAsync(i => i.CustomerInquiryId == id);
                if (inquiry == null) return Results.NotFound();

                var oldSnapshot = $"CustomerId={inquiry.CustomerId}, Subject={inquiry.Subject}, Status={inquiry.Status}";

                db.CustomerInquiries.Remove(inquiry);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "CustomerInquiry",
                    entityId: id,
                    oldValue: oldSnapshot);

                return Results.NoContent();
            });
        }
    }
}