using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class InventoryCategory
    {
        public int InventoryCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }

        // Navigation
        public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}
