using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class BookingService
    {
        public int BookingServiceId { get; set; }

        // Foreign keys
        public int BookingId { get; set; }
        public int StudioServiceId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation
        public virtual Booking? Booking { get; set; }
        public virtual StudioService? StudioService { get; set; }
    }
}
