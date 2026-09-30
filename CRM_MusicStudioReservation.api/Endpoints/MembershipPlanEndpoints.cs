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
    public static class MembershipPlanEndpoints
    {
        public static void MapMembershipPlanEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/membership-plans").RequireAuthorization();

            // GET /membership-plans
            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20, string? search = null) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.MembershipPlans.AsNoTracking().Where(p => p.IsActive);

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(p => p.PlanName.Contains(search));

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderBy(p => p.MembershipPlanId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(p => new MembershipPlanResponseDto
                {
                    MembershipPlanId = p.MembershipPlanId,
                    PlanName = p.PlanName,
                    Description = p.Description,
                    MonthlyFee = p.MonthlyFee,
                    LoyaltyPointsPerBooking = p.LoyaltyPointsPerBooking,
                    Benefits = p.Benefits,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<MembershipPlanResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // GET /membership-plans/{id}
            group.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var plan = await db.MembershipPlans.AsNoTracking().FirstOrDefaultAsync(p => p.MembershipPlanId == id);

                if (plan == null)
                    return Results.NotFound();

                return Results.Ok(new MembershipPlanResponseDto
                {
                    MembershipPlanId = plan.MembershipPlanId,
                    PlanName = plan.PlanName,
                    Description = plan.Description,
                    MonthlyFee = plan.MonthlyFee,
                    LoyaltyPointsPerBooking = plan.LoyaltyPointsPerBooking,
                    Benefits = plan.Benefits,
                    IsActive = plan.IsActive,
                    CreatedAt = plan.CreatedAt
                });
            });

            // ==================== POST /membership-plans ====================
            group.MapPost("", async (
                int companyId,
                MembershipPlanCreateDto createDto,
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

                var plan = new MembershipPlan
                {
                    PlanName = createDto.PlanName,
                    Description = createDto.Description,
                    MonthlyFee = createDto.MonthlyFee,
                    LoyaltyPointsPerBooking = createDto.LoyaltyPointsPerBooking,
                    Benefits = createDto.Benefits,
                    IsActive = true
                };

                db.MembershipPlans.Add(plan);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "MembershipPlan",
                    entityId: plan.MembershipPlanId,
                    newValue: $"Name={plan.PlanName}, Fee=₱{plan.MonthlyFee:N2}, Points/Booking={plan.LoyaltyPointsPerBooking}");

                return Results.Created($"/tenant/{companyId}/membership-plans/{plan.MembershipPlanId}",
                    new MembershipPlanResponseDto
                    {
                        MembershipPlanId = plan.MembershipPlanId,
                        PlanName = plan.PlanName,
                        Description = plan.Description,
                        MonthlyFee = plan.MonthlyFee,
                        LoyaltyPointsPerBooking = plan.LoyaltyPointsPerBooking,
                        Benefits = plan.Benefits,
                        IsActive = plan.IsActive,
                        CreatedAt = plan.CreatedAt
                    });
            });

            // ==================== PUT /membership-plans/{id} ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                MembershipPlanUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var plan = await db.MembershipPlans.FirstOrDefaultAsync(p => p.MembershipPlanId == id);

                if (plan == null)
                    return Results.NotFound();

                var oldSnapshot = $"Name={plan.PlanName}, Fee=₱{plan.MonthlyFee:N2}, Points/Booking={plan.LoyaltyPointsPerBooking}, Active={plan.IsActive}";
                var oldIsActive = plan.IsActive;

                if (!string.IsNullOrWhiteSpace(updateDto.PlanName))
                    plan.PlanName = updateDto.PlanName;
                if (updateDto.Description != null)
                    plan.Description = updateDto.Description;
                if (updateDto.MonthlyFee.HasValue)
                    plan.MonthlyFee = updateDto.MonthlyFee.Value;
                if (updateDto.LoyaltyPointsPerBooking.HasValue)
                    plan.LoyaltyPointsPerBooking = updateDto.LoyaltyPointsPerBooking.Value;
                if (updateDto.Benefits != null)
                    plan.Benefits = updateDto.Benefits;
                if (updateDto.IsActive.HasValue)
                    plan.IsActive = updateDto.IsActive.Value;

                db.MembershipPlans.Update(plan);
                await db.SaveChangesAsync();

                // 👇 Determine action type
                var action = "Update";
                if (updateDto.IsActive.HasValue && updateDto.IsActive.Value != oldIsActive)
                    action = updateDto.IsActive.Value ? "Activate" : "Deactivate";

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: action,
                    entityName: "MembershipPlan",
                    entityId: plan.MembershipPlanId,
                    oldValue: oldSnapshot,
                    newValue: $"Name={plan.PlanName}, Fee=₱{plan.MonthlyFee:N2}, Points/Booking={plan.LoyaltyPointsPerBooking}, Active={plan.IsActive}");

                return Results.Ok(new MembershipPlanResponseDto
                {
                    MembershipPlanId = plan.MembershipPlanId,
                    PlanName = plan.PlanName,
                    Description = plan.Description,
                    MonthlyFee = plan.MonthlyFee,
                    LoyaltyPointsPerBooking = plan.LoyaltyPointsPerBooking,
                    Benefits = plan.Benefits,
                    IsActive = plan.IsActive,
                    CreatedAt = plan.CreatedAt
                });
            });

            // ==================== DELETE /membership-plans/{id} ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var plan = await db.MembershipPlans.FirstOrDefaultAsync(p => p.MembershipPlanId == id);

                if (plan == null)
                    return Results.NotFound();

                var oldSnapshot = $"Name={plan.PlanName}, Fee=₱{plan.MonthlyFee:N2}, Active={plan.IsActive}";

                plan.IsActive = false;
                db.MembershipPlans.Update(plan);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "MembershipPlan",
                    entityId: plan.MembershipPlanId,
                    oldValue: oldSnapshot,
                    newValue: "IsActive=false");

                return Results.NoContent();
            });
        }
    }
}