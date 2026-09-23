using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.EntityFrameworkCore;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.enums;
using CRM_MusicStudioSystem.infrastructure.data;   // 👈 ADDED

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class DashboardEndpoints
    {
        public static void MapDashboardEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/dashboard");

            group.MapGet("/admin", GetAdminDashboard);
            group.MapGet("/staff", GetStaffDashboard);
            group.MapGet("/client/{customerId:int}", GetClientDashboard);
        }

        // ==================== ADMIN DASHBOARD ====================
        private static async Task<IResult> GetAdminDashboard(
            int companyId,
            ITenantDbContextFactory factory,
            MasterCRMDbContext masterDb)                   // 👈 ADDED PARAM
        {
            await using var db = await factory.CreateAsync(companyId);

            var now = DateTime.UtcNow;
            var today = now.Date;
            var tomorrow = today.AddDays(1);

            var weekStart = today.AddDays(-6);
            var lastWeekStart = today.AddDays(-13);

            // ==================== TOP-LEVEL KPIs ====================
            var totalBookings = await db.Bookings.CountAsync();
            var totalCustomers = await db.Customers.CountAsync();
            var totalRevenue = await db.Bookings
                .Where(b => b.BookingStatus != BookingStatus.Cancelled)
                .SumAsync(b => (decimal?)b.TotalAmount) ?? 0m;
            var activeStudios = await db.Studios.CountAsync(s => s.IsActive);

            // ==================== SECOND-ROW KPIs ====================
            var todayBookings = await db.Bookings
                .CountAsync(b => b.CreatedAt >= today && b.CreatedAt < tomorrow);

            var pendingBookings = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.Pending);

            var completedBookings = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.CheckedOut);

            var cancelledBookings = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.Cancelled);

            // ==================== TRENDS ====================
            var thisWeekTotal = await db.Bookings
                .CountAsync(b => b.CreatedAt >= weekStart && b.CreatedAt < tomorrow);
            var lastWeekTotal = await db.Bookings
                .CountAsync(b => b.CreatedAt >= lastWeekStart && b.CreatedAt < weekStart);

            var thisWeekToday = todayBookings;
            var lastWeekToday = await db.Bookings
                .CountAsync(b => b.CreatedAt >= today.AddDays(-7) && b.CreatedAt < tomorrow.AddDays(-7));

            var thisWeekPending = pendingBookings;
            var lastWeekPending = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.Pending
                              && b.CreatedAt >= lastWeekStart && b.CreatedAt < weekStart);

            var thisWeekCompleted = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.CheckedOut
                              && b.CreatedAt >= weekStart && b.CreatedAt < tomorrow);
            var lastWeekCompleted = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.CheckedOut
                              && b.CreatedAt >= lastWeekStart && b.CreatedAt < weekStart);

            var thisWeekCancelled = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.Cancelled
                              && b.CreatedAt >= weekStart && b.CreatedAt < tomorrow);
            var lastWeekCancelled = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.Cancelled
                              && b.CreatedAt >= lastWeekStart && b.CreatedAt < weekStart);

            static decimal Pct(int current, int previous)
            {
                if (previous == 0) return current == 0 ? 0m : 100m;
                return Math.Round(((decimal)(current - previous) / previous) * 100m, 1);
            }

            // ==================== BOOKINGS OVERVIEW ====================
            var rawCounts = await db.Bookings
                .Where(b => b.CreatedAt >= weekStart && b.CreatedAt < tomorrow)
                .GroupBy(b => b.CreatedAt.Date)
                .Select(g => new { Day = g.Key, Count = g.Count() })
                .ToListAsync();

            var bookingsOverview = Enumerable.Range(0, 7)
                .Select(offset => weekStart.AddDays(offset))
                .Select(d => new BookingsOverviewItem
                {
                    Date = d.ToString("MMM d"),
                    Count = rawCounts.FirstOrDefault(x => x.Day == d)?.Count ?? 0
                })
                .ToList();

            // ==================== BOOKINGS BY STATUS PER DAY ====================
            var statusGroups = await db.Bookings
                .Where(b => b.CreatedAt >= weekStart && b.CreatedAt < tomorrow)
                .GroupBy(b => new { Day = b.CreatedAt.Date, b.BookingStatus })
                .Select(g => new { g.Key.Day, g.Key.BookingStatus, Count = g.Count() })
                .ToListAsync();

            var bookingsByStatusByDay = Enumerable.Range(0, 7)
                .Select(offset => weekStart.AddDays(offset))
                .Select(d =>
                {
                    var dayRows = statusGroups.Where(x => x.Day == d).ToList();
                    return new BookingsByStatusDayItem
                    {
                        Date = d.ToString("MMM d"),
                        Completed = dayRows.Where(x => x.BookingStatus == BookingStatus.CheckedOut).Sum(x => x.Count),
                        Pending = dayRows.Where(x => x.BookingStatus == BookingStatus.Pending
                                                  || x.BookingStatus == BookingStatus.Confirmed
                                                  || x.BookingStatus == BookingStatus.CheckedIn).Sum(x => x.Count),
                        Cancelled = dayRows.Where(x => x.BookingStatus == BookingStatus.Cancelled).Sum(x => x.Count)
                    };
                })
                .ToList();

            // ==================== BOOKINGS BY STUDIO ====================
            var bookingsByStudioRaw = await db.Bookings
                .GroupBy(b => b.StudioId)
                .Select(g => new { StudioId = g.Key, Count = g.Count() })
                .ToListAsync();

            var studiosLookup = await db.Studios
                .ToDictionaryAsync(s => s.StudioId, s => s.StudioName);

            var donutData = bookingsByStudioRaw
                .Select(x => new BookingsByStudioItem
                {
                    Studio = studiosLookup.GetValueOrDefault(x.StudioId, $"Studio {x.StudioId}"),
                    Count = x.Count
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            // ==================== DONUT CENTER STATUS COUNTS ====================
            var bookingStatusCompleted = completedBookings;
            var bookingStatusPending = pendingBookings;
            var bookingStatusCancelled = cancelledBookings;
            var bookingStatusOnGoing = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.CheckedIn);

            // ==================== STUDIO UTILIZATION ====================
            const int WeeklyBookableHoursPerStudio = 56;

            var studioWeekBookings = await db.Bookings
                .Where(b => b.StartTime >= weekStart && b.StartTime < tomorrow.AddDays(7)
                         && b.BookingStatus != BookingStatus.Cancelled)
                .GroupBy(b => b.StudioId)
                .Select(g => new
                {
                    StudioId = g.Key,
                    BookedHours = g.Sum(b => (double)EF.Functions.DateDiffMinute(b.StartTime, b.EndTime)) / 60.0
                })
                .ToListAsync();

            var allStudios = await db.Studios
                .Where(s => s.IsActive)
                .Select(s => new { s.StudioId, s.StudioName })
                .ToListAsync();

            var studioUtilization = allStudios
                .Select(s =>
                {
                    var booked = studioWeekBookings.FirstOrDefault(x => x.StudioId == s.StudioId)?.BookedHours ?? 0;
                    var pct = (int)Math.Round(Math.Min(100, (booked / WeeklyBookableHoursPerStudio) * 100));
                    return new StudioUtilizationItem
                    {
                        StudioName = s.StudioName,
                        UtilizationPercent = pct,
                        BookingsThisWeek = (int)Math.Round(booked)
                    };
                })
                .OrderByDescending(x => x.UtilizationPercent)
                .ToList();

            // ==================== RECENT BOOKINGS ====================
            var recentRows = await db.Bookings
                .AsNoTracking()
                .OrderByDescending(b => b.CreatedAt)
                .Take(10)
                .Select(b => new
                {
                    b.BookingId,
                    b.BookingCode,
                    b.CustomerId,
                    b.StudioId,
                    b.StartTime,
                    b.EndTime,
                    b.TotalAmount,
                    b.BookingStatus,
                    b.CreatedAt
                })
                .ToListAsync();

            var customersLookup = await db.Customers
                .ToDictionaryAsync(c => c.CustomerId, c => c.CustomerName);

            var recentBookings = recentRows.Select(b => new RecentBookingItem
            {
                BookingId = b.BookingId,
                BookingCode = b.BookingCode,
                CustomerId = b.CustomerId,
                CustomerName = customersLookup.GetValueOrDefault(b.CustomerId, $"Customer {b.CustomerId}"),
                StudioId = b.StudioId,
                StudioName = studiosLookup.GetValueOrDefault(b.StudioId, $"Studio {b.StudioId}"),
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                TotalAmount = b.TotalAmount,
                BookingStatus = b.BookingStatus.ToString(),
                CreatedAt = b.CreatedAt
            }).ToList();

            // ==================== UPCOMING BOOKINGS ====================
            var upcomingRows = await db.Bookings
                .AsNoTracking()
                .Where(b => b.StartTime >= now
                         && b.BookingStatus != BookingStatus.Cancelled)
                .OrderBy(b => b.StartTime)
                .Take(5)
                .Select(b => new
                {
                    b.BookingId,
                    b.BookingCode,
                    b.CustomerId,
                    b.StudioId,
                    b.StartTime,
                    b.EndTime,
                    b.BookingStatus
                })
                .ToListAsync();

            var upcomingBookings = upcomingRows.Select(b => new UpcomingBookingItem
            {
                BookingId = b.BookingId,
                BookingCode = b.BookingCode,
                CustomerName = customersLookup.GetValueOrDefault(b.CustomerId, $"Customer {b.CustomerId}"),
                StudioName = studiosLookup.GetValueOrDefault(b.StudioId, $"Studio {b.StudioId}"),
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                BookingStatus = b.BookingStatus.ToString()
            }).ToList();

            // ==================== RECENT AUDIT LOGS (from master DB) ====================
            var recentAuditLogs = await masterDb.AuditLogs
                .AsNoTracking()
                .Where(a => a.CompanyId == companyId)
                .OrderByDescending(a => a.CreatedAt)
                .Take(10)
                .Select(a => new AuditLogFeedItem
                {
                    AuditLogId = a.AuditLogId,
                    UserEmail = a.UserEmail,
                    Action = a.Action,
                    EntityName = a.EntityName,
                    EntityId = a.EntityId,
                    Description = null,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            // ==================== RESPONSE ====================
            return Results.Ok(new AdminDashboardResponse
            {
                TotalBookings = totalBookings,
                TotalCustomers = totalCustomers,
                TotalRevenue = totalRevenue,
                ActiveStudios = activeStudios,

                TodayBookings = thisWeekToday,
                PendingBookings = thisWeekPending,
                CompletedBookings = thisWeekCompleted,
                CancelledBookings = thisWeekCancelled,

                TotalBookingsTrend = Pct(thisWeekTotal, lastWeekTotal),
                TodayBookingsTrend = Pct(thisWeekToday, lastWeekToday),
                PendingBookingsTrend = Pct(thisWeekPending, lastWeekPending),
                CompletedBookingsTrend = Pct(thisWeekCompleted, lastWeekCompleted),
                CancelledBookingsTrend = Pct(thisWeekCancelled, lastWeekCancelled),

                BookingsOverview = bookingsOverview,
                BookingsByStatusByDay = bookingsByStatusByDay,
                BookingsByStudio = donutData,

                BookingStatusCompleted = bookingStatusCompleted,
                BookingStatusPending = bookingStatusPending,
                BookingStatusCancelled = bookingStatusCancelled,
                BookingStatusOnGoing = bookingStatusOnGoing,

                StudioUtilization = studioUtilization,

                RecentBookings = recentBookings,
                UpcomingBookings = upcomingBookings,
                RecentAuditLogs = recentAuditLogs
            });
        }

        // ==================== STAFF DASHBOARD ====================
        private static async Task<IResult> GetStaffDashboard(
            int companyId,
            ITenantDbContextFactory factory)
        {
            await using var db = await factory.CreateAsync(companyId);

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var todaysBookings = await db.Bookings
                .CountAsync(b => b.StartTime >= today && b.StartTime < tomorrow);

            var checkedInCount = await db.Bookings
                .CountAsync(b => b.StartTime >= today && b.StartTime < tomorrow
                    && b.CheckInTime != null && b.CheckOutTime == null);

            var pendingBookings = await db.Bookings
                .CountAsync(b => b.BookingStatus == BookingStatus.Pending);

            var availableStudios = await db.Studios.CountAsync(s => s.IsActive);

            var schedule = await db.Bookings
                .Where(b => b.StartTime >= today && b.StartTime < tomorrow)
                .OrderBy(b => b.StartTime)
                .Select(b => new
                {
                    bookingId = b.BookingId,
                    bookingCode = b.BookingCode,
                    studioId = b.StudioId,
                    startTime = b.StartTime,
                    endTime = b.EndTime,
                    status = b.BookingStatus.ToString(),
                    checkedIn = b.CheckInTime != null,
                    checkedOut = b.CheckOutTime != null
                })
                .ToListAsync();

            return Results.Ok(new
            {
                todaysBookings,
                checkedInCount,
                pendingBookings,
                availableStudios,
                schedule
            });
        }

        // ==================== CLIENT DASHBOARD ====================
        private static async Task<IResult> GetClientDashboard(
            int companyId,
            int customerId,
            ITenantDbContextFactory factory)
        {
            await using var db = await factory.CreateAsync(companyId);

            var customer = await db.Customers.FindAsync(customerId);
            if (customer is null)
                return Results.NotFound(new { message = $"Customer {customerId} not found." });

            var now = DateTime.UtcNow;

            var upcomingBookings = await db.Bookings
                .Where(b => b.CustomerId == customerId
                    && b.StartTime >= now
                    && b.BookingStatus != BookingStatus.Cancelled)
                .OrderBy(b => b.StartTime)
                .Take(10)
                .Select(b => new
                {
                    bookingId = b.BookingId,
                    bookingCode = b.BookingCode,
                    studioId = b.StudioId,
                    startTime = b.StartTime,
                    endTime = b.EndTime,
                    status = b.BookingStatus.ToString()
                })
                .ToListAsync();

            var totalBookings = await db.Bookings.CountAsync(b => b.CustomerId == customerId);

            var membership = await db.Memberships
                .FirstOrDefaultAsync(m => m.CustomerId == customerId);

            var loyaltyPoints = membership?.LoyaltyPoints ?? 0;

            return Results.Ok(new
            {
                customerId,
                customerName = customer.CustomerName,
                upcomingBookings,
                totalBookings,
                loyaltyPoints,
                availableBalance = 0m
            });
        }
    }
}