using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class BookingService
    {
        private readonly ApiClient _api;

        public BookingService(ApiClient api)
        {
            _api = api;
        }

        // ==================== LIST ====================

        public async Task<List<BookingAdminDto>> GetAllAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<PagedResponse<BookingAdminDto>>(
                    $"tenant/{companyId}/bookings?pageSize=100");
                return result?.Items ?? new List<BookingAdminDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BookingService.GetAll] {ex.Message}");
                return new List<BookingAdminDto>();
            }
        }

        // ==================== CREATE ====================

        /// <summary>
        /// Creates a new booking. Returns the created booking response,
        /// or null if the request failed (validation error, conflict, etc.).
        /// </summary>
        public async Task<BookingResponse?> CreateAsync(int companyId, BookingCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<BookingCreateRequest, BookingResponse>(
                    $"tenant/{companyId}/bookings", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BookingService.Create] {ex.Message}");
                return null;
            }
        }

        // ==================== UPDATE ====================

        public async Task<BookingAdminDto?> UpdateAsync(int companyId, int bookingId, BookingUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<BookingUpdateRequest, BookingAdminDto>(
                    $"tenant/{companyId}/bookings/{bookingId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BookingService.Update] {ex.Message}");
                return null;
            }
        }
    }
}