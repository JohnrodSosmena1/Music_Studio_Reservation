using System;
using System.Collections.Generic;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = null!;

        // Foreign keys
        public int CustomerId { get; set; }
        public int StudioId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus BookingStatus { get; set; } = BookingStatus.Confirmed;
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual Studio? Studio { get; set; }
        public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
