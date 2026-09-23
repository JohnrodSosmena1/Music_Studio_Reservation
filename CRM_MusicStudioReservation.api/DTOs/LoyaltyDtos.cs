using System;
using System.ComponentModel.DataAnnotations;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class LoyaltyTransactionCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public LoyaltyTransactionType TransactionType { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
    }

    public class LoyaltyTransactionResponseDto
    {
        public int LoyaltyTransactionId { get; set; }
        public int CustomerId { get; set; }
        public int Points { get; set; }
        public LoyaltyTransactionType TransactionType { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
