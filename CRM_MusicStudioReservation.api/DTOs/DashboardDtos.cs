using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.api.DTOs
{
    // ==================== ADMIN DASHBOARD RESPONSE ====================

    public class AdminDashboardResponse
    {
        // Top-level KPIs
        public int TotalBookings { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveStudios { get; set; }

        // KPI row #2 (5 stat cards)
        public int TodayBookings { get; set; }
        public int PendingBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int CancelledBookings { get; set; }

        // Trend percentages vs last week
        public decimal TotalBookingsTrend { get; set; }
        public decimal TodayBookingsTrend { get; set; }
        public decimal PendingBookingsTrend { get; set; }
        public decimal CompletedBookingsTrend { get; set; }
        public decimal CancelledBookingsTrend { get; set; }

        // Charts
        public List<BookingsOverviewItem> BookingsOverview { get; set; } = new();
        public List<BookingsByStatusDayItem> BookingsByStatusByDay { get; set; } = new();
        public List<BookingsByStudioItem> BookingsByStudio { get; set; } = new();

        // Booking status breakdown (for donut center total)
        public int BookingStatusCompleted { get; set; }
        public int BookingStatusPending { get; set; }
        public int BookingStatusCancelled { get; set; }
        public int BookingStatusOnGoing { get; set; }

        // Studio utilization (per-studio load %)
        public List<StudioUtilizationItem> StudioUtilization { get; set; } = new();

        // Tables
        public List<RecentBookingItem> RecentBookings { get; set; } = new();
        public List<UpcomingBookingItem> UpcomingBookings { get; set; } = new();

        // Activity feed (stubbed for Phase 1)
        public List<AuditLogFeedItem> RecentAuditLogs { get; set; } = new();
    }

    // ==================== CHART DTOs ====================

    public class BookingsOverviewItem
    {
        public string Date { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class BookingsByStatusDayItem
    {
        public string Date { get; set; } = string.Empty;
        public int Completed { get; set; }
        public int Pending { get; set; }
        public int Cancelled { get; set; }
    }

    public class BookingsByStudioItem
    {
        public string Studio { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class StudioUtilizationItem
    {
        public string StudioName { get; set; } = string.Empty;
        public int UtilizationPercent { get; set; }
        public int BookingsThisWeek { get; set; }
    }

    // ==================== TABLE DTOs ====================

    public class RecentBookingItem
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int StudioId { get; set; }
        public string StudioName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class UpcomingBookingItem
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string StudioName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string BookingStatus { get; set; } = string.Empty;
    }

    // ==================== ACTIVITY FEED DTOs ====================

    public class AuditLogFeedItem
    {
        public int AuditLogId { get; set; }
        public string? UserEmail { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}