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

    // ==================== CRM ANALYTICS REPORT ====================

    public class CrmAnalyticsDto
    {
        [JsonPropertyName("from")]
        public DateTime From { get; set; }

        [JsonPropertyName("to")]
        public DateTime To { get; set; }

        [JsonPropertyName("totalCustomers")]
        public int TotalCustomers { get; set; }

        [JsonPropertyName("activeCustomersInPeriod")]
        public int ActiveCustomersInPeriod { get; set; }

        [JsonPropertyName("newCustomers")]
        public int NewCustomers { get; set; }

        [JsonPropertyName("returningCustomers")]
        public int ReturningCustomers { get; set; }

        [JsonPropertyName("retentionRate")]
        public decimal RetentionRate { get; set; }

        [JsonPropertyName("churnRate")]
        public decimal ChurnRate { get; set; }

        [JsonPropertyName("averageCLV")]
        public decimal AverageCLV { get; set; }

        [JsonPropertyName("repeatBookingRate")]
        public decimal RepeatBookingRate { get; set; }

        [JsonPropertyName("top20PercentCustomerCount")]
        public int Top20PercentCustomerCount { get; set; }

        [JsonPropertyName("top20PercentRevenue")]
        public decimal Top20PercentRevenue { get; set; }

        [JsonPropertyName("top20PercentRevenueShare")]
        public decimal Top20PercentRevenueShare { get; set; }

        [JsonPropertyName("totalPeriodRevenue")]
        public decimal TotalPeriodRevenue { get; set; }

        [JsonPropertyName("averageSpendPerVisit")]
        public decimal AverageSpendPerVisit { get; set; }

        [JsonPropertyName("peakHours")]
        public List<PeakHourDto> PeakHours { get; set; } = new();

        [JsonPropertyName("studioPreferences")]
        public List<BookingReportStudioItem> StudioPreferences { get; set; } = new();

        [JsonPropertyName("rfmSegments")]
        public List<RfmSegmentDto> RfmSegments { get; set; } = new();

        [JsonPropertyName("customerDetails")]
        public List<CustomerRfmDto> CustomerDetails { get; set; } = new();
    }

    public class PeakHourDto
    {
        [JsonPropertyName("hour")]
        public int Hour { get; set; }

        [JsonPropertyName("timeLabel")]
        public string TimeLabel { get; set; } = string.Empty;

        [JsonPropertyName("bookingCount")]
        public int BookingCount { get; set; }
    }

    public class RfmSegmentDto
    {
        [JsonPropertyName("segmentName")]
        public string SegmentName { get; set; } = string.Empty;

        [JsonPropertyName("customerCount")]
        public int CustomerCount { get; set; }

        [JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }

        [JsonPropertyName("totalRevenue")]
        public decimal TotalRevenue { get; set; }

        [JsonPropertyName("averageSpend")]
        public decimal AverageSpend { get; set; }
    }

    public class CustomerRfmDto
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customerCode")]
        public string CustomerCode { get; set; } = string.Empty;

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("totalBookings")]
        public int TotalBookings { get; set; }

        [JsonPropertyName("lifetimeSpend")]
        public decimal LifetimeSpend { get; set; }

        [JsonPropertyName("lastBookingDate")]
        public DateTime? LastBookingDate { get; set; }

        [JsonPropertyName("recencyDays")]
        public int RecencyDays { get; set; }

        [JsonPropertyName("rfmSegment")]
        public string RfmSegment { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }
}