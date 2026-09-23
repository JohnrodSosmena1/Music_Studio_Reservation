using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class CustomerFeedbackCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        [StringLength(2000)]
        public string? Comments { get; set; }
    }

    public class CustomerFeedbackResponseDto
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
