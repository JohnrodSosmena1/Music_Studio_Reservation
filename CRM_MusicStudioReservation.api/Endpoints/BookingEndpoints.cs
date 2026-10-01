using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.api.Helpers;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;
using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class BookingEndpoints
    {
        public static void MapBookingEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/bookings").RequireAuthorization();

            // ==================== GET ALL ====================
            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.Bookings.AsNoTracking();

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderBy(b => b.StudioId)
                    .ThenBy(b => b.BookingId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var result = items.Select(b => new BookingResponseDto
                {
                    BookingId = b.BookingId,
                    BookingCode = b.BookingCode,
                    CustomerId = b.CustomerId,
                    StudioId = b.StudioId,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    TotalAmount = b.TotalAmount,
                    BookingStatus = b.BookingStatus,
                    CheckInTime = b.CheckInTime,
                    CheckOutTime = b.CheckOutTime,
                    Notes = b.Notes,
                    CreatedAt = b.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<BookingResponseDto>
                {
                    Items = result,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // ==================== GET BY ID ====================
            group.MapGet("/{id:int}", async (int companyId, ITenantDbContextFactory tenantFactory, int id) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var b = await db.Bookings.AsNoTracking().FirstOrDefaultAsync(x => x.BookingId == id);
                if (b == null) return Results.NotFound();

                var dto = new BookingResponseDto
                {
                    BookingId = b.BookingId,
                    BookingCode = b.BookingCode,
                    CustomerId = b.CustomerId,
                    StudioId = b.StudioId,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    TotalAmount = b.TotalAmount,
                    BookingStatus = b.BookingStatus,
                    CheckInTime = b.CheckInTime,
                    CheckOutTime = b.CheckOutTime,
                    Notes = b.Notes,
                    CreatedAt = b.CreatedAt
                };

                dto.Services = await db.BookingServices.Where(s => s.BookingId == dto.BookingId)
                    .Select(s => new BookingServiceResponseDto
                    {
                        BookingServiceId = s.BookingServiceId,
                        StudioServiceId = s.StudioServiceId,
                        Quantity = s.Quantity,
                        UnitPrice = s.UnitPrice
                    }).ToListAsync();

                return Results.Ok(dto);
            });

            // ==================== CREATE ====================
            group.MapPost("", async (
                int companyId,
                IBookingService bookingService,
                IAuditService auditService,
                BookingCreateDto dto,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                try
                {
                    var booking = new Booking
                    {
                        CustomerId = dto.CustomerId,
                        StudioId = dto.StudioId,
                        StartTime = DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc),
                        EndTime = DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc),
                        BookingStatus = dto.BookingStatus ?? BookingStatus.Confirmed,
                        Notes = dto.Notes
                    };

                    var services = dto.Services?.Select(s => new CRM_MusicStudioReservation.domain.entities.BookingService
                    {
                        StudioServiceId = s.StudioServiceId,
                        Quantity = s.Quantity,
                        UnitPrice = s.UnitPrice
                    }) ?? Enumerable.Empty<CRM_MusicStudioReservation.domain.entities.BookingService>();

                    var created = await bookingService.CreateBookingForTenantAsync(companyId, booking, services);

                    // 👇 AUDIT LOG — using shared helper
                    await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                        action: "Create",
                        entityName: "Booking",
                        entityId: created.BookingId,
                        newValue: $"Code={created.BookingCode}, Customer={created.CustomerId}, Studio={created.StudioId}, Amount={created.TotalAmount}");

                    var response = new BookingResponseDto
                    {
                        BookingId = created.BookingId,
                        BookingCode = created.BookingCode,
                        CustomerId = created.CustomerId,
                        StudioId = created.StudioId,
                        StartTime = created.StartTime,
                        EndTime = created.EndTime,
                        TotalAmount = created.TotalAmount,
                        BookingStatus = created.BookingStatus,
                        Notes = created.Notes,
                        CreatedAt = created.CreatedAt,
                        Services = created.BookingServices?.Select(s => new BookingServiceResponseDto
                        {
                            BookingServiceId = s.BookingServiceId,
                            StudioServiceId = s.StudioServiceId,
                            Quantity = s.Quantity,
                            UnitPrice = s.UnitPrice
                        }).ToList()
                    };

                    return Results.Created($"/tenant/{companyId}/bookings/{response.BookingId}", response);
                }
                catch (ArgumentException aex)
                {
                    return Results.BadRequest(new { error = aex.Message });
                }
                catch (InvalidOperationException ioex)
                {
                    return Results.Conflict(new { error = ioex.Message });
                }
            });

            // ==================== UPDATE ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                BookingUpdateDto dto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var validation = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
                var results = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();
                System.ComponentModel.DataAnnotations.Validator.TryValidateObject(dto, validation, results, true);
                if (results.Any())
                    return Results.ValidationProblem(results.ToDictionary(
                        r => r.MemberNames.FirstOrDefault() ?? "",
                        r => new[] { r.ErrorMessage ?? "" }));

                await using var db = await tenantFactory.CreateAsync(companyId);
                var booking = await db.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
                if (booking == null) return Results.NotFound();

                var oldStatus = booking.BookingStatus;
                var oldSnapshot = $"Status={oldStatus}, Start={booking.StartTime:o}, End={booking.EndTime:o}";

                if (dto.CustomerId.HasValue) booking.CustomerId = dto.CustomerId.Value;
                if (dto.StudioId.HasValue)
                {
                    var studio = await db.Studios.FirstOrDefaultAsync(s => s.StudioId == dto.StudioId.Value && s.IsActive);
                    if (studio == null) return Results.BadRequest(new { error = "This studio is currently unavailable. Please select another." });
                    booking.StudioId = dto.StudioId.Value;
                }
                if (dto.StartTime.HasValue) booking.StartTime = DateTime.SpecifyKind(dto.StartTime.Value, DateTimeKind.Utc);
                if (dto.EndTime.HasValue) booking.EndTime = DateTime.SpecifyKind(dto.EndTime.Value, DateTimeKind.Utc);
                if (dto.Notes != null) booking.Notes = dto.Notes;

                if (dto.BookingStatus.HasValue)
                {
                    var newStatus = dto.BookingStatus.Value;
                    booking.BookingStatus = newStatus;

                    if (newStatus == BookingStatus.CheckedIn && oldStatus != BookingStatus.CheckedIn)
                        booking.CheckInTime = DateTime.UtcNow;

                    if (newStatus == BookingStatus.CheckedOut && oldStatus != BookingStatus.CheckedOut)
                        booking.CheckOutTime = DateTime.UtcNow;

                    if (newStatus == BookingStatus.Confirmed)
                    {
                        booking.CheckInTime = null;
                        booking.CheckOutTime = null;
                    }

                    if (newStatus == BookingStatus.Cancelled)
                        booking.CheckOutTime = null;
                }

                await db.SaveChangesAsync();

                // 👇 AUDIT LOG — using shared helper
                var action = dto.BookingStatus.HasValue && dto.BookingStatus.Value != oldStatus
                    ? $"StatusChange:{oldStatus}->{dto.BookingStatus.Value}"
                    : "Update";

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: action,
                    entityName: "Booking",
                    entityId: booking.BookingId,
                    oldValue: oldSnapshot,
                    newValue: $"Status={booking.BookingStatus}, Start={booking.StartTime:o}, End={booking.EndTime:o}");

                var response = new BookingResponseDto
                {
                    BookingId = booking.BookingId,
                    BookingCode = booking.BookingCode,
                    CustomerId = booking.CustomerId,
                    StudioId = booking.StudioId,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    TotalAmount = booking.TotalAmount,
                    BookingStatus = booking.BookingStatus,
                    CheckInTime = booking.CheckInTime,
                    CheckOutTime = booking.CheckOutTime,
                    Notes = booking.Notes,
                    CreatedAt = booking.CreatedAt
                };

                return Results.Ok(response);
            });

            // ==================== DELETE (soft cancel) ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user,
                int id) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var booking = await db.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
                if (booking == null) return Results.NotFound();

                var oldStatus = booking.BookingStatus;
                booking.BookingStatus = BookingStatus.Cancelled;
                await db.SaveChangesAsync();

                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Cancel",
                    entityName: "Booking",
                    entityId: booking.BookingId,
                    oldValue: $"Status={oldStatus}",
                    newValue: $"Status=Cancelled");

                return Results.NoContent();
            });

            // ==================== CHECK-IN ====================
            group.MapPost("/{id:int}/check-in", async (
                int companyId,
                IBookingService bookingService,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user,
                int id) =>
            {
                try
                {
                    await bookingService.CheckInAsync(companyId, id);

                    await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                        action: "CheckIn",
                        entityName: "Booking",
                        entityId: id);

                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            // ==================== CHECK-OUT ====================
            group.MapPost("/{id:int}/check-out", async (
                int companyId,
                IBookingService bookingService,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user,
                int id) =>
            {
                try
                {
                    await bookingService.CheckOutAsync(companyId, id);

                    await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                        action: "CheckOut",
                        entityName: "Booking",
                        entityId: id);

                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            // ==================== CANCEL ====================
            group.MapPost("/{id:int}/cancel", async (
                int companyId,
                IBookingService bookingService,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user,
                int id,
                string? reason) =>
            {
                try
                {
                    await bookingService.CancelBookingAsync(companyId, id, reason);

                    await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                        action: "Cancel",
                        entityName: "Booking",
                        entityId: id,
                        newValue: reason != null ? $"Reason={reason}" : null);

                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            // ==================== RESCHEDULE ====================
            group.MapPost("/{id:int}/reschedule", async (
                int companyId,
                IBookingService bookingService,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user,
                int id,
                DateTime newStartUtc,
                DateTime newEndUtc) =>
            {
                try
                {
                    await bookingService.RescheduleBookingAsync(companyId, id, newStartUtc, newEndUtc);

                    await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                        action: "Reschedule",
                        entityName: "Booking",
                        entityId: id,
                        newValue: $"NewStart={newStartUtc:o}, NewEnd={newEndUtc:o}");

                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });
        }
    }
}