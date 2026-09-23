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
    public static class MembershipEndpoints
    {
        public static void MapMembershipEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/memberships");

            // ==================== LIST ====================
            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.Memberships.AsNoTracking();

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(m => m.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(m => new MembershipResponseDto
                {
                    MembershipId = m.MembershipId,
                    CustomerId = m.CustomerId,
                    MembershipPlanId = m.MembershipPlanId,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    MembershipStatus = m.MembershipStatus,
                    LoyaltyPoints = m.LoyaltyPoints,
                    CreatedAt = m.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<MembershipResponseDto>
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
                var membership = await db.Memberships.AsNoTracking().FirstOrDefaultAsync(m => m.MembershipId == id);

                if (membership == null)
                    return Results.NotFound();

                return Results.Ok(new MembershipResponseDto
                {
                    MembershipId = membership.MembershipId,
                    CustomerId = membership.CustomerId,
                    MembershipPlanId = membership.MembershipPlanId,
                    StartDate = membership.StartDate,
                    EndDate = membership.EndDate,
                    MembershipStatus = membership.MembershipStatus,
                    LoyaltyPoints = membership.LoyaltyPoints,
                    CreatedAt = membership.CreatedAt
                });
            });

            // ==================== CREATE ====================
            group.MapPost("", async (
                int companyId,
                MembershipCreateDto createDto,
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

                var customerExists = await db.Customers.AnyAsync(c => c.CustomerId == createDto.CustomerId);
                var planExists = await db.MembershipPlans.AnyAsync(p => p.MembershipPlanId == createDto.MembershipPlanId);

                if (!customerExists || !planExists)
                    return Results.BadRequest("Customer or membership plan not found");

                var membership = new Membership
                {
                    CustomerId = createDto.CustomerId,
                    MembershipPlanId = createDto.MembershipPlanId,
                    StartDate = createDto.StartDate,
                    EndDate = createDto.EndDate,
                    MembershipStatus = CRM_MusicStudioReservation.domain.enums.MembershipStatus.Active,
                    LoyaltyPoints = 0
                };

                db.Memberships.Add(membership);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "Membership",
                    entityId: membership.MembershipId,
                    newValue: $"CustomerId={membership.CustomerId}, PlanId={membership.MembershipPlanId}, Start={membership.StartDate:yyyy-MM-dd}, End={(membership.EndDate.HasValue ? membership.EndDate.Value.ToString("yyyy-MM-dd") : "—")}");

                return Results.Created($"/tenant/{companyId}/memberships/{membership.MembershipId}",
                    new MembershipResponseDto
                    {
                        MembershipId = membership.MembershipId,
                        CustomerId = membership.CustomerId,
                        MembershipPlanId = membership.MembershipPlanId,
                        StartDate = membership.StartDate,
                        EndDate = membership.EndDate,
                        MembershipStatus = membership.MembershipStatus,
                        LoyaltyPoints = membership.LoyaltyPoints,
                        CreatedAt = membership.CreatedAt
                    });
            });

            // ==================== UPDATE ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                MembershipUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var membership = await db.Memberships.FirstOrDefaultAsync(m => m.MembershipId == id);

                if (membership == null)
                    return Results.NotFound();

                var oldSnapshot = $"Status={membership.MembershipStatus}, End={(membership.EndDate.HasValue ? membership.EndDate.Value.ToString("yyyy-MM-dd") : "—")}, Points={membership.LoyaltyPoints}";
                var oldStatus = membership.MembershipStatus;

                if (updateDto.EndDate.HasValue)
                    membership.EndDate = updateDto.EndDate.Value;
                if (updateDto.MembershipStatus.HasValue)
                    membership.MembershipStatus = updateDto.MembershipStatus.Value;
                if (updateDto.LoyaltyPoints.HasValue)
                    membership.LoyaltyPoints = updateDto.LoyaltyPoints.Value;

                db.Memberships.Update(membership);
                await db.SaveChangesAsync();

                // 👇 Determine action type
                var action = "Update";
                if (updateDto.MembershipStatus.HasValue && updateDto.MembershipStatus.Value != oldStatus)
                    action = $"StatusChange:{oldStatus}->{updateDto.MembershipStatus.Value}";

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: action,
                    entityName: "Membership",
                    entityId: membership.MembershipId,
                    oldValue: oldSnapshot,
                    newValue: $"Status={membership.MembershipStatus}, End={(membership.EndDate.HasValue ? membership.EndDate.Value.ToString("yyyy-MM-dd") : "—")}, Points={membership.LoyaltyPoints}");

                return Results.Ok(new MembershipResponseDto
                {
                    MembershipId = membership.MembershipId,
                    CustomerId = membership.CustomerId,
                    MembershipPlanId = membership.MembershipPlanId,
                    StartDate = membership.StartDate,
                    EndDate = membership.EndDate,
                    MembershipStatus = membership.MembershipStatus,
                    LoyaltyPoints = membership.LoyaltyPoints,
                    CreatedAt = membership.CreatedAt
                });
            });

            // ==================== DELETE (soft cancel) ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var membership = await db.Memberships.FirstOrDefaultAsync(m => m.MembershipId == id);

                if (membership == null)
                    return Results.NotFound();

                var oldStatus = membership.MembershipStatus;

                membership.MembershipStatus = CRM_MusicStudioReservation.domain.enums.MembershipStatus.Cancelled;
                db.Memberships.Update(membership);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Cancel",
                    entityName: "Membership",
                    entityId: membership.MembershipId,
                    oldValue: $"Status={oldStatus}",
                    newValue: "Status=Cancelled");

                return Results.NoContent();
            });
        }
    }
}