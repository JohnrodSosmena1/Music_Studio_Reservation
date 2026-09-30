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
    public static class InventoryEndpoints
    {
        public static void MapInventoryEndpoints(this WebApplication app)
        {
            var categoryGroup = app.MapGroup("/tenant/{companyId:int}/inventory-categories").RequireAuthorization();
            var itemGroup = app.MapGroup("/tenant/{companyId:int}/inventory-items").RequireAuthorization();

            // ==================== INVENTORY CATEGORIES ====================

            categoryGroup.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20, string? search = null) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.InventoryCategories.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(c => c.CategoryName.Contains(search));

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderBy(c => c.InventoryCategoryId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(c => new InventoryCategoryResponseDto
                {
                    InventoryCategoryId = c.InventoryCategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                }).ToList();

                return Results.Ok(new PagingResponse<InventoryCategoryResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            categoryGroup.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var category = await db.InventoryCategories.AsNoTracking().FirstOrDefaultAsync(c => c.InventoryCategoryId == id);
                if (category == null) return Results.NotFound();

                return Results.Ok(new InventoryCategoryResponseDto
                {
                    InventoryCategoryId = category.InventoryCategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                });
            });

            // ==================== CREATE CATEGORY ====================
            categoryGroup.MapPost("", async (
                int companyId,
                InventoryCategoryCreateDto createDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);

                var category = new InventoryCategory
                {
                    CategoryName = createDto.CategoryName,
                    Description = createDto.Description
                };

                db.InventoryCategories.Add(category);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "InventoryCategory",
                    entityId: category.InventoryCategoryId,
                    newValue: $"Name={category.CategoryName}, Description={category.Description ?? "(none)"}");

                return Results.Created($"/tenant/{companyId}/inventory-categories/{category.InventoryCategoryId}",
                    new InventoryCategoryResponseDto
                    {
                        InventoryCategoryId = category.InventoryCategoryId,
                        CategoryName = category.CategoryName,
                        Description = category.Description
                    });
            });

            // ==================== INVENTORY ITEMS ====================

            itemGroup.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 100, string? search = null) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.InventoryItems.AsNoTracking()
                    .Include(i => i.InventoryCategory)
                    .Include(i => i.Studio)
                    .Where(i => i.IsActive);

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(i => i.ItemName.Contains(search) || i.ItemCode.Contains(search) || (i.Location != null && i.Location.Contains(search)));

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderBy(i => i.ItemName)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(i => new InventoryItemResponseDto
                {
                    InventoryItemId = i.InventoryItemId,
                    ItemCode = i.ItemCode,
                    ItemName = i.ItemName,
                    InventoryCategoryId = i.InventoryCategoryId,
                    CategoryName = i.InventoryCategory?.CategoryName,
                    StudioId = i.StudioId,
                    StudioName = i.Studio?.StudioName,
                    StudioCode = i.Studio?.StudioCode,
                    QuantityOnHand = i.QuantityOnHand,
                    ReorderLevel = i.ReorderLevel,
                    UnitCost = i.UnitCost,
                    IsActive = i.IsActive,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt,
                    Condition = i.Condition,
                    Availability = i.Availability,
                    Location = i.Location
                }).ToList();

                return Results.Ok(new PagingResponse<InventoryItemResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            itemGroup.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var item = await db.InventoryItems.AsNoTracking()
                    .Include(i => i.InventoryCategory)
                    .Include(i => i.Studio)
                    .FirstOrDefaultAsync(i => i.InventoryItemId == id);

                if (item == null) return Results.NotFound();

                return Results.Ok(new InventoryItemResponseDto
                {
                    InventoryItemId = item.InventoryItemId,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    InventoryCategoryId = item.InventoryCategoryId,
                    CategoryName = item.InventoryCategory?.CategoryName,
                    StudioId = item.StudioId,
                    StudioName = item.Studio?.StudioName,
                    StudioCode = item.Studio?.StudioCode,
                    QuantityOnHand = item.QuantityOnHand,
                    ReorderLevel = item.ReorderLevel,
                    UnitCost = item.UnitCost,
                    IsActive = item.IsActive,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    Condition = item.Condition,
                    Availability = item.Availability,
                    Location = item.Location
                });
            });

            // ==================== CREATE ITEM ====================
            itemGroup.MapPost("", async (
                int companyId,
                InventoryItemCreateDto createDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);

                var category = await db.InventoryCategories.FirstOrDefaultAsync(c => c.InventoryCategoryId == createDto.InventoryCategoryId);
                if (category == null)
                    return Results.BadRequest("Inventory category not found");

                // Auto-generate sequential ItemCode if empty
                var itemCode = createDto.ItemCode?.Trim();
                if (string.IsNullOrWhiteSpace(itemCode))
                {
                    var prefix = GetCategoryPrefix(category.CategoryName);
                    var count = await db.InventoryItems.CountAsync(x => x.ItemCode.StartsWith(prefix));
                    itemCode = $"{prefix}-{(count + 1):D4}";
                    while (await db.InventoryItems.AnyAsync(x => x.ItemCode == itemCode))
                    {
                        count++;
                        itemCode = $"{prefix}-{(count + 1):D4}";
                    }
                }

                string? location = createDto.Location?.Trim();
                Studio? assignedStudio = null;
                if (createDto.StudioId.HasValue && createDto.StudioId.Value > 0)
                {
                    assignedStudio = await db.Studios.FirstOrDefaultAsync(s => s.StudioId == createDto.StudioId.Value);
                    if (assignedStudio != null && string.IsNullOrWhiteSpace(location))
                    {
                        location = assignedStudio.StudioName;
                    }
                }

                var item = new InventoryItem
                {
                    ItemCode = itemCode,
                    ItemName = createDto.ItemName.Trim(),
                    InventoryCategoryId = createDto.InventoryCategoryId,
                    StudioId = assignedStudio?.StudioId,
                    QuantityOnHand = createDto.QuantityOnHand,
                    ReorderLevel = createDto.ReorderLevel,
                    UnitCost = createDto.UnitCost,
                    IsActive = true,
                    Condition = string.IsNullOrWhiteSpace(createDto.Condition) ? "Good" : createDto.Condition,
                    Availability = string.IsNullOrWhiteSpace(createDto.Availability) ? "Available" : createDto.Availability,
                    Location = location,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                db.InventoryItems.Add(item);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityId: item.InventoryItemId,
                    entityName: "InventoryItem",
                    newValue: $"Code={item.ItemCode}, Name={item.ItemName}, CategoryId={item.InventoryCategoryId}, StudioId={item.StudioId}, Qty={item.QuantityOnHand}, Cost=₱{item.UnitCost:N2}, Condition={item.Condition}, Location={item.Location ?? "—"}");

                return Results.Created($"/tenant/{companyId}/inventory-items/{item.InventoryItemId}",
                    new InventoryItemResponseDto
                    {
                        InventoryItemId = item.InventoryItemId,
                        ItemCode = item.ItemCode,
                        ItemName = item.ItemName,
                        InventoryCategoryId = item.InventoryCategoryId,
                        CategoryName = category.CategoryName,
                        StudioId = item.StudioId,
                        StudioName = assignedStudio?.StudioName,
                        StudioCode = assignedStudio?.StudioCode,
                        QuantityOnHand = item.QuantityOnHand,
                        ReorderLevel = item.ReorderLevel,
                        UnitCost = item.UnitCost,
                        IsActive = item.IsActive,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                        Condition = item.Condition,
                        Availability = item.Availability,
                        Location = item.Location
                    });
            });

            // ==================== UPDATE ITEM ====================
            itemGroup.MapPut("/{id:int}", async (
                int companyId,
                int id,
                InventoryItemUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var item = await db.InventoryItems.Include(x => x.Studio).Include(x => x.InventoryCategory).FirstOrDefaultAsync(i => i.InventoryItemId == id);
                if (item == null) return Results.NotFound();

                var oldSnapshot = $"Code={item.ItemCode}, Name={item.ItemName}, StudioId={item.StudioId}, Qty={item.QuantityOnHand}, Cost=₱{item.UnitCost:N2}, Condition={item.Condition}, Location={item.Location ?? "—"}, Active={item.IsActive}";
                var oldIsActive = item.IsActive;

                if (!string.IsNullOrWhiteSpace(updateDto.ItemCode)) item.ItemCode = updateDto.ItemCode;
                if (!string.IsNullOrWhiteSpace(updateDto.ItemName)) item.ItemName = updateDto.ItemName;
                if (updateDto.InventoryCategoryId.HasValue) item.InventoryCategoryId = updateDto.InventoryCategoryId.Value;
                if (updateDto.QuantityOnHand.HasValue) item.QuantityOnHand = updateDto.QuantityOnHand.Value;
                if (updateDto.ReorderLevel.HasValue) item.ReorderLevel = updateDto.ReorderLevel.Value;
                if (updateDto.UnitCost.HasValue) item.UnitCost = updateDto.UnitCost.Value;
                if (updateDto.IsActive.HasValue) item.IsActive = updateDto.IsActive.Value;
                if (!string.IsNullOrWhiteSpace(updateDto.Condition)) item.Condition = updateDto.Condition;
                if (!string.IsNullOrWhiteSpace(updateDto.Availability)) item.Availability = updateDto.Availability;

                if (updateDto.StudioId.HasValue)
                {
                    if (updateDto.StudioId.Value > 0)
                    {
                        item.StudioId = updateDto.StudioId.Value;
                        var s = await db.Studios.FirstOrDefaultAsync(x => x.StudioId == updateDto.StudioId.Value);
                        if (s != null && string.IsNullOrWhiteSpace(updateDto.Location))
                        {
                            item.Location = s.StudioName;
                        }
                    }
                    else
                    {
                        item.StudioId = null;
                    }
                }

                if (updateDto.Location != null) item.Location = updateDto.Location;
                item.UpdatedAt = DateTime.UtcNow;

                db.InventoryItems.Update(item);
                await db.SaveChangesAsync();

                // Reload studio/category for response
                await db.Entry(item).Reference(x => x.Studio).LoadAsync();
                await db.Entry(item).Reference(x => x.InventoryCategory).LoadAsync();

                // 👇 Determine action
                var action = "Update";
                if (updateDto.IsActive.HasValue && updateDto.IsActive.Value != oldIsActive)
                    action = updateDto.IsActive.Value ? "Activate" : "Deactivate";

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: action,
                    entityName: "InventoryItem",
                    entityId: item.InventoryItemId,
                    oldValue: oldSnapshot,
                    newValue: $"Code={item.ItemCode}, Name={item.ItemName}, StudioId={item.StudioId}, Qty={item.QuantityOnHand}, Cost=₱{item.UnitCost:N2}, Condition={item.Condition}, Location={item.Location ?? "—"}, Active={item.IsActive}");

                return Results.Ok(new InventoryItemResponseDto
                {
                    InventoryItemId = item.InventoryItemId,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    InventoryCategoryId = item.InventoryCategoryId,
                    CategoryName = item.InventoryCategory?.CategoryName,
                    StudioId = item.StudioId,
                    StudioName = item.Studio?.StudioName,
                    StudioCode = item.Studio?.StudioCode,
                    QuantityOnHand = item.QuantityOnHand,
                    ReorderLevel = item.ReorderLevel,
                    UnitCost = item.UnitCost,
                    IsActive = item.IsActive,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    Condition = item.Condition,
                    Availability = item.Availability,
                    Location = item.Location
                });
            });

            // ==================== DELETE ITEM ====================
            itemGroup.MapDelete("/{id:int}", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var item = await db.InventoryItems.FirstOrDefaultAsync(i => i.InventoryItemId == id);
                if (item == null) return Results.NotFound();

                var oldSnapshot = $"Code={item.ItemCode}, Name={item.ItemName}, Active={item.IsActive}";

                item.IsActive = false;
                db.InventoryItems.Update(item);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "InventoryItem",
                    entityId: item.InventoryItemId,
                    oldValue: oldSnapshot,
                    newValue: "IsActive=false");

                return Results.NoContent();
            });

            // ==================== STOCK ADJUSTMENT ====================
            itemGroup.MapPost("/{id:int}/adjust-stock", async (
                int companyId,
                int id,
                InventoryStockAdjustmentDto dto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                if (dto.Delta == 0)
                    return Results.BadRequest("Delta cannot be zero.");

                await using var db = await tenantFactory.CreateAsync(companyId);
                var item = await db.InventoryItems.FirstOrDefaultAsync(i => i.InventoryItemId == id);
                if (item == null) return Results.NotFound();

                var oldQty = item.QuantityOnHand;
                var newQty = oldQty + dto.Delta;
                if (newQty < 0)
                    return Results.BadRequest($"Stock cannot go below 0. Current: {oldQty}, Delta: {dto.Delta}");

                item.QuantityOnHand = newQty;
                db.InventoryItems.Update(item);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG — Stock adjustment
                var sign = dto.Delta > 0 ? "+" : "";
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "StockAdjust",
                    entityName: "InventoryItem",
                    entityId: item.InventoryItemId,
                    oldValue: $"Qty={oldQty}",
                    newValue: $"Qty={newQty} ({sign}{dto.Delta}){(dto.Notes != null ? $", Notes={dto.Notes}" : "")}");

                return Results.Ok(new InventoryItemResponseDto
                {
                    InventoryItemId = item.InventoryItemId,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    InventoryCategoryId = item.InventoryCategoryId,
                    QuantityOnHand = item.QuantityOnHand,
                    ReorderLevel = item.ReorderLevel,
                    UnitCost = item.UnitCost,
                    IsActive = item.IsActive,
                    CreatedAt = item.CreatedAt,
                    Condition = item.Condition,
                    Availability = item.Availability,
                    Location = item.Location
                });
            });
        }

        private static string GetCategoryPrefix(string? categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) return "ITEM";
            var name = categoryName.Trim().ToLowerInvariant();

            if (name.Contains("mic")) return "MIC";
            if (name.Contains("guitar") || name.Contains("bass")) return "GTR";
            if (name.Contains("amp")) return "AMP";
            if (name.Contains("drum") || name.Contains("percussion")) return "DRM";
            if (name.Contains("synth") || name.Contains("keyboard") || name.Contains("piano")) return "SYN";
            if (name.Contains("access") || name.Contains("cable") || name.Contains("stand") || name.Contains("pick")) return "ACC";
            if (name.Contains("furn") || name.Contains("chair") || name.Contains("panel")) return "FRN";
            if (name.Contains("light") || name.Contains("softbox")) return "LGT";
            if (name.Contains("headphone") || name.Contains("audio") || name.Contains("equip")) return "AUD";

            return "ITEM";
        }
    }
}