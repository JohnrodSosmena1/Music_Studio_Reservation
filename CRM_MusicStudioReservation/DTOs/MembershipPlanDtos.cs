using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== RESPONSE ====================

    public class MembershipPlanDto
    {
        [JsonPropertyName("membershipPlanId")]
        public int MembershipPlanId { get; set; }

        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("monthlyFee")]
        public decimal MonthlyFee { get; set; }

        [JsonPropertyName("loyaltyPointsPerBooking")]
        public int LoyaltyPointsPerBooking { get; set; }

        [JsonPropertyName("benefits")]
        public string? Benefits { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        // Helpers
        [JsonIgnore]
        public string FeeDisplay => $"₱{MonthlyFee:N2} / month";

        [JsonIgnore]
        public string PointsDisplay => $"+{LoyaltyPointsPerBooking} pts/booking";

        [JsonIgnore]
        public string Status => IsActive ? "Active" : "Inactive";

        [JsonIgnore]
        public string BenefitsPreview
        {
            get
            {
                var b = (Benefits ?? "").Trim();
                return b.Length > 80 ? b.Substring(0, 80) + "…" : b;
            }
        }
    }

    // ==================== CREATE ====================

    public class MembershipPlanCreateRequest
    {
        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("monthlyFee")]
        public decimal MonthlyFee { get; set; }

        [JsonPropertyName("loyaltyPointsPerBooking")]
        public int LoyaltyPointsPerBooking { get; set; }

        [JsonPropertyName("benefits")]
        public string? Benefits { get; set; }
    }

    // ==================== UPDATE ====================

    public class MembershipPlanUpdateRequest
    {
        [JsonPropertyName("planName")]
        public string? PlanName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("monthlyFee")]
        public decimal? MonthlyFee { get; set; }

        [JsonPropertyName("loyaltyPointsPerBooking")]
        public int? LoyaltyPointsPerBooking { get; set; }

        [JsonPropertyName("benefits")]
        public string? Benefits { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }
    }
}