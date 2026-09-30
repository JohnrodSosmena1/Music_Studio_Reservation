using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public int InventoryCategoryId { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitCost { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 👇 NEW — Equipment-specific fields
        /// <summary>New | Good | Fair | NeedsRepair | Retired</summary>
        public string Condition { get; set; } = "Good";

        /// <summary>Available | InUse | Maintenance | Lost</summary>
        public string Availability { get; set; } = "Available";

        /// <summary>Free text — e.g. "Studio A", "Storage Room"</summary>
        public string? Location { get; set; }
        public int? StudioId { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual InventoryCategory? InventoryCategory { get; set; }
        public virtual Studio? Studio { get; set; }
    }
}