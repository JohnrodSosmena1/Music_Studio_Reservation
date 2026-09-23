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
    public static class StudioEndpoints
    {
        public static void MapStudioEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/studios");

            // ==================== GET ALL ====================
            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory,
                int page = 1, int pageSize = 20, string? search = null, bool includeInactive = false) =>
            {
                await using var tenantDb = await tenantFactory.CreateAsync(companyId);
                var query = tenantDb.Studios.AsNoTracking();

                if (!includeInactive) query = query.Where(s => s.IsActive);

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(s => s.StudioName.Contains(search) || s.StudioCode.Contains(search));

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderBy(s => s.StudioId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(s => new StudioResponseDto
                {
                    StudioId = s.StudioId,
                    StudioCode = s.StudioCode,
                    StudioName = s.StudioName,
                    StudioType = s.StudioType,
                    HourlyRate = s.HourlyRate,
                    Capacity = s.Capacity,
                    Description = s.Description,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<StudioResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // ==================== GET BY ID ====================
            group.MapGet("/{id:int}", async (int companyId, ITenantDbContextFactory tenantFactory, int id) =>
            {
                await using var tenantDb = await tenantFactory.CreateAsync(companyId);
                var s = await tenantDb.Studios.AsNoTracking().FirstOrDefaultAsync(x => x.StudioId == id);
                if (s == null) return Results.NotFound();

                return Results.Ok(new StudioResponseDto
                {
                    StudioId = s.StudioId,
                    StudioCode = s.StudioCode,
                    StudioName = s.StudioName,
                    StudioType = s.StudioType,
                    HourlyRate = s.HourlyRate,
                    Capacity = s.Capacity,
                    Description = s.Description,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt
                });
            });

            // ==================== CREATE ====================
            group.MapPost("", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                StudioCreateDto createDto,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validationResults = ValidateDto(createDto);
                if (validationResults.Any()) return Results.ValidationProblem(CreateProblemDetails(validationResults));

                await using var tenantDb = await tenantFactory.CreateAsync(companyId);

                var studio = new Studio
                {
                    StudioCode = createDto.StudioCode,
                    StudioName = createDto.StudioName,
                    StudioType = createDto.StudioType,
                    HourlyRate = createDto.HourlyRate,
                    Capacity = createDto.Capacity,
                    Description = createDto.Description,
                    IsActive = true
                };

                tenantDb.Studios.Add(studio);
                await tenantDb.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "Studio",
                    entityId: studio.StudioId,
                    newValue: $"Code={studio.StudioCode}, Name={studio.StudioName}, Type={studio.StudioType}, Rate=₱{studio.HourlyRate:N2}, Capacity={studio.Capacity}");

                var response = new StudioResponseDto
                {
                    StudioId = studio.StudioId,
                    StudioCode = studio.StudioCode,
                    StudioName = studio.StudioName,
                    StudioType = studio.StudioType,
                    HourlyRate = studio.HourlyRate,
                    Capacity = studio.Capacity,
                    Description = studio.Description,
                    IsActive = studio.IsActive,
                    CreatedAt = studio.CreatedAt
                };

                return Results.Created($"/tenant/{companyId}/studios/{studio.StudioId}", response);
            });

            // ==================== UPDATE ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                int id,
                StudioUpdateDto updateDto,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validationResults = ValidateDto(updateDto);
                if (validationResults.Any()) return Results.ValidationProblem(CreateProblemDetails(validationResults));

                await using var tenantDb = await tenantFactory.CreateAsync(companyId);
                var studio = await tenantDb.Studios.FirstOrDefaultAsync(x => x.StudioId == id);
                if (studio == null) return Results.NotFound();

                var oldSnapshot = $"Code={studio.StudioCode}, Name={studio.StudioName}, Rate=₱{studio.HourlyRate:N2}, Capacity={studio.Capacity}, Active={studio.IsActive}";
                var oldIsActive = studio.IsActive;

                if (updateDto.StudioCode != null) studio.StudioCode = updateDto.StudioCode;
                if (updateDto.StudioName != null) studio.StudioName = updateDto.StudioName;
                if (updateDto.StudioType.HasValue) studio.StudioType = updateDto.StudioType.Value;
                if (updateDto.HourlyRate.HasValue) studio.HourlyRate = updateDto.HourlyRate.Value;
                if (updateDto.Capacity.HasValue) studio.Capacity = updateDto.Capacity.Value;
                if (updateDto.Description != null) studio.Description = updateDto.Description;
                if (updateDto.IsActive.HasValue) studio.IsActive = updateDto.IsActive.Value;

                await tenantDb.SaveChangesAsync();

                // 👇 Determine action type
                var action = "Update";
                if (updateDto.IsActive.HasValue && updateDto.IsActive.Value != oldIsActive)
                    action = updateDto.IsActive.Value ? "Activate" : "Deactivate";

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: action,
                    entityName: "Studio",
                    entityId: studio.StudioId,
                    oldValue: oldSnapshot,
                    newValue: $"Code={studio.StudioCode}, Name={studio.StudioName}, Rate=₱{studio.HourlyRate:N2}, Capacity={studio.Capacity}, Active={studio.IsActive}");

                var response = new StudioResponseDto
                {
                    StudioId = studio.StudioId,
                    StudioCode = studio.StudioCode,
                    StudioName = studio.StudioName,
                    StudioType = studio.StudioType,
                    HourlyRate = studio.HourlyRate,
                    Capacity = studio.Capacity,
                    Description = studio.Description,
                    IsActive = studio.IsActive,
                    CreatedAt = studio.CreatedAt
                };

                return Results.Ok(response);
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
                await using var tenantDb = await tenantFactory.CreateAsync(companyId);
                var studio = await tenantDb.Studios.FirstOrDefaultAsync(x => x.StudioId == id);
                if (studio == null) return Results.NotFound();

                var oldSnapshot = $"Code={studio.StudioCode}, Name={studio.StudioName}, Active={studio.IsActive}";

                studio.IsActive = false;
                await tenantDb.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "Studio",
                    entityId: studio.StudioId,
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