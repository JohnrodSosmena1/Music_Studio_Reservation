using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using CRM.winforms.DTOs;
using System;
using System.Collections.Generic;

namespace CRM.winforms.Services
{
    public class StudioService
    {
        private readonly ApiClient _api;

        public StudioService(ApiClient api)
        {
            _api = api;
        }

        /// <summary>
        /// Fetches all studios for a company.
        /// Includes inactive studios so the admin can see and re-enable them.
        /// </summary>
        public async Task<List<StudioDto>> GetAllAsync(int companyId)
        {
            try
            {
                // 👈 includeInactive=true so disabled studios stay in the grid
                var result = await _api.GetAsync<PagedResponse<StudioDto>>(
                    $"tenant/{companyId}/studios?includeInactive=true&pageSize=100");
                return result?.Items ?? new List<StudioDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StudioService.GetAll] {ex.Message}");
                return new List<StudioDto>();
            }
        }

        public async Task<StudioDto?> CreateAsync(int companyId, StudioCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<StudioCreateRequest, StudioDto>(
                    $"tenant/{companyId}/studios", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StudioService.Create] {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Updates a studio. The API now returns the updated StudioResponseDto.
        /// </summary>
        public async Task<StudioDto?> UpdateAsync(int companyId, int studioId, StudioUpdateRequest request)
        {
            try
            {
                // API PUT now returns 200 with the updated studio (not 204)
                return await _api.PutAsync<StudioUpdateRequest, StudioDto>(
                    $"tenant/{companyId}/studios/{studioId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StudioService.Update] {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Fetches all inventory items assigned to a studio.
        /// </summary>
        public async Task<List<StudioInventoryItemDto>> GetStudioInventoryAsync(int companyId, int studioId)
        {
            try
            {
                var result = await _api.GetAsync<List<StudioInventoryItemDto>>(
                    $"tenant/{companyId}/studios/{studioId}/inventory");
                return result ?? new List<StudioInventoryItemDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StudioService.GetStudioInventory] {ex.Message}");
                return new List<StudioInventoryItemDto>();
            }
        }

        /// <summary>
        /// Deletes/archives a studio.
        /// </summary>
        public async Task<bool> DeleteAsync(int companyId, int studioId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/studios/{studioId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StudioService.Delete] {ex.Message}");
                return false;
            }
        }
    }
}
