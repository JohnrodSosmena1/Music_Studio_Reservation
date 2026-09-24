using System;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== RESPONSE ====================
    public class MembershipResponseDto
    {
        [JsonPropertyName("membershipId")] public int MembershipId { get; set; }
        [JsonPropertyName("customerId")] public int CustomerId { get; set; }
        [JsonPropertyName("membershipPlanId")] public int MembershipPlanId { get; set; }
        [JsonPropertyName("startDate")] public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")] public DateTime? EndDate { get; set; }
        [JsonPropertyName("membershipStatus")] public int MembershipStatus { get; set; }
        [JsonPropertyName("loyaltyPoints")] public int LoyaltyPoints { get; set; }
        [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }
    }

    // ==================== PAGING RESPONSE ====================
    public class PagingResponse<T>
    {
        [JsonPropertyName("items")] public System.Collections.Generic.List<T> Items { get; set; } = new();
        [JsonPropertyName("totalItems")] public int TotalItems { get; set; }
        [JsonPropertyName("totalPages")] public int TotalPages { get; set; }
        [JsonPropertyName("currentPage")] public int CurrentPage { get; set; }
        [JsonPropertyName("pageSize")] public int PageSize { get; set; }
    }

    // ==================== CREATE REQUEST ====================
    public class MembershipCreateRequest
    {
        [JsonPropertyName("customerId")] public int CustomerId { get; set; }
        [JsonPropertyName("membershipPlanId")] public int MembershipPlanId { get; set; }
        [JsonPropertyName("startDate")] public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")] public DateTime? EndDate { get; set; }
    }

    // ==================== UPDATE REQUEST ====================
    public class MembershipUpdateRequest
    {
        [JsonPropertyName("endDate")] public DateTime? EndDate { get; set; }
        [JsonPropertyName("membershipStatus")] public int? MembershipStatus { get; set; }
        [JsonPropertyName("loyaltyPoints")] public int? LoyaltyPoints { get; set; }
    }
}