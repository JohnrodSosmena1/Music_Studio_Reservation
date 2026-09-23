using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string PromotionCode { get; set; } = null!;
        public string PromotionName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
