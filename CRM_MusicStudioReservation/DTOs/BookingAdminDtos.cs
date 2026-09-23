using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== BOOKING DTO (from API) ====================

    public class BookingAdminDto
    {
        [JsonPropertyName("bookingId")]
        public int BookingId { get; set; }

        [JsonPropertyName("bookingCode")]
        public string BookingCode { get; set; } = string.Empty;

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("studioId")]
        public int StudioId { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("bookingStatus")]
        public int BookingStatus { get; set; }

        [JsonPropertyName("checkInTime")]
        public DateTime? CheckInTime { get; set; }

        [JsonPropertyName("checkOutTime")]
        public DateTime? CheckOutTime { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    // ==================== UPDATE REQUEST ====================

    public class BookingUpdateRequest
    {
        [JsonPropertyName("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime? EndTime { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("bookingStatus")]
        public int? BookingStatus { get; set; }

        [JsonPropertyName("customerId")]
        public int? CustomerId { get; set; }

        [JsonPropertyName("studioId")]
        public int? StudioId { get; set; }
    }

    // ==================== STATUS CONSTANTS ====================

    public static class BookingStatuses
    {
        public const int Pending = 1;
        public const int Confirmed = 2;
        public const int CheckedIn = 3;
        public const int CheckedOut = 4;
        public const int Cancelled = 5;
        public const int Rescheduled = 6;

        public static string GetName(int status) => status switch
        {
            Pending => "Pending",
            Confirmed => "Confirmed",
            CheckedIn => "Checked In",
            CheckedOut => "Checked Out",
            Cancelled => "Cancelled",
            Rescheduled => "Rescheduled",
            _ => "Unknown"
        };
    }
}