using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class CustomerReview
    {
        public int CustomerReviewId { get; set; }

        public int CustomerId { get; set; }

        /// <summary>Optional — the studio being reviewed.</summary>
        public int? StudioId { get; set; }

        /// <summary>Optional — the booking this review relates to.</summary>
        public int? BookingId { get; set; }

        /// <summary>"Studio" | "Service" | "General"</summary>
        public string ReviewType { get; set; } = "General";

        /// <summary>Short title/summary (optional).</summary>
        public string? Title { get; set; }

        /// <summary>1 to 5 stars.</summary>
        public int Rating { get; set; }

        /// <summary>Long-form review text.</summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>"Pending" | "Approved" | "Rejected"</summary>
        public string ModerationStatus { get; set; } = "Pending";

        /// <summary>True if the customer has a verified booking for this review.</summary>
        public bool IsVerified { get; set; } = false;

        /// <summary>Optional admin reply.</summary>
        public string? AdminReply { get; set; }

        /// <summary>Timestamp when admin replied.</summary>
        public DateTime? RepliedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual Customer? Customer { get; set; }
        public virtual Studio? Studio { get; set; }
    }
}