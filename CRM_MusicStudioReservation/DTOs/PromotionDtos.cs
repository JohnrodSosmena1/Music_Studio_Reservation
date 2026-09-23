using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== RESPONSE ====================

    public class PromotionDto
    {
        [JsonPropertyName("promotionId")]
        public int PromotionId { get; set; }

        [JsonPropertyName("promotionCode")]
        public string PromotionCode { get; set; } = string.Empty;

        [JsonPropertyName("promotionName")]
        public string PromotionName { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("discountPercent")]
        public decimal DiscountPercent { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        // -------- Helpers (not serialized) --------
        [JsonIgnore]
        public bool IsCurrentlyValid =>
            IsActive && DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        [JsonIgnore]
        public bool IsExpired => DateTime.UtcNow > EndDate;

        [JsonIgnore]
        public bool IsUpcoming => DateTime.UtcNow < StartDate;

        [JsonIgnore]
        public string Status
        {
            get
            {
                if (!IsActive) return "Inactive";
                if (IsExpired) return "Expired";
                if (IsUpcoming) return "Upcoming";
                return "Active";
            }
        }
    }

    // ==================== CREATE ====================

    public class PromotionCreateRequest
    {
        [JsonPropertyName("promotionCode")]
        public string PromotionCode { get; set; } = string.Empty;

        [JsonPropertyName("promotionName")]
        public string PromotionName { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("discountPercent")]
        public decimal DiscountPercent { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
    }

    // ==================== UPDATE ====================

    public class PromotionUpdateRequest
    {
        [JsonPropertyName("promotionName")]
        public string? PromotionName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("discountPercent")]
        public decimal? DiscountPercent { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }
    }

    // ==================== VALIDATE ====================

    public class PromotionValidateResponse
    {
        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("discountPercent")]
        public decimal DiscountPercent { get; set; }
    }
}