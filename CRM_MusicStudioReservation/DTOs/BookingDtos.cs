using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    public class BookingCreateRequest
    {
        [JsonPropertyName("customerId")] public int CustomerId { get; set; }
        [JsonPropertyName("studioId")] public int StudioId { get; set; }
        [JsonPropertyName("startTime")] public DateTime StartTime { get; set; }
        [JsonPropertyName("endTime")] public DateTime EndTime { get; set; }
        [JsonPropertyName("notes")] public string? Notes { get; set; }
    }

    public class BookingResponse
    {
        [JsonPropertyName("bookingId")] public int BookingId { get; set; }
        [JsonPropertyName("bookingCode")] public string BookingCode { get; set; } = string.Empty;
        [JsonPropertyName("customerId")] public int CustomerId { get; set; }
        [JsonPropertyName("studioId")] public int StudioId { get; set; }
        [JsonPropertyName("startTime")] public DateTime StartTime { get; set; }
        [JsonPropertyName("endTime")] public DateTime EndTime { get; set; }
        [JsonPropertyName("totalAmount")] public decimal TotalAmount { get; set; }
        [JsonPropertyName("bookingStatus")] public int BookingStatus { get; set; }
        [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }
    }
}