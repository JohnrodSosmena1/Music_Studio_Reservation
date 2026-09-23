using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CRM.winforms.DTOs
{
    // ==================== BOOKING REPORT ====================

    public class BookingReportDto
    {
        [JsonPropertyName("from")]
        public DateTime From { get; set; }

        [JsonPropertyName("to")]
        public DateTime To { get; set; }

        [JsonPropertyName("totalBookings")]
        public int TotalBookings { get; set; }

        [JsonPropertyName("completedBookings")]
        public int CompletedBookings { get; set; }

        [JsonPropertyName("pendingBookings")]
        public int PendingBookings { get; set; }

        [JsonPropertyName("cancelledBookings")]
        public int CancelledBookings { get; set; }

        [JsonPropertyName("totalRevenue")]
        public decimal TotalRevenue { get; set; }

        [JsonPropertyName("averageAmount")]
        public decimal AverageAmount { get; set; }

        [JsonPropertyName("dailyBreakdown")]
        public List<BookingReportDailyItem> DailyBreakdown { get; set; } = new();

        [JsonPropertyName("byStudio")]
        public List<BookingReportStudioItem> ByStudio { get; set; } = new();

        [JsonPropertyName("details")]
        public List<BookingReportDetailItem> Details { get; set; } = new();
    }

    public class BookingReportDailyItem
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("completed")]
        public int Completed { get; set; }

        [JsonPropertyName("pending")]
        public int Pending { get; set; }

        [JsonPropertyName("cancelled")]
        public int Cancelled { get; set; }

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }
    }

    public class BookingReportStudioItem
    {
        [JsonPropertyName("studioName")]
        public string StudioName { get; set; } = string.Empty;

        [JsonPropertyName("bookingCount")]
        public int BookingCount { get; set; }

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }
    }

    public class BookingReportDetailItem
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

        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("bookingStatus")]
        public string BookingStatus { get; set; } = string.Empty;
    }

    // ==================== REVENUE REPORT ====================

    public class RevenueReportDto
    {
        [JsonPropertyName("from")]
        public DateTime From { get; set; }

        [JsonPropertyName("to")]
        public DateTime To { get; set; }

        [JsonPropertyName("totalRevenue")]
        public decimal TotalRevenue { get; set; }

        [JsonPropertyName("averageBookingValue")]
        public decimal AverageBookingValue { get; set; }

        [JsonPropertyName("paidBookings")]
        public int PaidBookings { get; set; }

        [JsonPropertyName("highestBooking")]
        public decimal HighestBooking { get; set; }

        [JsonPropertyName("lowestBooking")]
        public decimal LowestBooking { get; set; }

        [JsonPropertyName("dailyRevenue")]
        public List<RevenueReportDailyItem> DailyRevenue { get; set; } = new();

        [JsonPropertyName("byStudio")]
        public List<RevenueReportStudioItem> ByStudio { get; set; } = new();

        [JsonPropertyName("topCustomers")]
        public List<RevenueReportCustomerItem> TopCustomers { get; set; } = new();
    }

    public class RevenueReportDailyItem
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }

        [JsonPropertyName("bookingCount")]
        public int BookingCount { get; set; }

        [JsonPropertyName("averageAmount")]
        public decimal AverageAmount { get; set; }
    }

    public class RevenueReportStudioItem
    {
        [JsonPropertyName("studioName")]
        public string StudioName { get; set; } = string.Empty;

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }

        [JsonPropertyName("bookingCount")]
        public int BookingCount { get; set; }

        [JsonPropertyName("revenueSharePercent")]
        public decimal RevenueSharePercent { get; set; }
    }

    public class RevenueReportCustomerItem
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("totalSpent")]
        public decimal TotalSpent { get; set; }

        [JsonPropertyName("bookingCount")]
        public int BookingCount { get; set; }
    }
}