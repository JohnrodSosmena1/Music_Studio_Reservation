using System;
using System.Collections.Generic;
using System.Text;
using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public decimal QuantityOnHand { get; set; }
        public decimal ReorderLevel { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Product? Product { get; set; }
    }
}
