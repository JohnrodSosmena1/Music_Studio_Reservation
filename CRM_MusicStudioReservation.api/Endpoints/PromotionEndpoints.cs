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
    public static class PromotionEndpoints
    {
        public static void MapPromotionEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/promotions");

            // GET /promotions - List all active promotions
            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20, string? search = null) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.Promotions.AsNoTracking().Where(p => p.IsActive);

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(p => p.PromotionCode.Contains(search) || p.PromotionName.Contains(search));

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(p => p.StartDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(p => new PromotionResponseDto
                {
                    PromotionId = p.PromotionId,
                    PromotionCode = p.PromotionCode,
                    PromotionName = p.PromotionName,
                    Description = p.Description,
                    DiscountPercent = p.DiscountPercent,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<PromotionResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // GET /promotions/{id}
            group.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var promotion = await db.Promotions.AsNoTracking().FirstOrDefaultAsync(p => p.PromotionId == id);

                if (promotion == null)
                    return Results.NotFound();

                return Results.Ok(new PromotionResponseDto
                {
                    PromotionId = promotion.PromotionId,
                    PromotionCode = promotion.PromotionCode,
                    PromotionName = promotion.PromotionName,
                    Description = promotion.Description,
                    DiscountPercent = promotion.DiscountPercent,
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate,
                    IsActive = promotion.IsActive,
                    CreatedAt = promotion.CreatedAt
                });
            });

            // ==================== POST /promotions ====================
            group.MapPost("", async (
                int companyId,
                PromotionCreateDto createDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var context = new ValidationContext(createDto);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createDto, context, results, true))
                    return Results.BadRequest(results.Select(r => r.ErrorMessage));

                await using var db = await tenantFactory.CreateAsync(companyId);

                var promotion = new Promotion
                {
                    PromotionCode = createDto.PromotionCode,
                    PromotionName = createDto.PromotionName,
                    Description = createDto.Description,
                    DiscountPercent = createDto.DiscountPercent,
                    StartDate = createDto.StartDate,
                    EndDate = createDto.EndDate,
                    IsActive = true
                };

                db.Promotions.Add(promotion);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "Promotion",
                    entityId: promotion.PromotionId,
                    newValue: $"Code={promotion.PromotionCode}, Name={promotion.PromotionName}, Discount={promotion.DiscountPercent}%, Start={promotion.StartDate:yyyy-MM-dd}, End={promotion.EndDate:yyyy-MM-dd}");

                return Results.Created($"/tenant/{companyId}/promotions/{promotion.PromotionId}",
                    new PromotionResponseDto
                    {
                        PromotionId = promotion.PromotionId,
                        PromotionCode = promotion.PromotionCode,
                        PromotionName = promotion.PromotionName,
                        Description = promotion.Description,
                        DiscountPercent = promotion.DiscountPercent,
                        StartDate = promotion.StartDate,
                        EndDate = promotion.EndDate,
                        IsActive = promotion.IsActive,
                        CreatedAt = promotion.CreatedAt
                    });
            });

            // ==================== PUT /promotions/{id} ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                PromotionUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var promotion = await db.Promotions.FirstOrDefaultAsync(p => p.PromotionId == id);

                if (promotion == null)
                    return Results.NotFound();

                // Capture snapshot BEFORE changes
                var oldSnapshot = $"Code={promotion.PromotionCode}, Name={promotion.PromotionName}, Discount={promotion.DiscountPercent}%, End={promotion.EndDate:yyyy-MM-dd}, Active={promotion.IsActive}";
                var oldIsActive = promotion.IsActive;

                if (!string.IsNullOrWhiteSpace(updateDto.PromotionName))
                    promotion.PromotionName = updateDto.PromotionName;
                if (updateDto.Description != null)
                    promotion.Description = updateDto.Description;
                if (updateDto.DiscountPercent.HasValue)
                    promotion.DiscountPercent = updateDto.DiscountPercent.Value;
                if (updateDto.EndDate.HasValue)
                    promotion.EndDate = updateDto.EndDate.Value;
                if (updateDto.IsActive.HasValue)
                    promotion.IsActive = updateDto.IsActive.Value;

                db.Promotions.Update(promotion);
                await db.SaveChangesAsync();

                // 👇 Determine action type (activate/deactivate vs generic update)
                var action = "Update";
                if (updateDto.IsActive.HasValue && updateDto.IsActive.Value != oldIsActive)
                    action = updateDto.IsActive.Value ? "Activate" : "Deactivate";

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: action,
                    entityName: "Promotion",
                    entityId: promotion.PromotionId,
                    oldValue: oldSnapshot,
                    newValue: $"Code={promotion.PromotionCode}, Name={promotion.PromotionName}, Discount={promotion.DiscountPercent}%, End={promotion.EndDate:yyyy-MM-dd}, Active={promotion.IsActive}");

                return Results.Ok(new PromotionResponseDto
                {
                    PromotionId = promotion.PromotionId,
                    PromotionCode = promotion.PromotionCode,
                    PromotionName = promotion.PromotionName,
                    Description = promotion.Description,
                    DiscountPercent = promotion.DiscountPercent,
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate,
                    IsActive = promotion.IsActive,
                    CreatedAt = promotion.CreatedAt
                });
            });

            // ==================== DELETE /promotions/{id} (soft delete) ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var promotion = await db.Promotions.FirstOrDefaultAsync(p => p.PromotionId == id);

                if (promotion == null)
                    return Results.NotFound();

                var oldSnapshot = $"Code={promotion.PromotionCode}, Name={promotion.PromotionName}, Active={promotion.IsActive}";

                promotion.IsActive = false;
                db.Promotions.Update(promotion);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "Promotion",
                    entityId: promotion.PromotionId,
                    oldValue: oldSnapshot,
                    newValue: "IsActive=false");

                return Results.NoContent();
            });

            // GET /promotions/validate/{code} - Validate promotion code
            group.MapGet("/validate/{code}", async (int companyId, string code, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var promotion = await db.Promotions.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PromotionCode == code && p.IsActive);

                if (promotion == null)
                    return Results.NotFound(new { message = "Promotion code not found or inactive" });

                if (DateTime.UtcNow > promotion.EndDate)
                    return Results.BadRequest(new { message = "Promotion has expired" });

                return Results.Ok(new
                {
                    isValid = true,
                    promotion.DiscountPercent
                });
            });
        }
    }
}