using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class BookingServiceCreateDto
    {
        [Required]
        public int StudioServiceId { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;

        [Range(0, 100000)]
        public decimal UnitPrice { get; set; }
    }

    public class BookingServiceResponseDto
    {
        public int BookingServiceId { get; set; }
        public int StudioServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class BookingCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int StudioId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public BookingStatus? BookingStatus { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }

        public List<BookingServiceCreateDto>? Services { get; set; }
    }

    public class BookingUpdateDto
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Notes { get; set; }

        // ✅ Changed from int? to BookingStatus? to match the entity
        public BookingStatus? BookingStatus { get; set; }

        public int? CustomerId { get; set; }
        public int? StudioId { get; set; }
    }

    public class BookingResponseDto
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = null!;
        public int CustomerId { get; set; }
        public int StudioId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<BookingServiceResponseDto>? Services { get; set; }
    }
}