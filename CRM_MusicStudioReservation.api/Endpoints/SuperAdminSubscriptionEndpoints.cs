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
    public static class SuperAdminSubscriptionEndpoints
    {
        public static void MapSuperAdminSubscriptionEndpoints(this WebApplication app)
        {
            var plansGroup = app.MapGroup("/superadmin/subscription-plans")
                .RequireAuthorization(p => p.RequireRole("SuperAdmin"));

            var subsGroup = app.MapGroup("/superadmin/subscriptions")
                .RequireAuthorization(p => p.RequireRole("SuperAdmin"));

            // ==================== PLANS ====================

            plansGroup.MapGet("", async (MasterCRMDbContext db) =>
            {
                var plans = await db.SubscriptionPlans
                    .OrderBy(p => p.Price)
                    .Select(p => new SubscriptionPlanDto
                    {
                        SubscriptionPlanId = p.SubscriptionPlanId,
                        PlanCode = p.PlanCode,
                        PlanName = p.PlanName,
                        Price = p.Price,
                        BillingCycle = p.BillingCycle,
                        MaxUsers = p.MaxUsers,
                        MaxBookingsPerMonth = p.MaxBookingsPerMonth,
                        MaxStorageMb = p.MaxStorageMb,
                        Features = p.Features,
                        IsActive = p.IsActive,
                        ActiveSubscribersCount = p.Subscriptions.Count(s => s.Status == "Active")
                    })
                    .ToListAsync();

                return Results.Ok(plans);
            });

            plansGroup.MapPost("", async (SubscriptionPlanCreateUpdateDto dto, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var exists = await db.SubscriptionPlans.AnyAsync(p => p.PlanCode.ToLower() == dto.PlanCode.Trim().ToLower());
                if (exists) return Results.BadRequest(new { error = $"Plan code '{dto.PlanCode}' already exists." });

                var plan = new SubscriptionPlan
                {
                    PlanCode = dto.PlanCode.Trim().ToUpperInvariant(),
                    PlanName = dto.PlanName.Trim(),
                    Price = dto.Price,
                    BillingCycle = dto.BillingCycle,
                    MaxUsers = dto.MaxUsers,
                    MaxBookingsPerMonth = dto.MaxBookingsPerMonth,
                    MaxStorageMb = dto.MaxStorageMb,
                    Features = dto.Features ?? string.Empty,
                    IsActive = dto.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                db.SubscriptionPlans.Add(plan);
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "CreatePlan",
                    TargetType = "SubscriptionPlan",
                    TargetId = plan.SubscriptionPlanId.ToString(),
                    Details = $"Created plan '{plan.PlanName}' (₱{plan.Price})",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Created($"/superadmin/subscription-plans/{plan.SubscriptionPlanId}", plan);
            });

            plansGroup.MapPut("/{id:int}", async (int id, SubscriptionPlanCreateUpdateDto dto, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var plan = await db.SubscriptionPlans.FirstOrDefaultAsync(p => p.SubscriptionPlanId == id);
                if (plan == null) return Results.NotFound();

                plan.PlanName = dto.PlanName.Trim();
                plan.Price = dto.Price;
                plan.BillingCycle = dto.BillingCycle;
                plan.MaxUsers = dto.MaxUsers;
                plan.MaxBookingsPerMonth = dto.MaxBookingsPerMonth;
                plan.MaxStorageMb = dto.MaxStorageMb;
                plan.Features = dto.Features ?? string.Empty;
                plan.IsActive = dto.IsActive;
                plan.UpdatedAt = DateTime.UtcNow;

                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "UpdatePlan",
                    TargetType = "SubscriptionPlan",
                    TargetId = id.ToString(),
                    Details = $"Updated plan '{plan.PlanName}'",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(plan);
            });

            // ==================== SUBSCRIPTIONS ====================

            subsGroup.MapGet("", async (MasterCRMDbContext db, string? status) =>
            {
                var query = db.Subscriptions
                    .Include(s => s.Company)
                    .Include(s => s.SubscriptionPlan)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(status) && status != "All")
                {
                    query = query.Where(s => s.Status == status);
                }

                var list = await query
                    .OrderByDescending(s => s.ExpiresAt)
                    .Select(s => new
                    {
                        s.SubscriptionId,
                        s.CompanyId,
                        CompanyName = s.Company != null ? s.Company.CompanyName : "Unknown",
                        CompanyCode = s.Company != null ? s.Company.CompanyCode : "",
                        PlanName = s.SubscriptionPlan != null ? s.SubscriptionPlan.PlanName : "Unknown",
                        Price = s.SubscriptionPlan != null ? s.SubscriptionPlan.Price : 0m,
                        s.Status,
                        s.StartedAt,
                        s.ExpiresAt,
                        s.AutoRenew
                    })
                    .ToListAsync();

                return Results.Ok(list);
            });

            subsGroup.MapPost("/assign", async (AssignSubscriptionDto dto, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var company = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == dto.CompanyId && !c.IsDeleted);
                if (company == null) return Results.NotFound(new { error = "Organization not found." });

                var plan = await db.SubscriptionPlans.FirstOrDefaultAsync(p => p.SubscriptionPlanId == dto.SubscriptionPlanId);
                if (plan == null) return Results.NotFound(new { error = "Subscription plan not found." });

                var now = DateTime.UtcNow;
                var expiresAt = now.AddMonths(dto.DurationMonths > 0 ? dto.DurationMonths : 1);

                // Check active subscription
                var currentSub = await db.Subscriptions.FirstOrDefaultAsync(s => s.CompanyId == dto.CompanyId && s.Status == "Active");
                if (currentSub != null)
                {
                    currentSub.Status = "Cancelled";
                    currentSub.CancelledAt = now;
                    currentSub.UpdatedAt = now;
                }

                var newSub = new Subscription
                {
                    CompanyId = company.CompanyId,
                    SubscriptionPlanId = plan.SubscriptionPlanId,
                    Status = "Active",
                    StartedAt = now,
                    ExpiresAt = expiresAt,
                    AutoRenew = dto.AutoRenew,
                    CreatedAt = now,
                    UpdatedAt = now
                };
                db.Subscriptions.Add(newSub);

                company.SubscriptionPlanId = plan.SubscriptionPlanId;
                company.SubscriptionStart = now;
                company.SubscriptionEnd = expiresAt;
                company.UpdatedAt = now;

                if (dto.GenerateInvoice)
                {
                    var count = await db.SubscriptionInvoices.CountAsync();
                    var invoice = new SubscriptionInvoice
                    {
                        InvoiceNumber = $"INV-{(count + 1):D5}",
                        CompanyId = company.CompanyId,
                        SubscriptionId = newSub.SubscriptionId,
                        Amount = plan.Price * dto.DurationMonths,
                        Status = plan.Price == 0 ? "Paid" : "Pending",
                        IssuedAt = now,
                        DueAt = now.AddDays(7),
                        PaidAt = plan.Price == 0 ? now : null,
                        PaymentMethod = plan.Price == 0 ? "Free Plan" : "Credit Card",
                        CreatedAt = now
                    };
                    db.SubscriptionInvoices.Add(invoice);
                }

                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog   
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "AssignPlan",
                    TargetType = "Subscription",
                    TargetId = newSub.SubscriptionId.ToString(),
                    Details = $"Assigned plan '{plan.PlanName}' to {company.CompanyName} for {dto.DurationMonths} month(s).",
                    CreatedAt = now
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Assigned {plan.PlanName} to {company.CompanyName} successfully." });
            });

            // ==================== INVOICES ====================

            subsGroup.MapGet("/invoices", async (MasterCRMDbContext db, int? companyId, string? status) =>
            {
                var query = db.SubscriptionInvoices
                    .Include(i => i.Company)
                    .AsQueryable();

                if (companyId.HasValue && companyId.Value > 0)
                {
                    query = query.Where(i => i.CompanyId == companyId.Value);
                }

                if (!string.IsNullOrWhiteSpace(status) && status != "All")
                {
                    query = query.Where(i => i.Status == status);
                }

                var invoices = await query
                    .OrderByDescending(i => i.IssuedAt)
                    .Select(i => new SubscriptionInvoiceDto
                    {
                        SubscriptionInvoiceId = i.SubscriptionInvoiceId,
                        InvoiceNumber = i.InvoiceNumber,
                        CompanyId = i.CompanyId,
                        CompanyName = i.Company != null ? i.Company.CompanyName : "Unknown",
                        Amount = i.Amount,
                        Status = i.Status,
                        IssuedAt = i.IssuedAt,
                        DueAt = i.DueAt,
                        PaidAt = i.PaidAt,
                        PaymentMethod = i.PaymentMethod
                    })
                    .ToListAsync();

                return Results.Ok(invoices);
            });

            subsGroup.MapPost("/invoices/{id:int}/mark-paid", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var invoice = await db.SubscriptionInvoices.FirstOrDefaultAsync(i => i.SubscriptionInvoiceId == id);
                if (invoice == null) return Results.NotFound();

                invoice.Status = "Paid";
                invoice.PaidAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "MarkInvoicePaid",
                    TargetType = "Invoice",
                    TargetId = id.ToString(),
                    Details = $"Marked invoice {invoice.InvoiceNumber} as Paid.",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Invoice {invoice.InvoiceNumber} marked as Paid." });
            });
        }
    }
}
