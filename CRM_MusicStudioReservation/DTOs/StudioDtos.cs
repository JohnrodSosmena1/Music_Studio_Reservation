using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    public class StudioDto
    {
        [JsonPropertyName("studioId")]
        public int StudioId { get; set; }

        [JsonPropertyName("studioCode")]
        public string StudioCode { get; set; } = string.Empty;

        [JsonPropertyName("studioName")]
        public string StudioName { get; set; } = string.Empty;

        [JsonPropertyName("studioType")]
        public int StudioType { get; set; }

        [JsonPropertyName("hourlyRate")]
        public decimal HourlyRate { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>Matches the PagingResponse&lt;T&gt; from the API.</summary>
    public class PagedResponse<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = new();

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }
}
