using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.api.Extensions
{
    public static class MembershipMappingExtensions
    {
        public static MembershipResponseDto ToDto(this Membership entity) => new()
        {
            MembershipId = entity.MembershipId,
            CustomerId = entity.CustomerId,
            MembershipPlanId = entity.MembershipPlanId,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            MembershipStatus = entity.MembershipStatus,
            LoyaltyPoints = entity.LoyaltyPoints,
            CreatedAt = entity.CreatedAt
        };

        public static Membership ToEntity(this MembershipCreateDto dto) => new()
        {
            CustomerId = dto.CustomerId,
            MembershipPlanId = dto.MembershipPlanId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            MembershipStatus = MembershipStatus.Active,
            LoyaltyPoints = 0,
            CreatedAt = DateTime.UtcNow
        };

        public static void ApplyUpdate(this Membership entity, MembershipUpdateDto dto)
        {
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate.Value;
            if (dto.MembershipStatus.HasValue) entity.MembershipStatus = dto.MembershipStatus.Value;
            if (dto.LoyaltyPoints.HasValue) entity.LoyaltyPoints = dto.LoyaltyPoints.Value;
        }

        public static MembershipPlanResponseDto ToDto(this MembershipPlan entity) => new()
        {
            MembershipPlanId = entity.MembershipPlanId,
            PlanName = entity.PlanName,
            Description = entity.Description,
            MonthlyFee = entity.MonthlyFee,
            LoyaltyPointsPerBooking = entity.LoyaltyPointsPerBooking,
            Benefits = entity.Benefits,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };

        public static MembershipPlan ToEntity(this MembershipPlanCreateDto dto) => new()
        {
            PlanName = dto.PlanName,
            Description = dto.Description,
            MonthlyFee = dto.MonthlyFee,
            LoyaltyPointsPerBooking = dto.LoyaltyPointsPerBooking,
            Benefits = dto.Benefits,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        public static void ApplyUpdate(this MembershipPlan entity, MembershipPlanUpdateDto dto)
        {
            if (dto.PlanName is not null) entity.PlanName = dto.PlanName;
            if (dto.Description is not null) entity.Description = dto.Description;
            if (dto.MonthlyFee.HasValue) entity.MonthlyFee = dto.MonthlyFee.Value;
            if (dto.LoyaltyPointsPerBooking.HasValue) entity.LoyaltyPointsPerBooking = dto.LoyaltyPointsPerBooking.Value;
            if (dto.Benefits is not null) entity.Benefits = dto.Benefits;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
        }
    }
}