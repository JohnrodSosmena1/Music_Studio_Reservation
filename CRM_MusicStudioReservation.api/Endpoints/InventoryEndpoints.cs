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
            var categoryGroup = app.MapGroup("/tenant/{companyId:int}/inventory-categories");
            var itemGroup = app.MapGroup("/tenant/{companyId:int}/inventory-items");

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
                    .Where(i => i.IsActive);

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(i => i.ItemName.Contains(search) || i.ItemCode.Contains(search));

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
                    QuantityOnHand = i.QuantityOnHand,
                    ReorderLevel = i.ReorderLevel,
                    UnitCost = i.UnitCost,
                    IsActive = i.IsActive,
                    CreatedAt = i.CreatedAt,
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
                    .FirstOrDefaultAsync(i => i.InventoryItemId == id);

                if (item == null) return Results.NotFound();

                return Results.Ok(new InventoryItemResponseDto
                {
                    InventoryItemId = item.InventoryItemId,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    InventoryCategoryId = item.InventoryCategoryId,
                    CategoryName = item.InventoryCategory?.CategoryName,
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

                var categoryExists = await db.InventoryCategories.AnyAsync(c => c.InventoryCategoryId == createDto.InventoryCategoryId);
                if (!categoryExists)
                    return Results.BadRequest("Inventory category not found");

                var item = new InventoryItem
                {
                    ItemCode = createDto.ItemCode,
                    ItemName = createDto.ItemName,
                    InventoryCategoryId = createDto.InventoryCategoryId,
                    QuantityOnHand = createDto.QuantityOnHand,
                    ReorderLevel = createDto.ReorderLevel,
                    UnitCost = createDto.UnitCost,
                    IsActive = true,
                    Condition = string.IsNullOrWhiteSpace(createDto.Condition) ? "Good" : createDto.Condition,
                    Availability = string.IsNullOrWhiteSpace(createDto.Availability) ? "Available" : createDto.Availability,
                    Location = createDto.Location
                };

                db.InventoryItems.Add(item);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityId: item.InventoryItemId,
                    entityName: "InventoryItem",
                    newValue: $"Code={item.ItemCode}, Name={item.ItemName}, CategoryId={item.InventoryCategoryId}, Qty={item.QuantityOnHand}, Cost=₱{item.UnitCost:N2}, Condition={item.Condition}, Location={item.Location ?? "—"}");

                return Results.Created($"/tenant/{companyId}/inventory-items/{item.InventoryItemId}",
                    new InventoryItemResponseDto
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
                var item = await db.InventoryItems.FirstOrDefaultAsync(i => i.InventoryItemId == id);
                if (item == null) return Results.NotFound();

                var oldSnapshot = $"Code={item.ItemCode}, Name={item.ItemName}, Qty={item.QuantityOnHand}, Cost=₱{item.UnitCost:N2}, Condition={item.Condition}, Location={item.Location ?? "—"}, Active={item.IsActive}";
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
                if (updateDto.Location != null) item.Location = updateDto.Location;

                db.InventoryItems.Update(item);
                await db.SaveChangesAsync();

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
                    newValue: $"Code={item.ItemCode}, Name={item.ItemName}, Qty={item.QuantityOnHand}, Cost=₱{item.UnitCost:N2}, Condition={item.Condition}, Location={item.Location ?? "—"}, Active={item.IsActive}");

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
    }
}