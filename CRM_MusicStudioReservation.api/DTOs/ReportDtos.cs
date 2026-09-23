using System;
using System.Collections.Generic;

namespace CRM_MusicStudioReservation.api.DTOs
{
    // ==================== BOOKING REPORT ====================

    public class BookingReportResponse
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        // Summary
        public int TotalBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int PendingBookings { get; set; }
        public int CancelledBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageAmount { get; set; }

        // Data
        public List<BookingReportDailyItem> DailyBreakdown { get; set; } = new();
        public List<BookingReportStudioItem> ByStudio { get; set; } = new();
        public List<BookingReportDetailItem> Details { get; set; } = new();
    }

    public class BookingReportDailyItem
    {
        public string Date { get; set; } = string.Empty;
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Pending { get; set; }
        public int Cancelled { get; set; }
        public decimal Revenue { get; set; }
    }

    public class BookingReportStudioItem
    {
        public string StudioName { get; set; } = string.Empty;
        public int BookingCount { get; set; }
        public decimal Revenue { get; set; }
    }

    public class BookingReportDetailItem
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string StudioName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set; } = string.Empty;
    }

    // ==================== REVENUE REPORT ====================

    public class RevenueReportResponse
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        // Summary
        public decimal TotalRevenue { get; set; }
        public decimal AverageBookingValue { get; set; }
        public int PaidBookings { get; set; }
        public decimal HighestBooking { get; set; }
        public decimal LowestBooking { get; set; }

        // Data
        public List<RevenueReportDailyItem> DailyRevenue { get; set; } = new();
        public List<RevenueReportStudioItem> ByStudio { get; set; } = new();
        public List<RevenueReportCustomerItem> TopCustomers { get; set; } = new();
    }

    public class RevenueReportDailyItem
    {
        public string Date { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int BookingCount { get; set; }
        public decimal AverageAmount { get; set; }
    }

    public class RevenueReportStudioItem
    {
        public string StudioName { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int BookingCount { get; set; }
        public decimal RevenueSharePercent { get; set; }
    }

    public class RevenueReportCustomerItem
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
        public int BookingCount { get; set; }
    }
}