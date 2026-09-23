using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioReservation.api.Extensions
{
    public static class PromotionMappingExtensions
    {
        public static PromotionResponseDto ToDto(this Promotion entity) => new()
        {
            PromotionId = entity.PromotionId,
            PromotionCode = entity.PromotionCode,
            PromotionName = entity.PromotionName,
            Description = entity.Description,
            DiscountPercent = entity.DiscountPercent,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };

        public static Promotion ToEntity(this PromotionCreateDto dto) => new()
        {
            PromotionCode = dto.PromotionCode,
            PromotionName = dto.PromotionName,
            Description = dto.Description,
            DiscountPercent = dto.DiscountPercent,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        public static void ApplyUpdate(this Promotion entity, PromotionUpdateDto dto)
        {
            if (dto.PromotionCode is not null) entity.PromotionCode = dto.PromotionCode;
            if (dto.PromotionName is not null) entity.PromotionName = dto.PromotionName;
            if (dto.Description is not null) entity.Description = dto.Description;
            if (dto.DiscountPercent.HasValue) entity.DiscountPercent = dto.DiscountPercent.Value;
            if (dto.StartDate.HasValue) entity.StartDate = dto.StartDate.Value;
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
        }
    }
}