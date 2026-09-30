using System;
using System.Collections.Generic;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Studio
    {
        public int StudioId { get; set; }
        public string StudioCode { get; set; } = null!;
        public string StudioName { get; set; } = null!;
        public StudioType StudioType { get; set; }
        public decimal HourlyRate { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}
