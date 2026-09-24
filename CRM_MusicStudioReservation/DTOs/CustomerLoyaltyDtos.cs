using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    public class CustomerLoyaltyDto
    {
        [JsonPropertyName("customerId")] public int CustomerId { get; set; }
        [JsonPropertyName("customerCode")] public string CustomerCode { get; set; } = "";
        [JsonPropertyName("customerName")] public string CustomerName { get; set; } = "";
        [JsonPropertyName("email")] public string? Email { get; set; }
        [JsonPropertyName("contactNumber")] public string? ContactNumber { get; set; }
        [JsonPropertyName("totalBookings")] public int TotalBookings { get; set; }
        [JsonPropertyName("membershipId")] public int? MembershipId { get; set; }
        [JsonPropertyName("membershipPlanId")] public int? MembershipPlanId { get; set; }
        [JsonPropertyName("planName")] public string? PlanName { get; set; }
        [JsonPropertyName("pointsPerBooking")] public int PointsPerBooking { get; set; }
        [JsonPropertyName("pointsFromBookings")] public int PointsFromBookings { get; set; }
        [JsonPropertyName("pointsAdjustment")] public int PointsAdjustment { get; set; }
        [JsonPropertyName("totalPointsEarned")] public int TotalPointsEarned { get; set; }
        [JsonPropertyName("pointsBalance")] public int PointsBalance { get; set; }
        [JsonPropertyName("membershipStatus")] public string MembershipStatus { get; set; } = "None";

        [JsonIgnore] public string PlanDisplay => string.IsNullOrEmpty(PlanName) ? "(none)" : PlanName;
        [JsonIgnore] public string PointsPerBookingDisplay => $"+{PointsPerBooking} pts/booking";
        [JsonIgnore] public bool IsMember => !string.IsNullOrEmpty(PlanName);
    }
}