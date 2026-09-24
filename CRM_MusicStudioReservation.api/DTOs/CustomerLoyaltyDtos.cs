namespace CRM_MusicStudioReservation.api.DTOs
{
    public class CustomerLoyaltyDto
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public int TotalBookings { get; set; }
        public int? MembershipId { get; set; }
        public int? MembershipPlanId { get; set; }
        public string? PlanName { get; set; }
        public int PointsPerBooking { get; set; }
        public int PointsFromBookings { get; set; }     // bookings × rate
        public int PointsAdjustment { get; set; }       // stored in Membership.LoyaltyPoints
        public int TotalPointsEarned { get; set; }      // PointsFromBookings + PointsAdjustment
        public int PointsBalance { get; set; }          // same as TotalPointsEarned (reserved for redemptions)
        public string MembershipStatus { get; set; } = "None";
    }
}