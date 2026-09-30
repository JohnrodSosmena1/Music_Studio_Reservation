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

        [JsonPropertyName("inventoryItemsCount")]
        public int InventoryItemsCount { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class StudioInventoryItemDto
    {
        [JsonPropertyName("inventoryItemId")]
        public int InventoryItemId { get; set; }

        [JsonPropertyName("itemCode")]
        public string ItemCode { get; set; } = string.Empty;

        [JsonPropertyName("itemName")]
        public string ItemName { get; set; } = string.Empty;

        [JsonPropertyName("categoryName")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("quantityOnHand")]
        public int QuantityOnHand { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; } = "Good";

        [JsonPropertyName("availability")]
        public string Availability { get; set; } = "Available";

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("unitCost")]
        public decimal UnitCost { get; set; }
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
