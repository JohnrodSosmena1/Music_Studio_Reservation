using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class MembershipCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int MembershipPlanId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class MembershipUpdateDto
    {
        public DateTime? EndDate { get; set; }
        public MembershipStatus? MembershipStatus { get; set; }
        public int? LoyaltyPoints { get; set; }
    }

    public class MembershipResponseDto
    {
        public int MembershipId { get; set; }
        public int CustomerId { get; set; }
        public int MembershipPlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
        public int LoyaltyPoints { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class MembershipPlanCreateDto
    {
        [Required]
        [StringLength(100)]
        public string PlanName { get; set; } = null!;

        [StringLength(2000)]
        public string? Description { get; set; }

        [Range(0, 100000)]
        public decimal MonthlyFee { get; set; }

        [Range(0, 100000)]
        public int LoyaltyPointsPerBooking { get; set; }

        public string? Benefits { get; set; }
    }

    public class MembershipPlanUpdateDto
    {
        [StringLength(100)]
        public string? PlanName { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [Range(0, 100000)]
        public decimal? MonthlyFee { get; set; }

        public int? LoyaltyPointsPerBooking { get; set; }
        public string? Benefits { get; set; }
        public bool? IsActive { get; set; }
    }

    public class MembershipPlanResponseDto
    {
        public int MembershipPlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal MonthlyFee { get; set; }
        public int LoyaltyPointsPerBooking { get; set; }
        public string? Benefits { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
