using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        public int CompanyId { get; set; }

        public int SubscriptionPlanId { get; set; }

        public string Status { get; set; } = "Active"; // Trial, Active, PastDue, Cancelled, Expired

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMonths(1);

        public bool AutoRenew { get; set; } = true;

        public DateTime? CancelledAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual Company? Company { get; set; }

        public virtual SubscriptionPlan? SubscriptionPlan { get; set; }
    }
}
