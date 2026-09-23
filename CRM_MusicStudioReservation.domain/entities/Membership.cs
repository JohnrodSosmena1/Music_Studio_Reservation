using System;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Membership
    {
        public int MembershipId { get; set; }

        // Foreign keys
        public int CustomerId { get; set; }
        public int MembershipPlanId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MembershipStatus MembershipStatus { get; set; } = MembershipStatus.Active;
        public int LoyaltyPoints { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual Customer? Customer { get; set; }
        public virtual MembershipPlan? MembershipPlan { get; set; }
    }
}
