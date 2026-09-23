using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    // ==================== CREATE ====================

    public class CustomerReviewCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        public int? StudioId { get; set; }
        public int? BookingId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReviewType { get; set; } = "General";

        [StringLength(200)]
        public string? Title { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(4000)]
        public string Comment { get; set; } = null!;
    }

    // ==================== UPDATE ====================

    public class CustomerReviewUpdateDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        [StringLength(4000)]
        public string? Comment { get; set; }

        [StringLength(50)]
        public string? ReviewType { get; set; }
    }

    // ==================== MODERATE ====================

    public class CustomerReviewModerateDto
    {
        /// <summary>"Approved" | "Rejected" | "Pending"</summary>
        [Required]
        [StringLength(20)]
        public string ModerationStatus { get; set; } = null!;
    }

    // ==================== REPLY ====================

    public class CustomerReviewReplyDto
    {
        [Required]
        [StringLength(2000)]
        public string AdminReply { get; set; } = null!;
    }

    // ==================== RESPONSE ====================

    public class CustomerReviewResponseDto
    {
        public int CustomerReviewId { get; set; }
        public int CustomerId { get; set; }
        public int? StudioId { get; set; }
        public int? BookingId { get; set; }
        public string ReviewType { get; set; } = "General";
        public string? Title { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string ModerationStatus { get; set; } = "Pending";
        public bool IsVerified { get; set; }
        public string? AdminReply { get; set; }
        public DateTime? RepliedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}