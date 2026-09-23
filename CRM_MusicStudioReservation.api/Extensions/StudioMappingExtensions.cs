using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioReservation.api.Extensions
{
    public static class StudioMappingExtensions
    {
        public static StudioResponseDto ToDto(this Studio entity) => new()
        {
            StudioId = entity.StudioId,
            StudioCode = entity.StudioCode,
            StudioName = entity.StudioName,
            StudioType = entity.StudioType,
            HourlyRate = entity.HourlyRate,
            Capacity = entity.Capacity,
            Description = entity.Description,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };

        public static Studio ToEntity(this StudioCreateDto dto) => new()
        {
            StudioCode = dto.StudioCode,
            StudioName = dto.StudioName,
            StudioType = dto.StudioType,
            HourlyRate = dto.HourlyRate,
            Capacity = dto.Capacity,
            Description = dto.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        public static void ApplyUpdate(this Studio entity, StudioUpdateDto dto)
        {
            if (dto.StudioCode is not null) entity.StudioCode = dto.StudioCode;
            if (dto.StudioName is not null) entity.StudioName = dto.StudioName;
            if (dto.StudioType.HasValue) entity.StudioType = dto.StudioType.Value;
            if (dto.HourlyRate.HasValue) entity.HourlyRate = dto.HourlyRate.Value;
            if (dto.Capacity.HasValue) entity.Capacity = dto.Capacity.Value;
            if (dto.Description is not null) entity.Description = dto.Description;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
        }
    }
}