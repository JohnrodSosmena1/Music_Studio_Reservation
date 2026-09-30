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
                    b.BookingStatus == BookingStatus.Pending ||
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
                            b.BookingStatus == BookingStatus.Pending ||
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
        }
    }
}