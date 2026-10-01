using System;
using System.Linq;
using System.Security.Claims;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class SuperAdminOrganizationEndpoints
    {
        public static void MapSuperAdminOrganizationEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/superadmin/organizations")
                .RequireAuthorization(p => p.RequireRole("SuperAdmin"));

            // 1. LIST ORGANIZATIONS
            group.MapGet("", async (
                MasterCRMDbContext db,
                string? search,
                string? status,
                int? planId,
                int page = 1,
                int pageSize = 20) =>
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 20;

                var query = db.Companies
                    .Where(c => !c.IsDeleted)
                    .Include(c => c.SubscriptionPlan)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var s = search.Trim().ToLower();
                    query = query.Where(c =>
                        c.CompanyName.ToLower().Contains(s) ||
                        c.CompanyCode.ToLower().Contains(s) ||
                        (c.OwnerFirstName != null && c.OwnerFirstName.ToLower().Contains(s)) ||
                        (c.OwnerLastName != null && c.OwnerLastName.ToLower().Contains(s)) ||
                        (c.OwnerEmail != null && c.OwnerEmail.ToLower().Contains(s)) ||
                        (c.Subdomain != null && c.Subdomain.ToLower().Contains(s)));
                }

                if (!string.IsNullOrWhiteSpace(status) && status != "All")
                {
                    query = query.Where(c => c.Status == status);
                }

                if (planId.HasValue && planId.Value > 0)
                {
                    query = query.Where(c => c.SubscriptionPlanId == planId.Value);
                }

                var totalCount = await query.CountAsync();
                var items = await query
                    .OrderByDescending(c => c.CompanyId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(c => new OrganizationListItemDto
                    {
                        CompanyId = c.CompanyId,
                        CompanyCode = c.CompanyCode,
                        CompanyName = c.CompanyName,
                        OwnerName = (c.OwnerFirstName + " " + c.OwnerLastName).Trim(),
                        OwnerEmail = c.OwnerEmail,
                        Subdomain = c.Subdomain,
                        PlanName = c.SubscriptionPlan != null ? c.SubscriptionPlan.PlanName : "Free",
                        Status = c.Status,
                        SubscriptionStart = c.SubscriptionStart,
                        SubscriptionEnd = c.SubscriptionEnd,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt
                    })
                    .ToListAsync();

                return Results.Ok(new
                {
                    items,
                    totalCount,
                    page,
                    pageSize,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                });
            });

            // 2. GET ORGANIZATION DETAIL
            group.MapGet("/{id:int}", async (int id, MasterCRMDbContext db) =>
            {
                var org = await db.Companies
                    .Include(c => c.SubscriptionPlan)
                    .Include(c => c.Invoices)
                    .FirstOrDefaultAsync(c => c.CompanyId == id && !c.IsDeleted);

                if (org == null) return Results.NotFound(new { error = "Organization not found." });

                // Users under this tenant
                var users = await db.AppUsers
                    .Where(u => u.CompanyId == id)
                    .OrderByDescending(u => u.UserId)
                    .Take(10)
                    .Select(u => new OrgUserSummaryDto
                    {
                        UserId = u.UserId,
                        FullName = u.FullName,
                        Email = u.Email,
                        Role = u.Role.ToString(),
                        IsActive = u.IsActive,
                        LastLoginAt = u.LastLoginAt
                    })
                    .ToListAsync();

                var totalUsers = await db.AppUsers.CountAsync(u => u.CompanyId == id);

                // TandC acknowledgments
                var acks = await db.PlatformTandCAcknowledgments
                    .Where(a => a.CompanyId == id)
                    .Include(a => a.PlatformTermsAndConditions)
                    .OrderByDescending(a => a.AcknowledgedAt)
                    .Select(a => new OrgTandCAcknowledgmentDto
                    {
                        TandCTitle = a.PlatformTermsAndConditions != null ? a.PlatformTermsAndConditions.Title : "Platform Agreement",
                        Version = a.Version,
                        AcknowledgedByEmail = a.AcknowledgedByEmail,
                        AcknowledgedAt = a.AcknowledgedAt
                    })
                    .ToListAsync();

                var invoices = org.Invoices
                    .OrderByDescending(i => i.IssuedAt)
                    .Take(10)
                    .Select(i => new OrgInvoiceSummaryDto
                    {
                        InvoiceId = i.SubscriptionInvoiceId,
                        InvoiceNumber = i.InvoiceNumber,
                        Amount = i.Amount,
                        Status = i.Status,
                        IssuedAt = i.IssuedAt,
                        PaidAt = i.PaidAt
                    })
                    .ToList();

                var detail = new OrganizationDetailDto
                {
                    CompanyId = org.CompanyId,
                    CompanyCode = org.CompanyCode,
                    CompanyName = org.CompanyName,
                    Subdomain = org.Subdomain,
                    OwnerFirstName = org.OwnerFirstName,
                    OwnerLastName = org.OwnerLastName,
                    OwnerEmail = org.OwnerEmail,
                    ContactNumber = org.ContactNumber,
                    TimeZone = org.TimeZone,
                    SubscriptionPlanId = org.SubscriptionPlanId,
                    PlanName = org.SubscriptionPlan?.PlanName ?? "Free",
                    PlanPrice = org.SubscriptionPlan?.Price ?? 0m,
                    Status = org.Status,
                    SubscriptionStart = org.SubscriptionStart,
                    SubscriptionEnd = org.SubscriptionEnd,
                    IsActive = org.IsActive,
                    CreatedAt = org.CreatedAt,
                    TotalUsers = totalUsers,
                    TotalBookings = 0,
                    TotalStudios = 0,
                    Users = users,
                    Invoices = invoices,
                    TandCAcknowledgments = acks
                };

                return Results.Ok(detail);
            });

            // 3. CREATE ORGANIZATION
            group.MapPost("", async (
                OrganizationCreateDto dto,
                MasterCRMDbContext db,
                ClaimsPrincipal user,
                HttpContext httpContext) =>
            {
                var normalizedSubdomain = dto.Subdomain.Trim().ToLowerInvariant();

                // Validate subdomain uniqueness
                var exists = await db.Companies.AnyAsync(c => c.Subdomain != null && c.Subdomain.ToLower() == normalizedSubdomain && !c.IsDeleted);
                if (exists)
                {
                    return Results.BadRequest(new { error = $"Subdomain '{normalizedSubdomain}' is already taken." });
                }

                // Check owner email
                var emailExists = await db.AppUsers.AnyAsync(u => u.Email.ToLower() == dto.OwnerEmail.Trim().ToLower());
                if (emailExists)
                {
                    return Results.BadRequest(new { error = $"User with email '{dto.OwnerEmail}' already exists in the platform." });
                }

                // Auto-generate CompanyCode: TEN-00001, TEN-00002
                var maxId = await db.Companies.CountAsync();
                var companyCode = $"TEN-{(maxId + 1):D5}";

                var plan = dto.SubscriptionPlanId.HasValue
                    ? await db.SubscriptionPlans.FirstOrDefaultAsync(p => p.SubscriptionPlanId == dto.SubscriptionPlanId.Value)
                    : await db.SubscriptionPlans.FirstOrDefaultAsync(p => p.PlanCode == "PLAN-FREE");

                var now = DateTime.UtcNow;
                var org = new Company
                {
                    CompanyCode = companyCode,
                    CompanyName = dto.CompanyName.Trim(),
                    Subdomain = normalizedSubdomain,
                    OwnerFirstName = dto.OwnerFirstName.Trim(),
                    OwnerLastName = dto.OwnerLastName.Trim(),
                    OwnerEmail = dto.OwnerEmail.Trim(),
                    ContactNumber = dto.ContactNumber?.Trim(),
                    TimeZone = string.IsNullOrWhiteSpace(dto.TimeZone) ? "Asia/Manila" : dto.TimeZone.Trim(),
                    SubscriptionPlanId = plan?.SubscriptionPlanId,
                    Status = "Active",
                    SubscriptionStart = now,
                    SubscriptionEnd = now.AddMonths(1),
                    IsActive = dto.IsActive,
                    CreatedAt = now,
                    UpdatedAt = now
                };

                db.Companies.Add(org);
                await db.SaveChangesAsync();

                // Create Tenant Owner (Admin role)
                var tempPassword = "ChangeMe123!";
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);

                var ownerUser = new AppUser
                {
                    Email = dto.OwnerEmail.Trim(),
                    FullName = $"{dto.OwnerFirstName.Trim()} {dto.OwnerLastName.Trim()}",
                    PasswordHash = passwordHash,
                    Role = UserRole.Admin,
                    CompanyId = org.CompanyId,
                    IsActive = true,
                    CreatedAt = now
                };
                db.AppUsers.Add(ownerUser);

                // Create initial Subscription
                if (plan != null)
                {
                    var subscription = new Subscription
                    {
                        CompanyId = org.CompanyId,
                        SubscriptionPlanId = plan.SubscriptionPlanId,
                        Status = "Active",
                        StartedAt = now,
                        ExpiresAt = now.AddMonths(1),
                        AutoRenew = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    };
                    db.Subscriptions.Add(subscription);
                    await db.SaveChangesAsync();

                    // Generate initial invoice
                    var invoiceCount = await db.SubscriptionInvoices.CountAsync();
                    var invoice = new SubscriptionInvoice
                    {
                        InvoiceNumber = $"INV-{(invoiceCount + 1):D5}",
                        CompanyId = org.CompanyId,
                        SubscriptionId = subscription.SubscriptionId,
                        Amount = plan.Price,
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

                // Log Super Admin action
                var adminEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin@crmstudio.com";
                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = adminEmail,
                    Action = "CreateTenant",
                    TargetType = "Organization",
                    TargetId = org.CompanyId.ToString(),
                    Details = $"Created tenant '{org.CompanyName}' ({org.CompanyCode}) with Subdomain '{org.Subdomain}'. Owner: {org.OwnerEmail}",
                    IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
                    CreatedAt = now
                });
                await db.SaveChangesAsync();

                return Results.Created($"/superadmin/organizations/{org.CompanyId}", new
                {
                    org.CompanyId,
                    org.CompanyCode,
                    org.CompanyName,
                    org.Subdomain,
                    TempPassword = tempPassword,
                    Message = "Organization and Owner account created successfully."
                });
            });

            // 4. UPDATE ORGANIZATION
            group.MapPut("/{id:int}", async (
                int id,
                OrganizationUpdateDto dto,
                MasterCRMDbContext db,
                ClaimsPrincipal user,
                HttpContext httpContext) =>
            {
                var org = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == id && !c.IsDeleted);
                if (org == null) return Results.NotFound(new { error = "Organization not found." });

                if (!string.IsNullOrWhiteSpace(dto.Subdomain))
                {
                    var normalized = dto.Subdomain.Trim().ToLowerInvariant();
                    var subdomainTaken = await db.Companies.AnyAsync(c =>
                        c.CompanyId != id &&
                        c.Subdomain != null &&
                        c.Subdomain.ToLower() == normalized &&
                        !c.IsDeleted);

                    if (subdomainTaken)
                    {
                        return Results.BadRequest(new { error = $"Subdomain '{normalized}' is already taken." });
                    }
                    org.Subdomain = normalized;
                }

                org.CompanyName = dto.CompanyName.Trim();
                if (dto.OwnerFirstName != null) org.OwnerFirstName = dto.OwnerFirstName.Trim();
                if (dto.OwnerLastName != null) org.OwnerLastName = dto.OwnerLastName.Trim();
                if (dto.OwnerEmail != null) org.OwnerEmail = dto.OwnerEmail.Trim();
                if (dto.ContactNumber != null) org.ContactNumber = dto.ContactNumber.Trim();
                if (dto.TimeZone != null) org.TimeZone = dto.TimeZone.Trim();
                if (dto.Status != null) org.Status = dto.Status.Trim();
                org.IsActive = dto.IsActive;

                if (dto.SubscriptionPlanId.HasValue && dto.SubscriptionPlanId.Value != org.SubscriptionPlanId)
                {
                    org.SubscriptionPlanId = dto.SubscriptionPlanId.Value;
                }

                org.UpdatedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                var adminEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin@crmstudio.com";
                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = adminEmail,
                    Action = "UpdateTenant",
                    TargetType = "Organization",
                    TargetId = org.CompanyId.ToString(),
                    Details = $"Updated organization '{org.CompanyName}' ({org.CompanyCode}).",
                    IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = "Organization updated successfully." });
            });

            // 5. LIFECYCLE: ACTIVATE
            group.MapPost("/{id:int}/activate", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var org = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == id && !c.IsDeleted);
                if (org == null) return Results.NotFound();

                org.Status = "Active";
                org.IsActive = true;
                org.UpdatedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "ActivateTenant",
                    TargetType = "Organization",
                    TargetId = id.ToString(),
                    Details = $"Activated organization {org.CompanyCode}",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Organization '{org.CompanyName}' activated." });
            });

            // 6. LIFECYCLE: DEACTIVATE
            group.MapPost("/{id:int}/deactivate", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var org = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == id && !c.IsDeleted);
                if (org == null) return Results.NotFound();

                org.Status = "Inactive";
                org.IsActive = false;
                org.UpdatedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "DeactivateTenant",
                    TargetType = "Organization",
                    TargetId = id.ToString(),
                    Details = $"Deactivated organization {org.CompanyCode}",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Organization '{org.CompanyName}' deactivated." });
            });

            // 7. LIFECYCLE: SUSPEND
            group.MapPost("/{id:int}/suspend", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var org = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == id && !c.IsDeleted);
                if (org == null) return Results.NotFound();

                org.Status = "Suspended";
                org.IsActive = false;
                org.UpdatedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "SuspendTenant",
                    TargetType = "Organization",
                    TargetId = id.ToString(),
                    Details = $"Suspended organization {org.CompanyCode}",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Organization '{org.CompanyName}' suspended." });
            });

            // 8. LIFECYCLE: ARCHIVE
            group.MapPost("/{id:int}/archive", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var org = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == id && !c.IsDeleted);
                if (org == null) return Results.NotFound();

                org.Status = "Archived";
                org.IsActive = false;
                org.UpdatedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "ArchiveTenant",
                    TargetType = "Organization",
                    TargetId = id.ToString(),
                    Details = $"Archived organization {org.CompanyCode}",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Organization '{org.CompanyName}' archived." });
            });

            // 9. SOFT DELETE
            group.MapDelete("/{id:int}", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var org = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == id && !c.IsDeleted);
                if (org == null) return Results.NotFound();

                org.IsDeleted = true;
                org.DeletedAt = DateTime.UtcNow;
                org.Status = "Deleted";
                org.IsActive = false;
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "SoftDeleteTenant",
                    TargetType = "Organization",
                    TargetId = id.ToString(),
                    Details = $"Soft deleted organization {org.CompanyCode}",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Organization '{org.CompanyName}' soft deleted." });
            });

            // 10. HARD DELETE (Permanent with confirmation phrase)
            group.MapPost("/{id:int}/hard-delete", async (
                int id,
                HardDeleteOrganizationDto dto,
                MasterCRMDbContext db,
                ClaimsPrincipal user) =>
            {
                var org = await db.Companies.FirstOrDefaultAsync(c => c.CompanyId == id);
                if (org == null) return Results.NotFound();

                var expectedPhrase = $"DELETE {org.CompanyCode}";
                if (dto.ConfirmationPhrase != expectedPhrase)
                {
                    return Results.BadRequest(new { error = $"Invalid confirmation phrase. Please type '{expectedPhrase}'." });
                }

                // Delete associated users, subscriptions, and invoices
                var users = await db.AppUsers.Where(u => u.CompanyId == id).ToListAsync();
                db.AppUsers.RemoveRange(users);

                var subscriptions = await db.Subscriptions.Where(s => s.CompanyId == id).ToListAsync();
                db.Subscriptions.RemoveRange(subscriptions);

                var invoices = await db.SubscriptionInvoices.Where(i => i.CompanyId == id).ToListAsync();
                db.SubscriptionInvoices.RemoveRange(invoices);

                var acks = await db.PlatformTandCAcknowledgments.Where(a => a.CompanyId == id).ToListAsync();
                db.PlatformTandCAcknowledgments.RemoveRange(acks);

                db.Companies.Remove(org);
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "HardDeleteTenant",
                    TargetType = "Organization",
                    TargetId = id.ToString(),
                    Details = $"PERMANENTLY deleted organization {org.CompanyCode} ('{org.CompanyName}') and all associated data.",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"Organization '{org.CompanyName}' was permanently deleted." });
            });
        }
    }
}
