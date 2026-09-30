using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== CREATE ====================
    public class StudioCreateRequest
    {
        [JsonPropertyName("studioCode")]
        public string? StudioCode { get; set; }

        [JsonPropertyName("studioName")]
        public string StudioName { get; set; } = string.Empty;

        [JsonPropertyName("studioType")]
        public int StudioType { get; set; }                     // ← int, not string

        [JsonPropertyName("hourlyRate")]
        public decimal HourlyRate { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    // ==================== UPDATE ====================
    public class StudioUpdateRequest
    {
        [JsonPropertyName("studioCode")]
        public string? StudioCode { get; set; }

        [JsonPropertyName("studioName")]
        public string? StudioName { get; set; }

        [JsonPropertyName("studioType")]
        public int? StudioType { get; set; }                    // ← int?, not string

        [JsonPropertyName("hourlyRate")]
        public decimal? HourlyRate { get; set; }

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }
    }
}