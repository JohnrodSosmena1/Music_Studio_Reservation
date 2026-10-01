using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.enums;
using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class ReportEndpoints
    {
        public static void MapReportEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/reports").RequireAuthorization();

            // ==================== BOOKING REPORT ====================
            group.MapGet("/booking", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                DateTime? from = null,
                DateTime? to = null) =>
            {
                // Default range: last 30 days
                var toDate = (to ?? DateTime.UtcNow).Date.AddDays(1);
                var fromDate = (from ?? toDate.AddDays(-30)).Date;

                if (fromDate >= toDate)
                    return Results.BadRequest("From date must be before To date");

                await using var db = await tenantFactory.CreateAsync(companyId);

                // Fetch all bookings in range
                var bookings = await db.Bookings
                    .AsNoTracking()
                    .Where(b => b.StartTime >= fromDate && b.StartTime < toDate)
                    .ToListAsync();

                if (bookings.Count == 0)
                    return Results.Ok(new BookingReportResponse
                    {
                        From = fromDate,
                        To = toDate.AddDays(-1)
                    });

                // Lookups
                var studioIds = bookings.Select(b => b.StudioId).Distinct().ToList();
                var studios = await db.Studios
                    .Where(s => studioIds.Contains(s.StudioId))
                    .ToDictionaryAsync(s => s.StudioId, s => s.StudioName);

                var customerIds = bookings.Select(b => b.CustomerId).Distinct().ToList();
                var customers = await db.Customers
                    .Where(c => customerIds.Contains(c.CustomerId))
                    .ToDictionaryAsync(c => c.CustomerId, c => c.CustomerName);

                // Summary
                var totalBookings = bookings.Count;
                var completedBookings = bookings.Count(b => b.BookingStatus == BookingStatus.CheckedOut);
                var pendingBookings = bookings.Count(b =>
                    b.BookingStatus == BookingStatus.Confirmed ||
                    b.BookingStatus == BookingStatus.CheckedIn);
                var cancelledBookings = bookings.Count(b => b.BookingStatus == BookingStatus.Cancelled);
                var totalRevenue = bookings
                    .Where(b => b.BookingStatus != BookingStatus.Cancelled)
                    .Sum(b => b.TotalAmount);
                var averageAmount = totalBookings > 0
                    ? Math.Round(totalRevenue / totalBookings, 2)
                    : 0m;

                // Daily breakdown
                var dailyBreakdown = bookings
                    .GroupBy(b => b.StartTime.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new BookingReportDailyItem
                    {
                        Date = g.Key.ToString("MMM d"),
                        Total = g.Count(),
                        Completed = g.Count(b => b.BookingStatus == BookingStatus.CheckedOut),
                        Pending = g.Count(b =>
                            b.BookingStatus == BookingStatus.Confirmed ||
                            b.BookingStatus == BookingStatus.CheckedIn),
                        Cancelled = g.Count(b => b.BookingStatus == BookingStatus.Cancelled),
                        Revenue = g.Where(b => b.BookingStatus != BookingStatus.Cancelled)
                                   .Sum(b => b.TotalAmount)
                    })
                    .ToList();

                // By studio
                var byStudio = bookings
                    .GroupBy(b => b.StudioId)
                    .Select(g => new BookingReportStudioItem
                    {
                        StudioName = studios.GetValueOrDefault(g.Key, $"Studio {g.Key}"),
                        BookingCount = g.Count(),
                        Revenue = g.Where(b => b.BookingStatus != BookingStatus.Cancelled)
                                   .Sum(b => b.TotalAmount)
                    })
                    .OrderByDescending(x => x.BookingCount)
                    .ToList();

                // Details (all bookings in range, newest first)
                var details = bookings
                    .OrderByDescending(b => b.StartTime)
                    .Select(b => new BookingReportDetailItem
                    {
                        BookingId = b.BookingId,
                        BookingCode = b.BookingCode,
                        CustomerName = customers.GetValueOrDefault(b.CustomerId, $"Customer {b.CustomerId}"),
                        StudioName = studios.GetValueOrDefault(b.StudioId, $"Studio {b.StudioId}"),
                        StartTime = b.StartTime,
                        TotalAmount = b.TotalAmount,
                        BookingStatus = b.BookingStatus.ToString()
                    })
                    .ToList();

                return Results.Ok(new BookingReportResponse
                {
                    From = fromDate,
                    To = toDate.AddDays(-1),
                    TotalBookings = totalBookings,
                    CompletedBookings = completedBookings,
                    PendingBookings = pendingBookings,
                    CancelledBookings = cancelledBookings,
                    TotalRevenue = totalRevenue,
                    AverageAmount = averageAmount,
                    DailyBreakdown = dailyBreakdown,
                    ByStudio = byStudio,
                    Details = details
                });
            });

            // ==================== REVENUE REPORT ====================
            group.MapGet("/revenue", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                DateTime? from = null,
                DateTime? to = null) =>
            {
                var toDate = (to ?? DateTime.UtcNow).Date.AddDays(1);
                var fromDate = (from ?? toDate.AddDays(-30)).Date;

                if (fromDate >= toDate)
                    return Results.BadRequest("From date must be before To date");

                await using var db = await tenantFactory.CreateAsync(companyId);

                // Only non-cancelled bookings count as revenue
                var bookings = await db.Bookings
                    .AsNoTracking()
                    .Where(b => b.StartTime >= fromDate && b.StartTime < toDate)
                    .Where(b => b.BookingStatus != BookingStatus.Cancelled)
                    .ToListAsync();

                if (bookings.Count == 0)
                    return Results.Ok(new RevenueReportResponse
                    {
                        From = fromDate,
                        To = toDate.AddDays(-1)
                    });

                // Lookups
                var studioIds = bookings.Select(b => b.StudioId).Distinct().ToList();
                var studios = await db.Studios
                    .Where(s => studioIds.Contains(s.StudioId))
                    .ToDictionaryAsync(s => s.StudioId, s => s.StudioName);

                var customerIds = bookings.Select(b => b.CustomerId).Distinct().ToList();
                var customers = await db.Customers
                    .Where(c => customerIds.Contains(c.CustomerId))
                    .ToDictionaryAsync(c => c.CustomerId, c => c.CustomerName);

                // Summary
                var totalRevenue = bookings.Sum(b => b.TotalAmount);
                var averageBookingValue = bookings.Count > 0
                    ? Math.Round(totalRevenue / bookings.Count, 2)
                    : 0m;
                var highestBooking = bookings.Count > 0 ? bookings.Max(b => b.TotalAmount) : 0m;
                var lowestBooking = bookings.Count > 0 ? bookings.Min(b => b.TotalAmount) : 0m;

                // Daily revenue
                var dailyRevenue = bookings
                    .GroupBy(b => b.StartTime.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new RevenueReportDailyItem
                    {
                        Date = g.Key.ToString("MMM d"),
                        Revenue = g.Sum(b => b.TotalAmount),
                        BookingCount = g.Count(),
                        AverageAmount = g.Count() > 0
                            ? Math.Round(g.Sum(b => b.TotalAmount) / g.Count(), 2)
                            : 0m
                    })
                    .ToList();

                // By studio (with share %)
                var byStudioRaw = bookings
                    .GroupBy(b => b.StudioId)
                    .Select(g => new
                    {
                        StudioId = g.Key,
                        Revenue = g.Sum(b => b.TotalAmount),
                        BookingCount = g.Count()
                    })
                    .OrderByDescending(x => x.Revenue)
                    .ToList();

                var byStudio = byStudioRaw.Select(x => new RevenueReportStudioItem
                {
                    StudioName = studios.GetValueOrDefault(x.StudioId, $"Studio {x.StudioId}"),
                    Revenue = x.Revenue,
                    BookingCount = x.BookingCount,
                    RevenueSharePercent = totalRevenue > 0
                        ? Math.Round((x.Revenue / totalRevenue) * 100m, 1)
                        : 0m
                }).ToList();

                // Top 10 customers by spend
                var topCustomers = bookings
                    .GroupBy(b => b.CustomerId)
                    .Select(g => new RevenueReportCustomerItem
                    {
                        CustomerId = g.Key,
                        CustomerName = customers.GetValueOrDefault(g.Key, $"Customer {g.Key}"),
                        TotalSpent = g.Sum(b => b.TotalAmount),
                        BookingCount = g.Count()
                    })
                    .OrderByDescending(x => x.TotalSpent)
                    .Take(10)
                    .ToList();

                return Results.Ok(new RevenueReportResponse
                {
                    From = fromDate,
                    To = toDate.AddDays(-1),
                    TotalRevenue = totalRevenue,
                    AverageBookingValue = averageBookingValue,
                    PaidBookings = bookings.Count,
                    HighestBooking = highestBooking,
                    LowestBooking = lowestBooking,
                    DailyRevenue = dailyRevenue,
                    ByStudio = byStudio,
                    TopCustomers = topCustomers
                });
            });

            // ==================== CRM ANALYTICS REPORT ====================
            group.MapGet("/crm-analytics", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                DateTime? from = null,
                DateTime? to = null) =>
            {
                var toDate = (to ?? DateTime.UtcNow).Date.AddDays(1);
                var fromDate = (from ?? toDate.AddDays(-30)).Date;

                if (fromDate >= toDate)
                    return Results.BadRequest("From date must be before To date");

                await using var db = await tenantFactory.CreateAsync(companyId);

                var customers = await db.Customers.AsNoTracking().ToListAsync();
                var allBookings = await db.Bookings
                    .AsNoTracking()
                    .Where(b => b.BookingStatus != BookingStatus.Cancelled)
                    .ToListAsync();

                var periodBookings = allBookings
                    .Where(b => b.StartTime >= fromDate && b.StartTime < toDate)
                    .ToList();

                var studios = await db.Studios.AsNoTracking().ToDictionaryAsync(s => s.StudioId, s => s.StudioName);

                var totalCustomers = customers.Count;
                var now = DateTime.UtcNow;

                // Group historical bookings by customer
                var bookingsByCustomer = allBookings
                    .GroupBy(b => b.CustomerId)
                    .ToDictionary(g => g.Key, g => g.OrderBy(b => b.StartTime).ToList());

                // Period bookings by customer
                var periodBookingsByCustomer = periodBookings
                    .GroupBy(b => b.CustomerId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var activeCustomersInPeriod = periodBookingsByCustomer.Keys.Count;

                int newCustomers = 0;
                int returningCustomers = 0;
                int repeatInPeriod = 0;

                foreach (var (custId, pBookings) in periodBookingsByCustomer)
                {
                    if (pBookings.Count >= 2) repeatInPeriod++;

                    var allCustBookings = bookingsByCustomer.GetValueOrDefault(custId);
                    if (allCustBookings != null && allCustBookings.Count > 0)
                    {
                        var firstBooking = allCustBookings.First();
                        if (firstBooking.StartTime >= fromDate && firstBooking.StartTime < toDate)
                            newCustomers++;
                        else if (firstBooking.StartTime < fromDate)
                            returningCustomers++;
                    }
                    else
                    {
                        newCustomers++;
                    }
                }

                decimal retentionRate = activeCustomersInPeriod > 0
                    ? Math.Round(((decimal)returningCustomers / activeCustomersInPeriod) * 100m, 1)
                    : 0m;

                decimal repeatBookingRate = activeCustomersInPeriod > 0
                    ? Math.Round(((decimal)repeatInPeriod / activeCustomersInPeriod) * 100m, 1)
                    : 0m;

                // Churn: customers who ever booked before 60 days ago, but have no bookings in the last 60 days
                var cutoff60Days = now.AddDays(-60);
                var customersWithAnyBooking = bookingsByCustomer.Keys.ToHashSet();
                int churnedCount = 0;
                int historicalEligible = 0;

                foreach (var custId in customersWithAnyBooking)
                {
                    var cBookings = bookingsByCustomer[custId];
                    var firstDate = cBookings.First().StartTime;
                    if (firstDate < cutoff60Days)
                    {
                        historicalEligible++;
                        var lastDate = cBookings.Last().StartTime;
                        if (lastDate < cutoff60Days)
                            churnedCount++;
                    }
                }

                decimal churnRate = historicalEligible > 0
                    ? Math.Round(((decimal)churnedCount / historicalEligible) * 100m, 1)
                    : 0m;

                decimal totalAllTimeSpend = allBookings.Sum(b => b.TotalAmount);
                decimal avgClv = customersWithAnyBooking.Count > 0
                    ? Math.Round(totalAllTimeSpend / customersWithAnyBooking.Count, 2)
                    : 0m;

                decimal totalPeriodRevenue = periodBookings.Sum(b => b.TotalAmount);
                decimal avgSpendPerVisit = periodBookings.Count > 0
                    ? Math.Round(totalPeriodRevenue / periodBookings.Count, 2)
                    : 0m;

                // Pareto 80/20 on period revenue
                var customerPeriodSpend = periodBookingsByCustomer
                    .Select(kv => new { CustomerId = kv.Key, Spend = kv.Value.Sum(b => b.TotalAmount) })
                    .OrderByDescending(x => x.Spend)
                    .ToList();

                int top20Count = customerPeriodSpend.Count > 0
                    ? Math.Max(1, (int)Math.Ceiling(customerPeriodSpend.Count * 0.2))
                    : 0;

                decimal top20Revenue = customerPeriodSpend.Take(top20Count).Sum(x => x.Spend);
                decimal top20Share = totalPeriodRevenue > 0
                    ? Math.Round((top20Revenue / totalPeriodRevenue) * 100m, 1)
                    : 0m;

                // Peak Hours (period bookings)
                var peakHours = Enumerable.Range(0, 24).Select(hour =>
                {
                    var count = periodBookings.Count(b => b.StartTime.Hour == hour);
                    var dt = new DateTime(2000, 1, 1, hour, 0, 0);
                    return new PeakHourItem
                    {
                        Hour = hour,
                        TimeLabel = dt.ToString("h tt"),
                        BookingCount = count
                    };
                }).Where(h => h.Hour >= 8 && h.Hour <= 23).ToList(); // studio operating hours 8 AM - 11 PM

                // Studio preferences
                var studioPreferences = periodBookings
                    .GroupBy(b => b.StudioId)
                    .Select(g => new BookingReportStudioItem
                    {
                        StudioName = studios.GetValueOrDefault(g.Key, $"Studio {g.Key}"),
                        BookingCount = g.Count(),
                        Revenue = g.Sum(b => b.TotalAmount)
                    })
                    .OrderByDescending(x => x.BookingCount)
                    .ToList();

                // RFM calculation for every customer
                var customerDetails = new List<CustomerRfmItem>();

                foreach (var cust in customers)
                {
                    var cBookings = bookingsByCustomer.GetValueOrDefault(cust.CustomerId) ?? new List<CRM_MusicStudioReservation.domain.entities.Booking>();
                    var totalBookings = cBookings.Count;
                    var lifetimeSpend = cBookings.Sum(b => b.TotalAmount);
                    DateTime? lastBooking = cBookings.Count > 0 ? cBookings.Last().StartTime : null;
                    int recencyDays = lastBooking.HasValue ? (int)Math.Max(0, (now - lastBooking.Value).TotalDays) : 999;

                    string segment;
                    if (totalBookings >= 4 && lifetimeSpend >= 5000m && recencyDays <= 30)
                        segment = "Champions (VIP)";
                    else if (totalBookings >= 2 && recencyDays <= 60)
                        segment = "Loyal Regulars";
                    else if (totalBookings == 1 && recencyDays <= 45)
                        segment = "New Customers";
                    else if (totalBookings >= 2 && recencyDays > 60)
                        segment = "At-Risk";
                    else
                        segment = "Hibernating";

                    customerDetails.Add(new CustomerRfmItem
                    {
                        CustomerId = cust.CustomerId,
                        CustomerCode = string.IsNullOrWhiteSpace(cust.CustomerCode) ? $"CUST-{cust.CustomerId:D5}" : cust.CustomerCode,
                        CustomerName = string.IsNullOrWhiteSpace(cust.CustomerName) ? $"{cust.FirstName} {cust.LastName}".Trim() : cust.CustomerName,
                        TotalBookings = totalBookings,
                        LifetimeSpend = lifetimeSpend,
                        LastBookingDate = lastBooking,
                        RecencyDays = recencyDays,
                        RfmSegment = segment,
                        IsActive = cust.IsActive
                    });
                }

                // RFM Segments Summary
                var segmentNames = new[] { "Champions (VIP)", "Loyal Regulars", "New Customers", "At-Risk", "Hibernating" };
                var rfmSegments = segmentNames.Select(name =>
                {
                    var members = customerDetails.Where(c => c.RfmSegment == name).ToList();
                    var count = members.Count;
                    var pct = totalCustomers > 0 ? Math.Round(((decimal)count / totalCustomers) * 100m, 1) : 0m;
                    var rev = members.Sum(c => c.LifetimeSpend);
                    var avg = count > 0 ? Math.Round(rev / count, 2) : 0m;

                    return new RfmSegmentItem
                    {
                        SegmentName = name,
                        CustomerCount = count,
                        Percentage = pct,
                        TotalRevenue = rev,
                        AverageSpend = avg
                    };
                }).ToList();

                return Results.Ok(new CrmAnalyticsResponse
                {
                    From = fromDate,
                    To = toDate.AddDays(-1),
                    TotalCustomers = totalCustomers,
                    ActiveCustomersInPeriod = activeCustomersInPeriod,
                    NewCustomers = newCustomers,
                    ReturningCustomers = returningCustomers,
                    RetentionRate = retentionRate,
                    ChurnRate = churnRate,
                    AverageCLV = avgClv,
                    RepeatBookingRate = repeatBookingRate,
                    Top20PercentCustomerCount = top20Count,
                    Top20PercentRevenue = top20Revenue,
                    Top20PercentRevenueShare = top20Share,
                    TotalPeriodRevenue = totalPeriodRevenue,
                    AverageSpendPerVisit = avgSpendPerVisit,
                    PeakHours = peakHours,
                    StudioPreferences = studioPreferences,
                    RfmSegments = rfmSegments,
                    CustomerDetails = customerDetails.OrderByDescending(c => c.LifetimeSpend).ToList()
                });
            });
        }
    }
}