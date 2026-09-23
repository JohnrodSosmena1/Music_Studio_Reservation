using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== CATEGORIES ====================

    public class InventoryCategoryDto
    {
        [JsonPropertyName("inventoryCategoryId")]
        public int InventoryCategoryId { get; set; }

        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    // ==================== ITEMS ====================

    public class InventoryItemDto
    {
        [JsonPropertyName("inventoryItemId")]
        public int InventoryItemId { get; set; }

        [JsonPropertyName("itemCode")]
        public string ItemCode { get; set; } = string.Empty;

        [JsonPropertyName("itemName")]
        public string ItemName { get; set; } = string.Empty;

        [JsonPropertyName("inventoryCategoryId")]
        public int InventoryCategoryId { get; set; }

        [JsonPropertyName("categoryName")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("quantityOnHand")]
        public int QuantityOnHand { get; set; }

        [JsonPropertyName("reorderLevel")]
        public int ReorderLevel { get; set; }

        [JsonPropertyName("unitCost")]
        public decimal UnitCost { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; } = "Good";

        [JsonPropertyName("availability")]
        public string Availability { get; set; } = "Available";

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        // -------- Helpers (not serialized) --------
        [JsonIgnore]
        public bool IsLowStock => QuantityOnHand <= ReorderLevel;

        [JsonIgnore]
        public decimal TotalValue => QuantityOnHand * UnitCost;
    }

    public class InventoryItemCreateRequest
    {
        [JsonPropertyName("itemCode")]
        public string ItemCode { get; set; } = string.Empty;

        [JsonPropertyName("itemName")]
        public string ItemName { get; set; } = string.Empty;

        [JsonPropertyName("inventoryCategoryId")]
        public int InventoryCategoryId { get; set; }

        [JsonPropertyName("quantityOnHand")]
        public int QuantityOnHand { get; set; }

        [JsonPropertyName("reorderLevel")]
        public int ReorderLevel { get; set; }

        [JsonPropertyName("unitCost")]
        public decimal UnitCost { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; } = "Good";

        [JsonPropertyName("availability")]
        public string Availability { get; set; } = "Available";

        [JsonPropertyName("location")]
        public string? Location { get; set; }
    }

    public class InventoryItemUpdateRequest
    {
        [JsonPropertyName("itemCode")]
        public string? ItemCode { get; set; }

        [JsonPropertyName("itemName")]
        public string? ItemName { get; set; }

        [JsonPropertyName("inventoryCategoryId")]
        public int? InventoryCategoryId { get; set; }

        [JsonPropertyName("quantityOnHand")]
        public int? QuantityOnHand { get; set; }

        [JsonPropertyName("reorderLevel")]
        public int? ReorderLevel { get; set; }

        [JsonPropertyName("unitCost")]
        public decimal? UnitCost { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("condition")]
        public string? Condition { get; set; }

        [JsonPropertyName("availability")]
        public string? Availability { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }
    }

    // ==================== STOCK ADJUSTMENT ====================

    public class StockAdjustmentRequest
    {
        [JsonPropertyName("delta")]
        public int Delta { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }
}