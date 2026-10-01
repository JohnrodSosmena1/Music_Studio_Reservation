using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class ReportService
    {
        private readonly ApiClient _api;

        public ReportService(ApiClient api)
        {
            _api = api;
        }

        // ==================== BOOKING REPORT ====================

        public async Task<BookingReportDto?> GetBookingReportAsync(int companyId, DateTime from, DateTime to)
        {
            try
            {
                var url = $"tenant/{companyId}/reports/booking" +
                          $"?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}";
                return await _api.GetAsync<BookingReportDto>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReportService.GetBookingReport] {ex.Message}");
                return null;
            }
        }

        // ==================== REVENUE REPORT ====================

        public async Task<RevenueReportDto?> GetRevenueReportAsync(int companyId, DateTime from, DateTime to)
        {
            try
            {
                var url = $"tenant/{companyId}/reports/revenue" +
                          $"?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}";
                return await _api.GetAsync<RevenueReportDto>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReportService.GetRevenueReport] {ex.Message}");
                return null;
            }
        }

        // ==================== CRM ANALYTICS ====================

        public async Task<CrmAnalyticsDto?> GetCrmAnalyticsAsync(int companyId, DateTime from, DateTime to)
        {
            try
            {
                var url = $"tenant/{companyId}/reports/crm-analytics" +
                          $"?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}";
                return await _api.GetAsync<CrmAnalyticsDto>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReportService.GetCrmAnalytics] {ex.Message}");
                return null;
            }
        }

        // ==================== CSV EXPORT HELPERS ====================

        /// <summary>
        /// Exports the Booking Report's details to a CSV file.
        /// Returns true if file was written successfully.
        /// </summary>
        public static bool ExportBookingReportToCsv(BookingReportDto report, string filePath)
        {
            try
            {
                var sb = new StringBuilder();

                // Header
                sb.AppendLine("BookingId,BookingCode,CustomerName,StudioName,StartTime,TotalAmount,Status");

                foreach (var d in report.Details)
                {
                    sb.AppendLine(string.Join(",",
                        d.BookingId.ToString(),
                        CsvEscape(d.BookingCode),
                        CsvEscape(d.CustomerName),
                        CsvEscape(d.StudioName),
                        d.StartTime.ToString("yyyy-MM-dd HH:mm"),
                        d.TotalAmount.ToString("0.00"),
                        CsvEscape(d.BookingStatus)));
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReportService.ExportBookingReport] {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Exports the Revenue Report's daily + studio + top customers to a CSV file.
        /// </summary>
        public static bool ExportRevenueReportToCsv(RevenueReportDto report, string filePath)
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("=== DAILY REVENUE ===");
                sb.AppendLine("Date,Revenue,BookingCount,AverageAmount");
                foreach (var d in report.DailyRevenue)
                {
                    sb.AppendLine(string.Join(",",
                        CsvEscape(d.Date),
                        d.Revenue.ToString("0.00"),
                        d.BookingCount.ToString(),
                        d.AverageAmount.ToString("0.00")));
                }

                sb.AppendLine();
                sb.AppendLine("=== REVENUE BY STUDIO ===");
                sb.AppendLine("StudioName,Revenue,BookingCount,SharePercent");
                foreach (var s in report.ByStudio)
                {
                    sb.AppendLine(string.Join(",",
                        CsvEscape(s.StudioName),
                        s.Revenue.ToString("0.00"),
                        s.BookingCount.ToString(),
                        s.RevenueSharePercent.ToString("0.0")));
                }

                sb.AppendLine();
                sb.AppendLine("=== TOP CUSTOMERS ===");
                sb.AppendLine("CustomerId,CustomerName,TotalSpent,BookingCount");
                foreach (var c in report.TopCustomers)
                {
                    sb.AppendLine(string.Join(",",
                        c.CustomerId.ToString(),
                        CsvEscape(c.CustomerName),
                        c.TotalSpent.ToString("0.00"),
                        c.BookingCount.ToString()));
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReportService.ExportRevenueReport] {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Exports the CRM Analytics report to CSV.
        /// </summary>
        public static bool ExportCrmAnalyticsToCsv(CrmAnalyticsDto report, string filePath)
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("=== CRM LIFECYCLE & RETENTION KPIS ===");
                sb.AppendLine("TotalCustomers,ActiveInPeriod,NewCustomers,ReturningCustomers,RetentionRate,ChurnRate,AverageCLV,RepeatBookingRate");
                sb.AppendLine(string.Join(",",
                    report.TotalCustomers,
                    report.ActiveCustomersInPeriod,
                    report.NewCustomers,
                    report.ReturningCustomers,
                    $"{report.RetentionRate:0.0}%",
                    $"{report.ChurnRate:0.0}%",
                    report.AverageCLV.ToString("0.00"),
                    $"{report.RepeatBookingRate:0.0}%"));

                sb.AppendLine();
                sb.AppendLine("=== PARETO 80/20 ATTRIBUTION ===");
                sb.AppendLine("Top20CustomerCount,Top20Revenue,RevenueSharePercent,TotalPeriodRevenue");
                sb.AppendLine(string.Join(",",
                    report.Top20PercentCustomerCount,
                    report.Top20PercentRevenue.ToString("0.00"),
                    $"{report.Top20PercentRevenueShare:0.0}%",
                    report.TotalPeriodRevenue.ToString("0.00")));

                sb.AppendLine();
                sb.AppendLine("=== RFM SEGMENTS ===");
                sb.AppendLine("SegmentName,CustomerCount,Percentage,TotalRevenue,AverageSpend");
                foreach (var seg in report.RfmSegments)
                {
                    sb.AppendLine(string.Join(",",
                        CsvEscape(seg.SegmentName),
                        seg.CustomerCount,
                        $"{seg.Percentage:0.0}%",
                        seg.TotalRevenue.ToString("0.00"),
                        seg.AverageSpend.ToString("0.00")));
                }

                sb.AppendLine();
                sb.AppendLine("=== CUSTOMER RFM DETAILS ===");
                sb.AppendLine("CustomerId,CustomerCode,CustomerName,TotalBookings,LifetimeSpend,LastBookingDate,RecencyDays,Segment,IsActive");
                foreach (var c in report.CustomerDetails)
                {
                    sb.AppendLine(string.Join(",",
                        c.CustomerId,
                        CsvEscape(c.CustomerCode),
                        CsvEscape(c.CustomerName),
                        c.TotalBookings,
                        c.LifetimeSpend.ToString("0.00"),
                        c.LastBookingDate?.ToString("yyyy-MM-dd") ?? "Never",
                        c.RecencyDays == 999 ? "N/A" : c.RecencyDays.ToString(),
                        CsvEscape(c.RfmSegment),
                        c.IsActive ? "Active" : "Inactive"));
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReportService.ExportCrmAnalytics] {ex.Message}");
                return false;
            }
        }

        /// <summary>Escapes a CSV field: quotes if it contains comma, quote, or newline.</summary>
        private static string CsvEscape(string? value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}