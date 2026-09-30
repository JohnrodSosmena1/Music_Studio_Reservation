using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class PromotionCreateDto
    {
        [Required]
        [StringLength(100)]
        public string PromotionName { get; set; } = null!;

        [StringLength(2000)]
        public string? Description { get; set; }

        [Range(0, 100)]
        public decimal DiscountPercent { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }

    public class PromotionUpdateDto
    {
        [StringLength(50)]
        public string? PromotionCode { get; set; }

        [StringLength(100)]
        public string? PromotionName { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [Range(0, 100)]
        public decimal? DiscountPercent { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PromotionResponseDto
    {
        public int PromotionId { get; set; }
        public string PromotionCode { get; set; } = null!;
        public string PromotionName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
