using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyCode { get; set; } = string.Empty; // e.g. TEN-00001

        public string CompanyName { get; set; } = string.Empty;

        public string? Subdomain { get; set; } // e.g. "studio1" -> studio1.crmapp.com

        public string? OwnerFirstName { get; set; }

        public string? OwnerLastName { get; set; }

        public string? OwnerEmail { get; set; }

        public string? ContactNumber { get; set; }

        public string TimeZone { get; set; } = "Asia/Manila";

        public int? SubscriptionPlanId { get; set; }

        public string Status { get; set; } = "Active"; // Active, Inactive, Suspended, Trial, Expired

        public DateTime? SubscriptionStart { get; set; }

        public DateTime? SubscriptionEnd { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual SubscriptionPlan? SubscriptionPlan { get; set; }

        public ICollection<Device> Devices { get; set; } = new List<Device>();

        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public ICollection<SubscriptionInvoice> Invoices { get; set; } = new List<SubscriptionInvoice>();
    }
}
