using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== DASHBOARD ====================

    public class SuperAdminDashboardDto
    {
        [JsonPropertyName("totalOrganizations")]
        public int TotalOrganizations { get; set; }

        [JsonPropertyName("activeOrganizations")]
        public int ActiveOrganizations { get; set; }

        [JsonPropertyName("suspendedOrganizations")]
        public int SuspendedOrganizations { get; set; }

        [JsonPropertyName("inactiveOrganizations")]
        public int InactiveOrganizations { get; set; }

        [JsonPropertyName("trialOrganizations")]
        public int TrialOrganizations { get; set; }

        [JsonPropertyName("totalPlatformUsers")]
        public int TotalPlatformUsers { get; set; }

        [JsonPropertyName("activeSubscriptions")]
        public int ActiveSubscriptions { get; set; }

        [JsonPropertyName("monthlyRecurringRevenue")]
        public decimal MonthlyRecurringRevenue { get; set; }

        [JsonPropertyName("expiringSoonCount")]
        public int ExpiringSoonCount { get; set; }

        [JsonPropertyName("tenantGrowth")]
        public List<MonthlyGrowthItemDto> TenantGrowth { get; set; } = new();

        [JsonPropertyName("statusDistribution")]
        public List<StatusCountItemDto> StatusDistribution { get; set; } = new();

        [JsonPropertyName("recentActivities")]
        public List<RecentActivityItemDto> RecentActivities { get; set; } = new();
    }

    public class MonthlyGrowthItemDto
    {
        [JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class StatusCountItemDto
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; } = "#8B5CF6";
    }

    public class RecentActivityItemDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("actionType")]
        public string ActionType { get; set; } = string.Empty;

        [JsonPropertyName("timeAgo")]
        public string TimeAgo { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    // ==================== ORGANIZATIONS ====================

    public class OrganizationPagedResponse
    {
        [JsonPropertyName("items")]
        public List<OrganizationListItemDto> Items { get; set; } = new();

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }

    public class OrganizationListItemDto
    {
        [JsonPropertyName("companyId")]
        public int CompanyId { get; set; }

        [JsonPropertyName("companyCode")]
        public string CompanyCode { get; set; } = string.Empty;

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("ownerName")]
        public string? OwnerName { get; set; }

        [JsonPropertyName("ownerEmail")]
        public string? OwnerEmail { get; set; }

        [JsonPropertyName("subdomain")]
        public string? Subdomain { get; set; }

        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = "Free";

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Active";

        [JsonPropertyName("subscriptionStart")]
        public DateTime? SubscriptionStart { get; set; }

        [JsonPropertyName("subscriptionEnd")]
        public DateTime? SubscriptionEnd { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class OrganizationDetailDto
    {
        [JsonPropertyName("companyId")]
        public int CompanyId { get; set; }

        [JsonPropertyName("companyCode")]
        public string CompanyCode { get; set; } = string.Empty;

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("subdomain")]
        public string? Subdomain { get; set; }

        [JsonPropertyName("ownerFirstName")]
        public string? OwnerFirstName { get; set; }

        [JsonPropertyName("ownerLastName")]
        public string? OwnerLastName { get; set; }

        [JsonPropertyName("ownerEmail")]
        public string? OwnerEmail { get; set; }

        [JsonPropertyName("contactNumber")]
        public string? ContactNumber { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; } = "Asia/Manila";

        [JsonPropertyName("subscriptionPlanId")]
        public int? SubscriptionPlanId { get; set; }

        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = "Free";

        [JsonPropertyName("planPrice")]
        public decimal PlanPrice { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Active";

        [JsonPropertyName("subscriptionStart")]
        public DateTime? SubscriptionStart { get; set; }

        [JsonPropertyName("subscriptionEnd")]
        public DateTime? SubscriptionEnd { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("totalUsers")]
        public int TotalUsers { get; set; }

        [JsonPropertyName("totalBookings")]
        public int TotalBookings { get; set; }

        [JsonPropertyName("totalStudios")]
        public int TotalStudios { get; set; }

        [JsonPropertyName("users")]
        public List<OrgUserSummaryDto> Users { get; set; } = new();

        [JsonPropertyName("invoices")]
        public List<OrgInvoiceSummaryDto> Invoices { get; set; } = new();

        [JsonPropertyName("tandCAcknowledgments")]
        public List<OrgTandCAcknowledgmentDto> TandCAcknowledgments { get; set; } = new();
    }

    public class OrgUserSummaryDto
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("lastLoginAt")]
        public DateTime? LastLoginAt { get; set; }
    }

    public class OrgInvoiceSummaryDto
    {
        [JsonPropertyName("invoiceId")]
        public int InvoiceId { get; set; }

        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("issuedAt")]
        public DateTime IssuedAt { get; set; }

        [JsonPropertyName("paidAt")]
        public DateTime? PaidAt { get; set; }
    }

    public class OrgTandCAcknowledgmentDto
    {
        [JsonPropertyName("tandCTitle")]
        public string TandCTitle { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("acknowledgedByEmail")]
        public string AcknowledgedByEmail { get; set; } = string.Empty;

        [JsonPropertyName("acknowledgedAt")]
        public DateTime AcknowledgedAt { get; set; }
    }

    public class OrganizationCreateDto
    {
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("ownerFirstName")]
        public string OwnerFirstName { get; set; } = string.Empty;

        [JsonPropertyName("ownerLastName")]
        public string OwnerLastName { get; set; } = string.Empty;

        [JsonPropertyName("ownerEmail")]
        public string OwnerEmail { get; set; } = string.Empty;

        [JsonPropertyName("contactNumber")]
        public string? ContactNumber { get; set; }

        [JsonPropertyName("subdomain")]
        public string Subdomain { get; set; } = string.Empty;

        [JsonPropertyName("subscriptionPlanId")]
        public int? SubscriptionPlanId { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; } = "Asia/Manila";

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;
    }

    public class OrganizationUpdateDto
    {
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("ownerFirstName")]
        public string? OwnerFirstName { get; set; }

        [JsonPropertyName("ownerLastName")]
        public string? OwnerLastName { get; set; }

        [JsonPropertyName("ownerEmail")]
        public string? OwnerEmail { get; set; }

        [JsonPropertyName("contactNumber")]
        public string? ContactNumber { get; set; }

        [JsonPropertyName("subdomain")]
        public string? Subdomain { get; set; }

        [JsonPropertyName("subscriptionPlanId")]
        public int? SubscriptionPlanId { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; } = "Asia/Manila";

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;
    }

    // ==================== SUBSCRIPTIONS ====================

    public class SubscriptionPlanDto
    {
        [JsonPropertyName("subscriptionPlanId")]
        public int SubscriptionPlanId { get; set; }

        [JsonPropertyName("planCode")]
        public string PlanCode { get; set; } = string.Empty;

        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("billingCycle")]
        public string BillingCycle { get; set; } = "Monthly";

        [JsonPropertyName("maxUsers")]
        public int MaxUsers { get; set; }

        [JsonPropertyName("maxBookingsPerMonth")]
        public int MaxBookingsPerMonth { get; set; }

        [JsonPropertyName("maxStorageMb")]
        public int MaxStorageMb { get; set; }

        [JsonPropertyName("features")]
        public string Features { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("activeSubscribersCount")]
        public int ActiveSubscribersCount { get; set; }
    }

    public class SubscriptionPlanCreateUpdateDto
    {
        [JsonPropertyName("planCode")]
        public string PlanCode { get; set; } = string.Empty;

        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("billingCycle")]
        public string BillingCycle { get; set; } = "Monthly";

        [JsonPropertyName("maxUsers")]
        public int MaxUsers { get; set; } = 5;

        [JsonPropertyName("maxBookingsPerMonth")]
        public int MaxBookingsPerMonth { get; set; } = 100;

        [JsonPropertyName("maxStorageMb")]
        public int MaxStorageMb { get; set; } = 1024;

        [JsonPropertyName("features")]
        public string Features { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;
    }

    public class AssignSubscriptionDto
    {
        [JsonPropertyName("companyId")]
        public int CompanyId { get; set; }

        [JsonPropertyName("subscriptionPlanId")]
        public int SubscriptionPlanId { get; set; }

        [JsonPropertyName("durationMonths")]
        public int DurationMonths { get; set; } = 1;

        [JsonPropertyName("autoRenew")]
        public bool AutoRenew { get; set; } = true;

        [JsonPropertyName("generateInvoice")]
        public bool GenerateInvoice { get; set; } = true;
    }

    public class SubscriptionListItemDto
    {
        [JsonPropertyName("subscriptionId")]
        public int SubscriptionId { get; set; }

        [JsonPropertyName("companyId")]
        public int CompanyId { get; set; }

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("companyCode")]
        public string CompanyCode { get; set; } = string.Empty;

        [JsonPropertyName("planName")]
        public string PlanName { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Active";

        [JsonPropertyName("startedAt")]
        public DateTime StartedAt { get; set; }

        [JsonPropertyName("expiresAt")]
        public DateTime ExpiresAt { get; set; }

        [JsonPropertyName("autoRenew")]
        public bool AutoRenew { get; set; }
    }

    public class SubscriptionInvoiceDto
    {
        [JsonPropertyName("subscriptionInvoiceId")]
        public int SubscriptionInvoiceId { get; set; }

        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; } = string.Empty;

        [JsonPropertyName("companyId")]
        public int CompanyId { get; set; }

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Paid";

        [JsonPropertyName("issuedAt")]
        public DateTime IssuedAt { get; set; }

        [JsonPropertyName("dueAt")]
        public DateTime DueAt { get; set; }

        [JsonPropertyName("paidAt")]
        public DateTime? PaidAt { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string? PaymentMethod { get; set; }
    }

    // ==================== PLATFORM TERMS & CONDITIONS ====================

    public class PlatformTandCDto
    {
        [JsonPropertyName("platformTandCId")]
        public int PlatformTandCId { get; set; }

        [JsonPropertyName("tandCCode")]
        public string TandCCode { get; set; } = string.Empty;

        [JsonPropertyName("tandCType")]
        public string TandCType { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("publishedAt")]
        public DateTime? PublishedAt { get; set; }

        [JsonPropertyName("authorName")]
        public string? AuthorName { get; set; }

        [JsonPropertyName("changeNotes")]
        public string? ChangeNotes { get; set; }

        [JsonPropertyName("requiresReAcceptance")]
        public bool RequiresReAcceptance { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("acknowledgedCount")]
        public int AcknowledgedCount { get; set; }
    }

    public class PlatformTandCCreateDto
    {
        [JsonPropertyName("tandCType")]
        public string TandCType { get; set; } = "PlatformTerms";

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("changeNotes")]
        public string? ChangeNotes { get; set; }

        [JsonPropertyName("requiresReAcceptance")]
        public bool RequiresReAcceptance { get; set; } = false;
    }

    public class PlatformTandCUpdateDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("changeNotes")]
        public string? ChangeNotes { get; set; }

        [JsonPropertyName("requiresReAcceptance")]
        public bool RequiresReAcceptance { get; set; }
    }
}
