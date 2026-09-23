using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class MembershipPlanService
    {
        private readonly ApiClient _api;

        public MembershipPlanService(ApiClient api)
        {
            _api = api;
        }

        // ==================== LIST ====================

        public async Task<List<MembershipPlanDto>> GetAllAsync(int companyId, string? search = null)
        {
            try
            {
                var url = $"tenant/{companyId}/membership-plans?pageSize=200";
                if (!string.IsNullOrWhiteSpace(search))
                    url += $"&search={Uri.EscapeDataString(search)}";

                var result = await _api.GetAsync<PagedResponse<MembershipPlanDto>>(url);
                return result?.Items ?? new List<MembershipPlanDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipPlanService.GetAll] {ex.Message}");
                return new List<MembershipPlanDto>();
            }
        }

        // ==================== GET ONE ====================

        public async Task<MembershipPlanDto?> GetAsync(int companyId, int planId)
        {
            try
            {
                return await _api.GetAsync<MembershipPlanDto>(
                    $"tenant/{companyId}/membership-plans/{planId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipPlanService.Get] {ex.Message}");
                return null;
            }
        }

        // ==================== CREATE ====================

        public async Task<MembershipPlanDto?> CreateAsync(int companyId, MembershipPlanCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<MembershipPlanCreateRequest, MembershipPlanDto>(
                    $"tenant/{companyId}/membership-plans", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipPlanService.Create] {ex.Message}");
                return null;
            }
        }

        // ==================== UPDATE ====================

        public async Task<MembershipPlanDto?> UpdateAsync(int companyId, int planId, MembershipPlanUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<MembershipPlanUpdateRequest, MembershipPlanDto>(
                    $"tenant/{companyId}/membership-plans/{planId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipPlanService.Update] {ex.Message}");
                return null;
            }
        }

        // ==================== DELETE ====================

        public async Task<bool> DeleteAsync(int companyId, int planId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/membership-plans/{planId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipPlanService.Delete] {ex.Message}");
                return false;
            }
        }
    }
}