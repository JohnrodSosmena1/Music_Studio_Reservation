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
    public static class SuperAdminTandCEndpoints
    {
        public static void MapSuperAdminTandCEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/superadmin/terms")
                .RequireAuthorization(p => p.RequireRole("SuperAdmin"));

            // LIST
            group.MapGet("", async (MasterCRMDbContext db, string? type, string? status) =>
            {
                var query = db.PlatformTermsAndConditions
                    .Include(t => t.Acknowledgments)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(type) && type != "All")
                {
                    query = query.Where(t => t.TandCType == type);
                }

                if (!string.IsNullOrWhiteSpace(status) && status != "All")
                {
                    query = query.Where(t => t.Status == status);
                }

                var list = await query
                    .OrderByDescending(t => t.UpdatedAt)
                    .Select(t => new PlatformTandCDto
                    {
                        PlatformTandCId = t.PlatformTandCId,
                        TandCCode = t.TandCCode,
                        TandCType = t.TandCType,
                        Title = t.Title,
                        Content = t.Content,
                        Version = t.Version,
                        Status = t.Status,
                        PublishedAt = t.PublishedAt,
                        AuthorName = t.AuthorName,
                        ChangeNotes = t.ChangeNotes,
                        RequiresReAcceptance = t.RequiresReAcceptance,
                        CreatedAt = t.CreatedAt,
                        UpdatedAt = t.UpdatedAt,
                        AcknowledgedCount = t.Acknowledgments.Count
                    })
                    .ToListAsync();

                return Results.Ok(list);
            });

            // GET BY ID
            group.MapGet("/{id:int}", async (int id, MasterCRMDbContext db) =>
            {
                var item = await db.PlatformTermsAndConditions
                    .Include(t => t.Acknowledgments)
                    .FirstOrDefaultAsync(t => t.PlatformTandCId == id);

                if (item == null) return Results.NotFound();

                var dto = new PlatformTandCDto
                {
                    PlatformTandCId = item.PlatformTandCId,
                    TandCCode = item.TandCCode,
                    TandCType = item.TandCType,
                    Title = item.Title,
                    Content = item.Content,
                    Version = item.Version,
                    Status = item.Status,
                    PublishedAt = item.PublishedAt,
                    AuthorName = item.AuthorName,
                    ChangeNotes = item.ChangeNotes,
                    RequiresReAcceptance = item.RequiresReAcceptance,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    AcknowledgedCount = item.Acknowledgments.Count
                };

                return Results.Ok(dto);
            });

            // CREATE DRAFT
            group.MapPost("", async (PlatformTandCCreateDto dto, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var count = await db.PlatformTermsAndConditions.CountAsync();
                var code = $"PTC-{(count + 1):D5}";

                // Version calculation
                var latest = await db.PlatformTermsAndConditions
                    .Where(t => t.TandCType == dto.TandCType)
                    .OrderByDescending(t => t.MajorVersion)
                    .ThenByDescending(t => t.MinorVersion)
                    .FirstOrDefaultAsync();

                var major = latest != null ? latest.MajorVersion : 1;
                var minor = latest != null ? latest.MinorVersion + 1 : 0;
                var version = $"v{major}.{minor}";

                var adminName = user.FindFirst(ClaimTypes.Name)?.Value ?? "Super Admin";

                var tc = new PlatformTermsAndConditions
                {
                    TandCCode = code,
                    TandCType = dto.TandCType,
                    Title = dto.Title.Trim(),
                    Content = dto.Content.Trim(),
                    Version = version,
                    MajorVersion = major,
                    MinorVersion = minor,
                    Status = "Draft",
                    AuthorName = adminName,
                    ChangeNotes = dto.ChangeNotes,
                    RequiresReAcceptance = dto.RequiresReAcceptance,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                db.PlatformTermsAndConditions.Add(tc);
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "CreatePlatformTandC",
                    TargetType = "TandC",
                    TargetId = tc.PlatformTandCId.ToString(),
                    Details = $"Created draft T&C '{tc.Title}' ({tc.Version})",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Created($"/superadmin/terms/{tc.PlatformTandCId}", tc);
            });

            // UPDATE DRAFT
            group.MapPut("/{id:int}", async (int id, PlatformTandCUpdateDto dto, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var tc = await db.PlatformTermsAndConditions.FirstOrDefaultAsync(t => t.PlatformTandCId == id);
                if (tc == null) return Results.NotFound();

                if (tc.Status != "Draft")
                {
                    return Results.BadRequest(new { error = "Only draft Terms & Conditions can be edited. Published versions are immutable." });
                }

                tc.Title = dto.Title.Trim();
                tc.Content = dto.Content.Trim();
                tc.ChangeNotes = dto.ChangeNotes;
                tc.RequiresReAcceptance = dto.RequiresReAcceptance;
                tc.UpdatedAt = DateTime.UtcNow;

                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "UpdatePlatformTandC",
                    TargetType = "TandC",
                    TargetId = id.ToString(),
                    Details = $"Updated draft T&C '{tc.Title}'",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(tc);
            });

            // PUBLISH
            group.MapPost("/{id:int}/publish", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var tc = await db.PlatformTermsAndConditions.FirstOrDefaultAsync(t => t.PlatformTandCId == id);
                if (tc == null) return Results.NotFound();

                var now = DateTime.UtcNow;

                // Archive previously published version of same type
                var previous = await db.PlatformTermsAndConditions
                    .Where(t => t.TandCType == tc.TandCType && t.Status == "Published")
                    .ToListAsync();

                foreach (var p in previous)
                {
                    p.Status = "Archived";
                    p.UpdatedAt = now;
                }

                tc.Status = "Published";
                tc.PublishedAt = now;
                tc.UpdatedAt = now;

                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "PublishPlatformTandC",
                    TargetType = "TandC",
                    TargetId = id.ToString(),
                    Details = $"Published T&C '{tc.Title}' ({tc.Version})",
                    CreatedAt = now
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"T&C '{tc.Title}' published successfully." });
            });

            // ARCHIVE
            group.MapPost("/{id:int}/archive", async (int id, MasterCRMDbContext db, ClaimsPrincipal user) =>
            {
                var tc = await db.PlatformTermsAndConditions.FirstOrDefaultAsync(t => t.PlatformTandCId == id);
                if (tc == null) return Results.NotFound();

                tc.Status = "Archived";
                tc.UpdatedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                db.SuperAdminAuditLogs.Add(new SuperAdminAuditLog
                {
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "superadmin",
                    Action = "ArchivePlatformTandC",
                    TargetType = "TandC",
                    TargetId = id.ToString(),
                    Details = $"Archived T&C '{tc.Title}'",
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = $"T&C '{tc.Title}' archived." });
            });

            // ACKNOWLEDGMENTS
            group.MapGet("/{id:int}/acknowledgments", async (int id, MasterCRMDbContext db) =>
            {
                var acks = await db.PlatformTandCAcknowledgments
                    .Where(a => a.PlatformTandCId == id)
                    .Include(a => a.Company)
                    .OrderByDescending(a => a.AcknowledgedAt)
                    .Select(a => new
                    {
                        a.AcknowledgmentId,
                        a.CompanyId,
                        CompanyName = a.Company != null ? a.Company.CompanyName : "Unknown",
                        CompanyCode = a.Company != null ? a.Company.CompanyCode : "",
                        a.AcknowledgedByEmail,
                        a.AcknowledgedByName,
                        a.Version,
                        a.IpAddress,
                        a.AcknowledgedAt
                    })
                    .ToListAsync();

                return Results.Ok(acks);
            });
        }
    }
}
