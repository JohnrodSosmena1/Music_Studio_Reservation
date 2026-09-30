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
    public static class StudioServiceEndpoints
    {
        public static void MapStudioServiceEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/studio-services").RequireAuthorization();

            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20, string? search = null) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.StudioServices.AsNoTracking().Where(s => s.IsActive);

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(s => s.ServiceName.Contains(search) || s.ServiceCode.Contains(search));

                var items = await query.OrderBy(s => s.StudioServiceId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(s => new StudioServiceResponseDto
                {
                    StudioServiceId = s.StudioServiceId,
                    ServiceCode = s.ServiceCode,
                    ServiceName = s.ServiceName,
                    Price = s.Price,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt
                });

                return Results.Ok(results);
            });

            group.MapGet("/{id:int}", async (int companyId, ITenantDbContextFactory tenantFactory, int id) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var svc = await db.StudioServices.AsNoTracking().FirstOrDefaultAsync(s => s.StudioServiceId == id);
                if (svc == null) return Results.NotFound();

                return Results.Ok(new StudioServiceResponseDto
                {
                    StudioServiceId = svc.StudioServiceId,
                    ServiceCode = svc.ServiceCode,
                    ServiceName = svc.ServiceName,
                    Price = svc.Price,
                    IsActive = svc.IsActive,
                    CreatedAt = svc.CreatedAt
                });
            });

            // ==================== CREATE ====================
            group.MapPost("", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                StudioServiceCreateDto createDto,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validation = ValidateDto(createDto);
                if (validation.Any()) return Results.ValidationProblem(CreateProblemDetails(validation));

                await using var db = await tenantFactory.CreateAsync(companyId);

                var svc = new StudioService
                {
                    ServiceCode = createDto.ServiceCode,
                    ServiceName = createDto.ServiceName,
                    Price = createDto.Price,
                    IsActive = true
                };

                db.StudioServices.Add(svc);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "StudioService",
                    entityId: svc.StudioServiceId,
                    newValue: $"Code={svc.ServiceCode}, Name={svc.ServiceName}, Price=₱{svc.Price:N2}");

                var response = new StudioServiceResponseDto
                {
                    StudioServiceId = svc.StudioServiceId,
                    ServiceCode = svc.ServiceCode,
                    ServiceName = svc.ServiceName,
                    Price = svc.Price,
                    IsActive = svc.IsActive,
                    CreatedAt = svc.CreatedAt
                };

                return Results.Created($"/tenant/{companyId}/studio-services/{svc.StudioServiceId}", response);
            });

            // ==================== UPDATE ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                int id,
                StudioServiceUpdateDto updateDto,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validation = ValidateDto(updateDto);
                if (validation.Any()) return Results.ValidationProblem(CreateProblemDetails(validation));

                await using var db = await tenantFactory.CreateAsync(companyId);
                var svc = await db.StudioServices.FirstOrDefaultAsync(s => s.StudioServiceId == id);
                if (svc == null) return Results.NotFound();

                var oldSnapshot = $"Code={svc.ServiceCode}, Name={svc.ServiceName}, Price=₱{svc.Price:N2}, Active={svc.IsActive}";
                var oldIsActive = svc.IsActive;

                if (updateDto.ServiceCode != null) svc.ServiceCode = updateDto.ServiceCode;
                if (updateDto.ServiceName != null) svc.ServiceName = updateDto.ServiceName;
                if (updateDto.Price.HasValue) svc.Price = updateDto.Price.Value;
                if (updateDto.IsActive.HasValue) svc.IsActive = updateDto.IsActive.Value;

                await db.SaveChangesAsync();

                // 👇 Determine action type
                var action = "Update";
                if (updateDto.IsActive.HasValue && updateDto.IsActive.Value != oldIsActive)
                    action = updateDto.IsActive.Value ? "Activate" : "Deactivate";

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: action,
                    entityName: "StudioService",
                    entityId: svc.StudioServiceId,
                    oldValue: oldSnapshot,
                    newValue: $"Code={svc.ServiceCode}, Name={svc.ServiceName}, Price=₱{svc.Price:N2}, Active={svc.IsActive}");

                return Results.NoContent();
            });

            // ==================== DELETE ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                int id,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var svc = await db.StudioServices.FirstOrDefaultAsync(s => s.StudioServiceId == id);
                if (svc == null) return Results.NotFound();

                var oldSnapshot = $"Code={svc.ServiceCode}, Name={svc.ServiceName}, Active={svc.IsActive}";

                svc.IsActive = false;
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "StudioService",
                    entityId: svc.StudioServiceId,
                    oldValue: oldSnapshot,
                    newValue: "IsActive=false");

                return Results.NoContent();
            });
        }

        private static List<ValidationResult> ValidateDto(object dto)
        {
            var ctx = new ValidationContext(dto, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, ctx, results, validateAllProperties: true);
            return results;
        }

        private static IDictionary<string, string[]> CreateProblemDetails(List<ValidationResult> results)
        {
            var dict = new Dictionary<string, string[]>();
            foreach (var r in results)
            {
                var key = r.MemberNames.FirstOrDefault() ?? string.Empty;
                if (!dict.ContainsKey(key)) dict[key] = new[] { r.ErrorMessage ?? string.Empty };
                else dict[key] = dict[key].Concat(new[] { r.ErrorMessage ?? string.Empty }).ToArray();
            }
            return dict;
        }
    }
}