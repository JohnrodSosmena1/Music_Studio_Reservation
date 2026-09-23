using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class CustomerFeedbackService
    {
        private readonly ApiClient _api;

        public CustomerFeedbackService(ApiClient api)
        {
            _api = api;
        }

        // ==================== LIST ALL ====================

        public async Task<List<CustomerFeedbackDto>> GetAllAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<PagedResponse<CustomerFeedbackDto>>(
                    $"tenant/{companyId}/customer-feedback?pageSize=500");
                return result?.Items ?? new List<CustomerFeedbackDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FeedbackService.GetAll] {ex.Message}");
                return new List<CustomerFeedbackDto>();
            }
        }

        // ==================== GET ONE ====================

        public async Task<CustomerFeedbackDto?> GetAsync(int companyId, int feedbackId)
        {
            try
            {
                return await _api.GetAsync<CustomerFeedbackDto>(
                    $"tenant/{companyId}/customer-feedback/{feedbackId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FeedbackService.Get] {ex.Message}");
                return null;
            }
        }

        // ==================== CREATE ====================

        public async Task<CustomerFeedbackDto?> CreateAsync(int companyId, CustomerFeedbackCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<CustomerFeedbackCreateRequest, CustomerFeedbackDto>(
                    $"tenant/{companyId}/customer-feedback", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FeedbackService.Create] {ex.Message}");
                return null;
            }
        }

        // ==================== UPDATE ====================

        public async Task<CustomerFeedbackDto?> UpdateAsync(int companyId, int feedbackId, CustomerFeedbackCreateRequest request)
        {
            try
            {
                return await _api.PutAsync<CustomerFeedbackCreateRequest, CustomerFeedbackDto>(
                    $"tenant/{companyId}/customer-feedback/{feedbackId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FeedbackService.Update] {ex.Message}");
                return null;
            }
        }

        // ==================== DELETE ====================

        public async Task<bool> DeleteAsync(int companyId, int feedbackId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/customer-feedback/{feedbackId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FeedbackService.Delete] {ex.Message}");
                return false;
            }
        }
    }
}