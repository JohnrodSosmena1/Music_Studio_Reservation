using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.api.Extensions
{
    public static class BookingMappingExtensions
    {
        public static BookingResponseDto ToDto(this Booking entity) => new()
        {
            BookingId = entity.BookingId,
            BookingCode = entity.BookingCode,
            CustomerId = entity.CustomerId,
            StudioId = entity.StudioId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            TotalAmount = entity.TotalAmount,
            BookingStatus = entity.BookingStatus,
            CheckInTime = entity.CheckInTime,
            CheckOutTime = entity.CheckOutTime,
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt,
            Services = entity.BookingServices?
                .Select(bs => bs.ToDto())
                .ToList()
        };

        public static Booking ToEntity(this BookingCreateDto dto) => new()
        {
            CustomerId = dto.CustomerId,
            StudioId = dto.StudioId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Notes = dto.Notes,
            BookingStatus = BookingStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        public static void ApplyUpdate(this Booking entity, BookingUpdateDto dto)
        {
            if (dto.StartTime.HasValue) entity.StartTime = dto.StartTime.Value;
            if (dto.EndTime.HasValue) entity.EndTime = dto.EndTime.Value;
            if (dto.Notes is not null) entity.Notes = dto.Notes;
            if (dto.BookingStatus.HasValue) entity.BookingStatus = dto.BookingStatus.Value;
        }

        public static BookingServiceResponseDto ToDto(this BookingService entity) => new()
        {
            BookingServiceId = entity.BookingServiceId,
            StudioServiceId = entity.StudioServiceId,
            Quantity = entity.Quantity,
            UnitPrice = entity.UnitPrice
        };

        public static BookingService ToEntity(this BookingServiceCreateDto dto) => new()
        {
            StudioServiceId = dto.StudioServiceId,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice
        };
    }
}