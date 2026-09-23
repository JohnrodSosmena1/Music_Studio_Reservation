using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class CustomerFeedback
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual Customer? Customer { get; set; }
    }
}
