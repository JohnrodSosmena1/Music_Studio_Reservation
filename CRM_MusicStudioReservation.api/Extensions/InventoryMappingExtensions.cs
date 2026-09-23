using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioReservation.api.Extensions
{
    public static class InventoryMappingExtensions
    {
        // ============ InventoryCategory ============

        public static InventoryCategoryResponseDto ToDto(this InventoryCategory entity) => new()
        {
            InventoryCategoryId = entity.InventoryCategoryId,
            CategoryName = entity.CategoryName,
            Description = entity.Description
        };

        public static InventoryCategory ToEntity(this InventoryCategoryCreateDto dto) => new()
        {
            CategoryName = dto.CategoryName,
            Description = dto.Description
        };

        // ============ InventoryItem ============

        public static InventoryItemResponseDto ToDto(this InventoryItem entity) => new()
        {
            InventoryItemId = entity.InventoryItemId,
            ItemCode = entity.ItemCode,
            ItemName = entity.ItemName,
            InventoryCategoryId = entity.InventoryCategoryId,
            QuantityOnHand = entity.QuantityOnHand,
            ReorderLevel = entity.ReorderLevel,
            UnitCost = entity.UnitCost,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };

        public static InventoryItem ToEntity(this InventoryItemCreateDto dto) => new()
        {
            ItemCode = dto.ItemCode,
            ItemName = dto.ItemName,
            InventoryCategoryId = dto.InventoryCategoryId,
            QuantityOnHand = dto.QuantityOnHand,
            ReorderLevel = dto.ReorderLevel,
            UnitCost = dto.UnitCost,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        public static void ApplyUpdate(this InventoryItem entity, InventoryItemUpdateDto dto)
        {
            if (dto.ItemCode is not null) entity.ItemCode = dto.ItemCode;
            if (dto.ItemName is not null) entity.ItemName = dto.ItemName;
            if (dto.InventoryCategoryId.HasValue) entity.InventoryCategoryId = dto.InventoryCategoryId.Value;
            if (dto.QuantityOnHand.HasValue) entity.QuantityOnHand = dto.QuantityOnHand.Value;
            if (dto.ReorderLevel.HasValue) entity.ReorderLevel = dto.ReorderLevel.Value;
            if (dto.UnitCost.HasValue) entity.UnitCost = dto.UnitCost.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
        }
    }
}