using System;
using System.Linq;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Booking service handles booking creation and lifecycle operations for tenant databases.
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public BookingService(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<bool> CheckAvailabilityAsync(int companyId, int studioId, DateTime startUtc, DateTime endUtc)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            // Overlap if existing booking's start < requested end AND existing booking's end > requested start
            var overlap = await db.Bookings
                .AsNoTracking()
                .Where(b => b.StudioId == studioId && b.BookingStatus != BookingStatus.Cancelled)
                .AnyAsync(b => b.StartTime < endUtc && b.EndTime > startUtc);

            return !overlap;
        }

        public async Task<Booking> CreateBookingInternalAsync(int companyId, Booking booking, System.Collections.Generic.IEnumerable<CRM_MusicStudioReservation.domain.entities.BookingService> services)
        {
            // Expect booking.StartTime/EndTime to be UTC
            if (booking.EndTime <= booking.StartTime)
                throw new ArgumentException("EndTime must be after StartTime.");

            await using var db = await _tenantFactory.CreateAsync(companyId);

            // Ensure studio exists and is active
            var studio = await db.Studios.FirstOrDefaultAsync(s => s.StudioId == booking.StudioId && s.IsActive);
            if (studio == null) throw new InvalidOperationException("This studio is currently unavailable. Please select another.");

            // Availability
            var available = await CheckAvailabilityAsync(companyId, booking.StudioId, booking.StartTime, booking.EndTime);
            if (!available) throw new InvalidOperationException("Studio is not available for the requested time range.");

            // Calculate total: studio rate * hours + services
            var duration = (decimal)(booking.EndTime - booking.StartTime).TotalHours;
            if (duration <= 0) duration = 0;

            decimal total = Math.Round(studio.HourlyRate * duration, 2);

            // Auto-generate sequential booking code: BKG-00001, BKG-00002, etc.
            if (string.IsNullOrWhiteSpace(booking.BookingCode))
            {
                var count = await db.Bookings.CountAsync();
                booking.BookingCode = $"BKG-{(count + 1):D5}";
            }

            // Default to Confirmed if not set or invalid
            if (!Enum.IsDefined(typeof(BookingStatus), booking.BookingStatus))
            {
                booking.BookingStatus = BookingStatus.Confirmed;
            }

            db.Bookings.Add(booking);
            await db.SaveChangesAsync();    

            if (services != null)
            {
                foreach (var s in services)
                {
                    var service = await db.StudioServices.FirstOrDefaultAsync(ss => ss.StudioServiceId == s.StudioServiceId && ss.IsActive);
                    var unitPrice = s.UnitPrice;
                    if (service != null) unitPrice = service.Price;

                    var bs = new CRM_MusicStudioReservation.domain.entities.BookingService
                    {
                        BookingId = booking.BookingId,
                        StudioServiceId = s.StudioServiceId,
                        Quantity = s.Quantity,
                        UnitPrice = unitPrice
                    };

                    total += Math.Round(unitPrice * s.Quantity, 2);
                    db.BookingServices.Add(bs);
                }

                await db.SaveChangesAsync();
            }

            booking.TotalAmount = Math.Round(total, 2);

            // ✅ Status remains Pending — do NOT overwrite it here
            await db.SaveChangesAsync();

            // load services
            booking.BookingServices = await db.BookingServices.Where(b => b.BookingId == booking.BookingId).ToListAsync();

            return booking;
        }

        public async Task<Booking> CreateBookingForTenantAsync(int companyId, Booking booking, System.Collections.Generic.IEnumerable<CRM_MusicStudioReservation.domain.entities.BookingService> services)
        {
            return await CreateBookingInternalAsync(companyId, booking, services);
        }

        public async Task CancelBookingAsync(int companyId, int bookingId, string? reason = null)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var booking = await db.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (booking == null) throw new InvalidOperationException("Booking not found.");

            booking.BookingStatus = BookingStatus.Cancelled;
            booking.Notes = string.IsNullOrWhiteSpace(reason) ? booking.Notes : booking.Notes + "\nCancellation: " + reason;
            await db.SaveChangesAsync();
        }

        public async Task RescheduleBookingAsync(int companyId, int bookingId, DateTime newStartUtc, DateTime newEndUtc)
        {
            if (newEndUtc <= newStartUtc) throw new ArgumentException("End must be after start.");

            await using var db = await _tenantFactory.CreateAsync(companyId);
            var booking = await db.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (booking == null) throw new InvalidOperationException("Booking not found.");

            var available = await CheckAvailabilityAsync(companyId, booking.StudioId, newStartUtc, newEndUtc);
            if (!available) throw new InvalidOperationException("Studio not available for the new time range.");

            booking.StartTime = newStartUtc;
            booking.EndTime = newEndUtc;
            booking.BookingStatus = BookingStatus.Rescheduled;
            await db.SaveChangesAsync();
        }

        public async Task CheckInAsync(int companyId, int bookingId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var booking = await db.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (booking == null) throw new InvalidOperationException("Booking not found.");

            booking.CheckInTime = DateTime.UtcNow;
            booking.BookingStatus = BookingStatus.CheckedIn;
            await db.SaveChangesAsync();
        }

        public async Task CheckOutAsync(int companyId, int bookingId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var booking = await db.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (booking == null) throw new InvalidOperationException("Booking not found.");

            booking.CheckOutTime = DateTime.UtcNow;
            booking.BookingStatus = BookingStatus.CheckedOut;
            await db.SaveChangesAsync();
        }
    }
}