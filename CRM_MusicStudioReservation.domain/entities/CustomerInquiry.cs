using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class CustomerInquiry
    {
        public int CustomerInquiryId { get; set; }

        public int CustomerId { get; set; }

        /// <summary>Short title/summary of the inquiry.</summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>The full message from the customer.</summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>"Open" | "InProgress" | "Resolved" | "Closed"</summary>
        public string Status { get; set; } = "Open";

        /// <summary>"Low" | "Normal" | "High" | "Urgent"</summary>
        public string Priority { get; set; } = "Normal";

        /// <summary>Admin/staff response (single reply).</summary>
        public string? Response { get; set; }

        /// <summary>When the response was sent.</summary>
        public DateTime? RespondedAt { get; set; }

        /// <summary>Who replied (email or user id).</summary>
        public string? RespondedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual Customer? Customer { get; set; }
    }
}