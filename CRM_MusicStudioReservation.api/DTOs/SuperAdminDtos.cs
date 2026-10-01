using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    // ==================== DASHBOARD DTOs ====================

    public class SuperAdminDashboardDto
    {
        public int TotalOrganizations { get; set; }
        public int ActiveOrganizations { get; set; }
        public int SuspendedOrganizations { get; set; }
        public int InactiveOrganizations { get; set; }
        public int TrialOrganizations { get; set; }
        public int TotalPlatformUsers { get; set; }
        public int ActiveSubscriptions { get; set; }
        public decimal MonthlyRecurringRevenue { get; set; }
        public int ExpiringSoonCount { get; set; } // Expiring in next 7 days

        public List<MonthlyGrowthItemDto> TenantGrowth { get; set; } = new();
        public List<StatusCountItemDto> StatusDistribution { get; set; } = new();
        public List<RecentActivityItemDto> RecentActivities { get; set; } = new();
    }

    public class MonthlyGrowthItemDto
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class StatusCountItemDto
    {
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
        public string Color { get; set; } = "#8B5CF6";
    }

    public class RecentActivityItemDto
    {
        public string TimeAgo { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    // ==================== ORGANIZATION DTOs ====================

    public class OrganizationListItemDto
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string? OwnerName { get; set; }
        public string? OwnerEmail { get; set; }
        public string? Subdomain { get; set; }
        public string PlanName { get; set; } = "Free";
        public string Status { get; set; } = "Active";
        public DateTime? SubscriptionStart { get; set; }
        public DateTime? SubscriptionEnd { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class OrganizationDetailDto
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string? Subdomain { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerLastName { get; set; }
        public string? OwnerEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string TimeZone { get; set; } = "Asia/Manila";
        public int? SubscriptionPlanId { get; set; }
        public string PlanName { get; set; } = "Free";
        public decimal PlanPrice { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime? SubscriptionStart { get; set; }
        public DateTime? SubscriptionEnd { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public int TotalUsers { get; set; }
        public int TotalBookings { get; set; }
        public int TotalStudios { get; set; }

        public List<OrgUserSummaryDto> Users { get; set; } = new();
        public List<OrgInvoiceSummaryDto> Invoices { get; set; } = new();
        public List<OrgTandCAcknowledgmentDto> TandCAcknowledgments { get; set; } = new();
    }

    public class OrgUserSummaryDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

    public class OrgInvoiceSummaryDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public DateTime? PaidAt { get; set; }
    }

    public class OrgTandCAcknowledgmentDto
    {
        public string TandCTitle { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string AcknowledgedByEmail { get; set; } = string.Empty;
        public DateTime AcknowledgedAt { get; set; }
    }

    public class OrganizationCreateDto
    {
        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string OwnerFirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string OwnerLastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string OwnerEmail { get; set; } = string.Empty;

        public string? ContactNumber { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Subdomain can only contain lowercase letters, numbers, and hyphens.")]
        public string Subdomain { get; set; } = string.Empty;

        public int? SubscriptionPlanId { get; set; }

        public string TimeZone { get; set; } = "Asia/Manila";

        public bool IsActive { get; set; } = true;
    }

    public class OrganizationUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; } = string.Empty;

        public string? OwnerFirstName { get; set; }
        public string? OwnerLastName { get; set; }
        public string? OwnerEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? Subdomain { get; set; }
        public int? SubscriptionPlanId { get; set; }
        public string? Status { get; set; }
        public string TimeZone { get; set; } = "Asia/Manila";
        public bool IsActive { get; set; } = true;
    }

    public class HardDeleteOrganizationDto
    {
        [Required]
        public string ConfirmationPhrase { get; set; } = string.Empty; // Must match "DELETE [COMPANY_CODE]"
    }

    // ==================== SUBSCRIPTION DTOs ====================

    public class SubscriptionPlanDto
    {
        public int SubscriptionPlanId { get; set; }
        public string PlanCode { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string BillingCycle { get; set; } = "Monthly";
        public int MaxUsers { get; set; }
        public int MaxBookingsPerMonth { get; set; }
        public int MaxStorageMb { get; set; }
        public string Features { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int ActiveSubscribersCount { get; set; }
    }

    public class SubscriptionPlanCreateUpdateDto
    {
        [Required]
        public string PlanCode { get; set; } = string.Empty;

        [Required]
        public string PlanName { get; set; } = string.Empty;

        [Range(0, 1000000)]
        public decimal Price { get; set; }

        public string BillingCycle { get; set; } = "Monthly";
        public int MaxUsers { get; set; } = 5;
        public int MaxBookingsPerMonth { get; set; } = 100;
        public int MaxStorageMb { get; set; } = 1024;
        public string Features { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class AssignSubscriptionDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int SubscriptionPlanId { get; set; }

        public int DurationMonths { get; set; } = 1;
        public bool AutoRenew { get; set; } = true;
        public bool GenerateInvoice { get; set; } = true;
    }

    public class SubscriptionInvoiceDto
    {
        public int SubscriptionInvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Paid";
        public DateTime IssuedAt { get; set; }
        public DateTime DueAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public string? PaymentMethod { get; set; }
    }

    // ==================== PLATFORM T&C DTOs ====================

    public class PlatformTandCDto
    {
        public int PlatformTandCId { get; set; }
        public string TandCCode { get; set; } = string.Empty;
        public string TandCType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? PublishedAt { get; set; }
        public string? AuthorName { get; set; }
        public string? ChangeNotes { get; set; }
        public bool RequiresReAcceptance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AcknowledgedCount { get; set; }
    }

    public class PlatformTandCCreateDto
    {
        [Required]
        public string TandCType { get; set; } = "PlatformTerms";

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? ChangeNotes { get; set; }
        public bool RequiresReAcceptance { get; set; } = false;
    }

    public class PlatformTandCUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? ChangeNotes { get; set; }
        public bool RequiresReAcceptance { get; set; }
    }
}
