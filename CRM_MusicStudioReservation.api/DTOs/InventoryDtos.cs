using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioReservation.api.DTOs
{
    // ==================== CATEGORIES ====================

    public class InventoryCategoryCreateDto
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }
    }

    public class InventoryCategoryResponseDto
    {
        public int InventoryCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
    }

    // ==================== ITEMS ====================

    public class InventoryItemCreateDto
    {
        [StringLength(50)]
        public string? ItemCode { get; set; }

        [Required]
        [StringLength(200)]
        public string ItemName { get; set; } = null!;

        [Required]
        public int InventoryCategoryId { get; set; }

        public int? StudioId { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityOnHand { get; set; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; }

        [Range(0, 100000)]
        public decimal UnitCost { get; set; }

        // 👇 NEW
        [StringLength(50)]
        public string Condition { get; set; } = "Good";

        [StringLength(50)]
        public string Availability { get; set; } = "Available";

        [StringLength(200)]
        public string? Location { get; set; }
    }

    public class InventoryItemUpdateDto
    {
        [StringLength(50)]
        public string? ItemCode { get; set; }

        [StringLength(200)]
        public string? ItemName { get; set; }

        public int? InventoryCategoryId { get; set; }
        public int? StudioId { get; set; }
        public int? QuantityOnHand { get; set; }
        public int? ReorderLevel { get; set; }
        public decimal? UnitCost { get; set; }
        public bool? IsActive { get; set; }

        // 👇 NEW
        [StringLength(50)]
        public string? Condition { get; set; }

        [StringLength(50)]
        public string? Availability { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }
    }

    public class InventoryItemResponseDto
    {
        public int InventoryItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public int InventoryCategoryId { get; set; }
        public string? CategoryName { get; set; }        // 👈 NEW — populated in response for convenience
        public int? StudioId { get; set; }
        public string? StudioName { get; set; }
        public string? StudioCode { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitCost { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // 👇 NEW
        public string Condition { get; set; } = "Good";
        public string Availability { get; set; } = "Available";
        public string? Location { get; set; }
    }

    // ==================== STOCK ADJUSTMENT ====================

    public class InventoryStockAdjustmentDto
    {
        /// <summary>Positive = stock in, Negative = stock out.</summary>
        [Required]
        public int Delta { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}