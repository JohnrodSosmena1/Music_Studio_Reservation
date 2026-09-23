using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.DTOs;

namespace CRM.winforms.Services
{
    /// <summary>
    /// Lightweight API client for tenant-scoped calls. Uses simple baseUrl and passes companyId in path.
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<PagingResponse<StudioResponseDto>> GetStudiosAsync(int companyId, int page = 1, int pageSize = 20, string? search = null)
        {
            var url = $"tenant/{companyId}/studios?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(search)) url += "&search=" + Uri.EscapeDataString(search);
            var res = await _http.GetFromJsonAsync<PagingResponse<StudioResponseDto>>(url);
            return res ?? new PagingResponse<StudioResponseDto>();
        }

        public async Task<StudioResponseDto?> GetStudioAsync(int companyId, int id)
        {
            var url = $"tenant/{companyId}/studios/{id}";
            return await _http.GetFromJsonAsync<StudioResponseDto?>(url);
        }

        public async Task<StudioResponseDto?> CreateStudioAsync(int companyId, StudioCreateDto dto)
        {
            var url = $"tenant/{companyId}/studios";
            var r = await _http.PostAsJsonAsync(url, dto);
            if (!r.IsSuccessStatusCode) return null;
            return await r.Content.ReadFromJsonAsync<StudioResponseDto>();
        }

        public async Task<bool> UpdateStudioAsync(int companyId, int id, StudioUpdateDto dto)
        {
            var url = $"tenant/{companyId}/studios/{id}";
            var r = await _http.PutAsJsonAsync(url, dto);
            return r.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudioAsync(int companyId, int id)
        {
            var url = $"tenant/{companyId}/studios/{id}";
            var r = await _http.DeleteAsync(url);
            return r.IsSuccessStatusCode;
        }

        public async Task<PagingResponse<BookingResponseDto>> GetBookingsAsync(int companyId, int page = 1, int pageSize = 20)
        {
            var url = $"tenant/{companyId}/bookings?page={page}&pageSize={pageSize}";
            var res = await _http.GetFromJsonAsync<PagingResponse<BookingResponseDto>>(url);
            return res ?? new PagingResponse<BookingResponseDto>();
        }
    }
}
