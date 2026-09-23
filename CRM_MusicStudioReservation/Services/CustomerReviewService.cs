using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class CustomerReviewService
    {
        private readonly ApiClient _api;

        public CustomerReviewService(ApiClient api)
        {
            _api = api;
        }

        // ==================== LIST ====================

        public async Task<List<CustomerReviewDto>> GetAllAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<PagedResponse<CustomerReviewDto>>(
                    $"tenant/{companyId}/customer-reviews?pageSize=500");
                return result?.Items ?? new List<CustomerReviewDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewService.GetAll] {ex.Message}");
                return new List<CustomerReviewDto>();
            }
        }

        public async Task<CustomerReviewDto?> GetAsync(int companyId, int reviewId)
        {
            try
            {
                return await _api.GetAsync<CustomerReviewDto>(
                    $"tenant/{companyId}/customer-reviews/{reviewId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewService.Get] {ex.Message}");
                return null;
            }
        }

        // ==================== CREATE ====================

        public async Task<CustomerReviewDto?> CreateAsync(int companyId, CustomerReviewCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<CustomerReviewCreateRequest, CustomerReviewDto>(
                    $"tenant/{companyId}/customer-reviews", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewService.Create] {ex.Message}");
                return null;
            }
        }

        // ==================== UPDATE ====================

        public async Task<CustomerReviewDto?> UpdateAsync(int companyId, int reviewId, CustomerReviewUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<CustomerReviewUpdateRequest, CustomerReviewDto>(
                    $"tenant/{companyId}/customer-reviews/{reviewId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewService.Update] {ex.Message}");
                return null;
            }
        }

        // ==================== MODERATE ====================

        public async Task<bool> ModerateAsync(int companyId, int reviewId, string newStatus)
        {
            try
            {
                var request = new CustomerReviewModerateRequest { ModerationStatus = newStatus };
                await _api.PostAsync<CustomerReviewModerateRequest, object>(
                    $"tenant/{companyId}/customer-reviews/{reviewId}/moderate", request);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewService.Moderate] {ex.Message}");
                return false;
            }
        }

        // ==================== REPLY ====================

        public async Task<bool> ReplyAsync(int companyId, int reviewId, string adminReply)
        {
            try
            {
                var request = new CustomerReviewReplyRequest { AdminReply = adminReply };
                await _api.PostAsync<CustomerReviewReplyRequest, object>(
                    $"tenant/{companyId}/customer-reviews/{reviewId}/reply", request);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewService.Reply] {ex.Message}");
                return false;
            }
        }

        // ==================== DELETE ====================

        public async Task<bool> DeleteAsync(int companyId, int reviewId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/customer-reviews/{reviewId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReviewService.Delete] {ex.Message}");
                return false;
            }
        }
    }
}