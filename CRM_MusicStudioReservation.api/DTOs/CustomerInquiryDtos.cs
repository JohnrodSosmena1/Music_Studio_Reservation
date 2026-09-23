using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    // ==================== CREATE ====================

    public class CustomerInquiryCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = null!;

        [Required]
        [StringLength(4000)]
        public string Message { get; set; } = null!;

        [StringLength(20)]
        public string Priority { get; set; } = "Normal";
    }

    // ==================== UPDATE (subject / message / priority) ====================

    public class CustomerInquiryUpdateDto
    {
        [StringLength(200)]
        public string? Subject { get; set; }

        [StringLength(4000)]
        public string? Message { get; set; }

        [StringLength(20)]
        public string? Priority { get; set; }
    }

    // ==================== RESPOND ====================

    public class CustomerInquiryRespondDto
    {
        [Required]
        [StringLength(4000)]
        public string Response { get; set; } = null!;

        /// <summary>
        /// Optional new status to apply. Defaults to "Resolved" if not provided.
        /// Allowed: Open, InProgress, Resolved, Closed.
        /// </summary>
        [StringLength(20)]
        public string? Status { get; set; }
    }

    // ==================== STATUS CHANGE ====================

    public class CustomerInquiryStatusDto
    {
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = null!;
    }

    // ==================== RESPONSE ====================

    public class CustomerInquiryResponseDto
    {
        public int CustomerInquiryId { get; set; }
        public int CustomerId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
        public string Priority { get; set; } = "Normal";
        public string? Response { get; set; }
        public DateTime? RespondedAt { get; set; }
        public string? RespondedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}