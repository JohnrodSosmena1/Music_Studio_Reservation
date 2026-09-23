using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class MembershipPlan
    {
        public int MembershipPlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal MonthlyFee { get; set; }
        public int LoyaltyPointsPerBooking { get; set; }
        public string? Benefits { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}
