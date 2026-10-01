using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class SubscriptionInvoice
    {
        public int SubscriptionInvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty; // e.g. INV-00001

        public int CompanyId { get; set; }

        public int? SubscriptionId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = "Paid"; // Pending, Paid, Overdue, Cancelled

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public DateTime DueAt { get; set; } = DateTime.UtcNow.AddDays(7);

        public DateTime? PaidAt { get; set; } = DateTime.UtcNow;

        public string? PaymentMethod { get; set; } = "Credit Card";

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Company? Company { get; set; }

        public virtual Subscription? Subscription { get; set; }
    }
}
