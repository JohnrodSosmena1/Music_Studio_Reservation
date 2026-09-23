using System;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class LoyaltyTransaction
    {
        public int LoyaltyTransactionId { get; set; }
        public int CustomerId { get; set; }
        public int Points { get; set; }
        public LoyaltyTransactionType TransactionType { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual Customer? Customer { get; set; }
    }
}
