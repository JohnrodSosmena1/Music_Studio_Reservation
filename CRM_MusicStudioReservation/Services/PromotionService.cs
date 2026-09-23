using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class PromotionService
    {
        private readonly ApiClient _api;

        public PromotionService(ApiClient api)
        {
            _api = api;
        }

        // ==================== LIST ====================

        public async Task<List<PromotionDto>> GetAllAsync(int companyId, string? search = null)
        {
            try
            {
                var url = $"tenant/{companyId}/promotions?pageSize=200";
                if (!string.IsNullOrWhiteSpace(search))
                    url += $"&search={Uri.EscapeDataString(search)}";

                var result = await _api.GetAsync<PagedResponse<PromotionDto>>(url);
                return result?.Items ?? new List<PromotionDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PromotionService.GetAll] {ex.Message}");
                return new List<PromotionDto>();
            }
        }

        // ==================== GET ONE ====================

        public async Task<PromotionDto?> GetAsync(int companyId, int promotionId)
        {
            try
            {
                return await _api.GetAsync<PromotionDto>(
                    $"tenant/{companyId}/promotions/{promotionId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PromotionService.Get] {ex.Message}");
                return null;
            }
        }

        // ==================== CREATE ====================

        public async Task<PromotionDto?> CreateAsync(int companyId, PromotionCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<PromotionCreateRequest, PromotionDto>(
                    $"tenant/{companyId}/promotions", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PromotionService.Create] {ex.Message}");
                return null;
            }
        }

        // ==================== UPDATE ====================

        public async Task<PromotionDto?> UpdateAsync(int companyId, int promotionId, PromotionUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<PromotionUpdateRequest, PromotionDto>(
                    $"tenant/{companyId}/promotions/{promotionId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PromotionService.Update] {ex.Message}");
                return null;
            }
        }

        // ==================== DELETE ====================

        public async Task<bool> DeleteAsync(int companyId, int promotionId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/promotions/{promotionId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PromotionService.Delete] {ex.Message}");
                return false;
            }
        }

        // ==================== VALIDATE CODE ====================

        public async Task<PromotionValidateResponse?> ValidateCodeAsync(int companyId, string code)
        {
            try
            {
                return await _api.GetAsync<PromotionValidateResponse>(
                    $"tenant/{companyId}/promotions/validate/{Uri.EscapeDataString(code)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PromotionService.ValidateCode] {ex.Message}");
                return null;
            }
        }
    }
}