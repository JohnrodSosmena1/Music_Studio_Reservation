using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioReservation.api.Extensions
{
    public static class CustomerFeedbackMappingExtensions
    {
        public static CustomerFeedbackResponseDto ToDto(this CustomerFeedback entity) => new()
        {
            FeedbackId = entity.FeedbackId,
            CustomerId = entity.CustomerId,
            Rating = entity.Rating,
            Comments = entity.Comments,
            CreatedAt = entity.CreatedAt
        };

        public static CustomerFeedback ToEntity(this CustomerFeedbackCreateDto dto) => new()
        {
            CustomerId = dto.CustomerId,
            Rating = dto.Rating,
            Comments = dto.Comments,
            CreatedAt = DateTime.UtcNow
        };
    }
}