using System;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;
using System.Collections.Generic;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public interface IBookingService
    {
        /// <summary>
        /// Checks availability for a studio in the given time range.
        /// </summary>
        Task<bool> CheckAvailabilityAsync(int companyId, int studioId, DateTime startUtc, DateTime endUtc);

        /// <summary>
        /// Creates a booking for the tenant.
        /// </summary>
        Task<CRM_MusicStudioReservation.domain.entities.Booking> CreateBookingForTenantAsync(int companyId, CRM_MusicStudioReservation.domain.entities.Booking booking, IEnumerable<CRM_MusicStudioReservation.domain.entities.BookingService> services);

        Task CancelBookingAsync(int companyId, int bookingId, string? reason = null);

        Task RescheduleBookingAsync(int companyId, int bookingId, DateTime newStartUtc, DateTime newEndUtc);

        Task CheckInAsync(int companyId, int bookingId);

        Task CheckOutAsync(int companyId, int bookingId);
    }
}
