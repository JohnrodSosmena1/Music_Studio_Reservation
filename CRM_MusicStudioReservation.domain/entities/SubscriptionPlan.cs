using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class SubscriptionPlan
    {
        public int SubscriptionPlanId { get; set; }

        public string PlanCode { get; set; } = string.Empty; // e.g. PLAN-FREE, PLAN-BASIC, PLAN-PRO, PLAN-ENT

        public string PlanName { get; set; } = string.Empty; // Free, Basic, Pro, Enterprise

        public decimal Price { get; set; } = 0.00m;

        public string BillingCycle { get; set; } = "Monthly"; // Monthly, Yearly

        public int MaxUsers { get; set; } = 5;

        public int MaxBookingsPerMonth { get; set; } = 100;

        public int MaxStorageMb { get; set; } = 1024;

        public string Features { get; set; } = string.Empty; // JSON or comma-separated list of features

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Company> Companies { get; set; } = new List<Company>();

        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
