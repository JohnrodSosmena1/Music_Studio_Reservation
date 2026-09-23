using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== ADMIN DASHBOARD ====================

    public class AdminDashboardDto
    {
        // -------- Top-level KPIs --------
        [JsonPropertyName("totalBookings")]
        public int TotalBookings { get; set; }

        [JsonPropertyName("totalCustomers")]
        public int TotalCustomers { get; set; }

        [JsonPropertyName("totalRevenue")]
        public decimal TotalRevenue { get; set; }

        [JsonPropertyName("activeStudios")]
        public int ActiveStudios { get; set; }

        // -------- Second-row KPIs --------
        [JsonPropertyName("todayBookings")]
        public int TodayBookings { get; set; }

        [JsonPropertyName("pendingBookings")]
        public int PendingBookings { get; set; }

        [JsonPropertyName("completedBookings")]
        public int CompletedBookings { get; set; }

        [JsonPropertyName("cancelledBookings")]
        public int CancelledBookings { get; set; }

        // -------- Trend % vs last week --------
        [JsonPropertyName("totalBookingsTrend")]
        public decimal TotalBookingsTrend { get; set; }

        [JsonPropertyName("todayBookingsTrend")]
        public decimal TodayBookingsTrend { get; set; }

        [JsonPropertyName("pendingBookingsTrend")]
        public decimal PendingBookingsTrend { get; set; }

        [JsonPropertyName("completedBookingsTrend")]
        public decimal CompletedBookingsTrend { get; set; }

        [JsonPropertyName("cancelledBookingsTrend")]
        public decimal CancelledBookingsTrend { get; set; }

        // -------- Charts --------
        [JsonPropertyName("bookingsOverview")]
        public List<BookingsOverviewItem> BookingsOverview { get; set; } = new();

        [JsonPropertyName("bookingsByStatusByDay")]
        public List<BookingsByStatusDayItem> BookingsByStatusByDay { get; set; } = new();

        [JsonPropertyName("bookingsByStudio")]
        public List<BookingsByStudioItem> BookingsByStudio { get; set; } = new();

        // -------- Donut center totals --------
        [JsonPropertyName("bookingStatusCompleted")]
        public int BookingStatusCompleted { get; set; }

        [JsonPropertyName("bookingStatusPending")]
        public int BookingStatusPending { get; set; }

        [JsonPropertyName("bookingStatusCancelled")]
        public int BookingStatusCancelled { get; set; }

        [JsonPropertyName("bookingStatusOnGoing")]
        public int BookingStatusOnGoing { get; set; }

        // -------- Studio utilization --------
        [JsonPropertyName("studioUtilization")]
        public List<StudioUtilizationItem> StudioUtilization { get; set; } = new();

        // -------- Tables --------
        [JsonPropertyName("recentBookings")]
        public List<RecentBookingItem> RecentBookings { get; set; } = new();

        [JsonPropertyName("upcomingBookings")]
        public List<UpcomingBookingItem> UpcomingBookings { get; set; } = new();

        // -------- Activity feed --------
        [JsonPropertyName("recentAuditLogs")]
        public List<AuditLogFeedItem> RecentAuditLogs { get; set; } = new();
    }

    // ==================== RECENT BOOKINGS ====================

    public class RecentBookingItem
    {
        [JsonPropertyName("bookingId")]
        public int BookingId { get; set; }

        [JsonPropertyName("bookingCode")]
        public string BookingCode { get; set; } = string.Empty;

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("studioId")]
        public int StudioId { get; set; }

        [JsonPropertyName("studioName")]
        public string StudioName { get; set; } = string.Empty;

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("bookingStatus")]
        public string BookingStatus { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    // ==================== UPCOMING BOOKINGS ====================

    public class UpcomingBookingItem
    {
        [JsonPropertyName("bookingId")]
        public int BookingId { get; set; }

        [JsonPropertyName("bookingCode")]
        public string BookingCode { get; set; } = string.Empty;

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("studioName")]
        public string StudioName { get; set; } = string.Empty;

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("bookingStatus")]
        public string BookingStatus { get; set; } = string.Empty;
    }

    // ==================== CHARTS ====================

    public class BookingsOverviewItem
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class BookingsByStatusDayItem
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("completed")]
        public int Completed { get; set; }

        [JsonPropertyName("pending")]
        public int Pending { get; set; }

        [JsonPropertyName("cancelled")]
        public int Cancelled { get; set; }
    }

    public class BookingsByStudioItem
    {
        [JsonPropertyName("studio")]
        public string Studio { get; set; } = string.Empty;

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class StudioUtilizationItem
    {
        [JsonPropertyName("studioName")]
        public string StudioName { get; set; } = string.Empty;

        [JsonPropertyName("utilizationPercent")]
        public int UtilizationPercent { get; set; }

        [JsonPropertyName("bookingsThisWeek")]
        public int BookingsThisWeek { get; set; }
    }

    // ==================== ACTIVITY FEED ====================

    public class AuditLogFeedItem
    {
        [JsonPropertyName("auditLogId")]
        public int AuditLogId { get; set; }

        [JsonPropertyName("userEmail")]
        public string? UserEmail { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;

        [JsonPropertyName("entityName")]
        public string EntityName { get; set; } = string.Empty;

        [JsonPropertyName("entityId")]
        public int? EntityId { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    // ==================== STAFF DASHBOARD ====================

    public class StaffDashboardDto
    {
        [JsonPropertyName("todaysBookings")]
        public int TodaysBookings { get; set; }

        [JsonPropertyName("checkedInCount")]
        public int CheckedInCount { get; set; }

        [JsonPropertyName("pendingBookings")]
        public int PendingBookings { get; set; }

        [JsonPropertyName("availableStudios")]
        public int AvailableStudios { get; set; }

        [JsonPropertyName("schedule")]
        public List<ScheduleItem> Schedule { get; set; } = new();
    }

    public class ScheduleItem
    {
        [JsonPropertyName("bookingId")]
        public int BookingId { get; set; }

        [JsonPropertyName("bookingCode")]
        public string BookingCode { get; set; } = string.Empty;

        [JsonPropertyName("studioId")]
        public int StudioId { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("checkedIn")]
        public bool CheckedIn { get; set; }

        [JsonPropertyName("checkedOut")]
        public bool CheckedOut { get; set; }
    }

    // ==================== CLIENT DASHBOARD ====================

    public class ClientDashboardDto
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("upcomingBookings")]
        public List<ClientBookingItem> UpcomingBookings { get; set; } = new();

        [JsonPropertyName("totalBookings")]
        public int TotalBookings { get; set; }

        [JsonPropertyName("loyaltyPoints")]
        public int LoyaltyPoints { get; set; }

        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
    }

    public class ClientBookingItem
    {
        [JsonPropertyName("bookingId")]
        public int BookingId { get; set; }

        [JsonPropertyName("bookingCode")]
        public string BookingCode { get; set; } = string.Empty;

        [JsonPropertyName("studioId")]
        public int StudioId { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}