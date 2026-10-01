using System;
using System.Linq;
using System.Security.Claims;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class SuperAdminDashboardEndpoints
    {
        public static void MapSuperAdminDashboardEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/superadmin/dashboard")
                .RequireAuthorization(p => p.RequireRole("SuperAdmin"));

            group.MapGet("", async (MasterCRMDbContext db) =>
            {
                var now = DateTime.UtcNow;
                var sevenDaysFromNow = now.AddDays(7);

                var totalOrgs = await db.Companies.CountAsync(c => !c.IsDeleted);
                var activeOrgs = await db.Companies.CountAsync(c => !c.IsDeleted && c.Status == "Active");
                var suspendedOrgs = await db.Companies.CountAsync(c => !c.IsDeleted && c.Status == "Suspended");
                var inactiveOrgs = await db.Companies.CountAsync(c => !c.IsDeleted && c.Status == "Inactive");
                var trialOrgs = await db.Companies.CountAsync(c => !c.IsDeleted && c.Status == "Trial");

                var totalUsers = await db.AppUsers.CountAsync(u => u.IsActive);
                var activeSubs = await db.Subscriptions.CountAsync(s => s.Status == "Active" && s.ExpiresAt > now);

                // Calculate MRR from active subscriptions joined with their plan prices
                var mrr = await db.Subscriptions
                    .Where(s => s.Status == "Active" && s.ExpiresAt > now)
                    .Include(s => s.SubscriptionPlan)
                    .SumAsync(s => s.SubscriptionPlan != null ? s.SubscriptionPlan.Price : 0m);

                var expiringSoon = await db.Subscriptions
                    .CountAsync(s => s.Status == "Active" && s.ExpiresAt >= now && s.ExpiresAt <= sevenDaysFromNow);

                // Monthly tenant growth (last 6 months)
                var sixMonthsAgo = now.AddMonths(-5);
                var orgList = await db.Companies
                    .Where(c => !c.IsDeleted && c.CreatedAt >= new DateTime(sixMonthsAgo.Year, sixMonthsAgo.Month, 1))
                    .Select(c => c.CreatedAt)
                    .ToListAsync();

                var monthlyGrowth = Enumerable.Range(0, 6)
                    .Select(offset =>
                    {
                        var targetMonth = sixMonthsAgo.AddMonths(offset);
                        var count = orgList.Count(d => d.Year == targetMonth.Year && d.Month == targetMonth.Month);
                        return new MonthlyGrowthItemDto
                        {
                            Month = targetMonth.ToString("MMM yyyy"),
                            Count = count
                        };
                    })
                    .ToList();

                // Status distribution
                var statusDist = new System.Collections.Generic.List<StatusCountItemDto>
                {
                    new() { Status = "Active", Count = activeOrgs, Color = "#10B981" },
                    new() { Status = "Trial", Count = trialOrgs, Color = "#3B82F6" },
                    new() { Status = "Suspended", Count = suspendedOrgs, Color = "#EF4444" },
                    new() { Status = "Inactive", Count = inactiveOrgs, Color = "#6B7280" }
                };

                // Recent activities from SuperAdminAuditLogs
                var recentLogs = await db.SuperAdminAuditLogs
                    .OrderByDescending(l => l.CreatedAt)
                    .Take(10)
                    .ToListAsync();

                var recentActivities = recentLogs.Select(l =>
                {
                    var span = now - l.CreatedAt;
                    var timeAgo = span.TotalMinutes < 60 ? $"{(int)span.TotalMinutes}m ago"
                        : span.TotalHours < 24 ? $"{(int)span.TotalHours}h ago"
                        : $"{(int)span.TotalDays}d ago";

                    return new RecentActivityItemDto
                    {
                        Title = $"{l.Action} on {l.TargetType}",
                        Description = l.Details ?? $"{l.Action} executed by {l.UserEmail}",
                        ActionType = l.Action,
                        Timestamp = l.CreatedAt,
                        TimeAgo = timeAgo
                    };
                }).ToList();

                var dto = new SuperAdminDashboardDto
                {
                    TotalOrganizations = totalOrgs,
                    ActiveOrganizations = activeOrgs,
                    SuspendedOrganizations = suspendedOrgs,
                    InactiveOrganizations = inactiveOrgs,
                    TrialOrganizations = trialOrgs,
                    TotalPlatformUsers = totalUsers,
                    ActiveSubscriptions = activeSubs,
                    MonthlyRecurringRevenue = mrr,
                    ExpiringSoonCount = expiringSoon,
                    TenantGrowth = monthlyGrowth,
                    StatusDistribution = statusDist,
                    RecentActivities = recentActivities
                };

                return Results.Ok(dto);
            });
        }
    }
}
