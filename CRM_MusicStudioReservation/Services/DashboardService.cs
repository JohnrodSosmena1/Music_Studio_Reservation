using System;
using System.Collections.Generic;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    /// <summary>
    /// Wraps ApiClient to fetch dashboard data from the tenant API.
    /// Includes short-lived in-memory caching so repeated navigation
    /// (Dashboard → Module → Dashboard) does not re-fetch every time.
    /// </summary>
    public class DashboardService
    {
        private readonly ApiClient _api;

        // ==================== CACHE (per-process) ====================
        private static AdminDashboardDto? _cachedAdminDashboard;
        private static DateTime _adminDashboardCachedAt = DateTime.MinValue;
        private static int _adminDashboardCachedCompanyId = 0;
        private static readonly TimeSpan AdminDashboardCacheTtl = TimeSpan.FromSeconds(60);

        public DashboardService(ApiClient api)
        {
            _api = api;
        }

        // ==================== CACHE CONTROL ====================

        /// <summary>
        /// Clears the cached admin dashboard. Call this after any action that
        /// changes bookings/customers/studios so the next visit is fresh.
        /// </summary>
        public static void InvalidateAdminDashboardCache()
        {
            _cachedAdminDashboard = null;
            _adminDashboardCachedAt = DateTime.MinValue;
            _adminDashboardCachedCompanyId = 0;
        }

        // ==================== DASHBOARDS ====================

        /// <summary>
        /// Gets the admin dashboard. Uses a 60-second in-memory cache.
        /// Pass forceRefresh = true to bypass the cache.
        /// </summary>
        public async Task<AdminDashboardDto?> GetAdminDashboardAsync(int companyId, bool forceRefresh = false)
        {
            // Serve from cache if still valid and for the same company
            if (!forceRefresh
                && _cachedAdminDashboard != null
                && _adminDashboardCachedCompanyId == companyId
                && (DateTime.UtcNow - _adminDashboardCachedAt) < AdminDashboardCacheTtl)
            {
                System.Diagnostics.Debug.WriteLine("[DashboardService] Returning cached admin dashboard");
                return _cachedAdminDashboard;
            }

            try
            {
                var data = await _api.GetAsync<AdminDashboardDto>($"tenant/{companyId}/dashboard/admin");

                if (data != null)
                {
                    _cachedAdminDashboard = data;
                    _adminDashboardCachedAt = DateTime.UtcNow;
                    _adminDashboardCachedCompanyId = companyId;
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAdminDashboard] {ex.Message}");
                return null;
            }
        }

        public async Task<StaffDashboardDto?> GetStaffDashboardAsync(int companyId)
        {
            try
            {
                return await _api.GetAsync<StaffDashboardDto>($"tenant/{companyId}/dashboard/staff");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetStaffDashboard] {ex.Message}");
                return null;
            }
        }

        public async Task<ClientDashboardDto?> GetClientDashboardAsync(int companyId, int customerId)
        {
            try
            {
                return await _api.GetAsync<ClientDashboardDto>($"tenant/{companyId}/dashboard/client/{customerId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetClientDashboard] {ex.Message}");
                return null;
            }
        }

        // ==================== STUDIOS ====================

        public async Task<List<StudioDto>> GetStudiosAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<PagedResponse<StudioDto>>($"tenant/{companyId}/studios");
                return result?.Items ?? new List<StudioDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetStudios] Failed: {ex.Message}");
                return new List<StudioDto>();
            }
        }

        // ==================== BOOKINGS ====================

        public async Task<BookingResponse?> CreateBookingAsync(int companyId, BookingCreateRequest request)
        {
            try
            {
                var result = await _api.PostAsync<BookingCreateRequest, BookingResponse>(
                    $"tenant/{companyId}/bookings", request);

                // New booking → invalidate dashboard cache so numbers update
                if (result != null)
                    InvalidateAdminDashboardCache();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateBooking] Failed: {ex.Message}");
                return null;
            }
        }
    }
}