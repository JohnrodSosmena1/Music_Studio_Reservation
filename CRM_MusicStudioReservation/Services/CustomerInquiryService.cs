using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class CustomerInquiryService
    {
        private readonly ApiClient _api;

        public CustomerInquiryService(ApiClient api)
        {
            _api = api;
        }

        // ==================== LIST ALL ====================

        public async Task<List<CustomerInquiryDto>> GetAllAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<PagedResponse<CustomerInquiryDto>>(
                    $"tenant/{companyId}/customer-inquiries?pageSize=500");
                return result?.Items ?? new List<CustomerInquiryDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.GetAll] {ex.Message}");
                return new List<CustomerInquiryDto>();
            }
        }

        // ==================== LIST BY CUSTOMER (for future client view) ====================

        public async Task<List<CustomerInquiryDto>> GetByCustomerAsync(int companyId, int customerId)
        {
            try
            {
                var result = await _api.GetAsync<PagedResponse<CustomerInquiryDto>>(
                    $"tenant/{companyId}/customer-inquiries/customer/{customerId}?pageSize=200");
                return result?.Items ?? new List<CustomerInquiryDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.GetByCustomer] {ex.Message}");
                return new List<CustomerInquiryDto>();
            }
        }

        // ==================== GET ONE ====================

        public async Task<CustomerInquiryDto?> GetAsync(int companyId, int inquiryId)
        {
            try
            {
                return await _api.GetAsync<CustomerInquiryDto>(
                    $"tenant/{companyId}/customer-inquiries/{inquiryId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.Get] {ex.Message}");
                return null;
            }
        }

        // ==================== CREATE ====================

        public async Task<CustomerInquiryDto?> CreateAsync(int companyId, CustomerInquiryCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<CustomerInquiryCreateRequest, CustomerInquiryDto>(
                    $"tenant/{companyId}/customer-inquiries", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.Create] {ex.Message}");
                return null;
            }
        }

        // ==================== UPDATE ====================

        public async Task<CustomerInquiryDto?> UpdateAsync(int companyId, int inquiryId, CustomerInquiryUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<CustomerInquiryUpdateRequest, CustomerInquiryDto>(
                    $"tenant/{companyId}/customer-inquiries/{inquiryId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.Update] {ex.Message}");
                return null;
            }
        }

        // ==================== RESPOND ====================

        public async Task<CustomerInquiryDto?> RespondAsync(int companyId, int inquiryId, string response, string? status = null)
        {
            try
            {
                var request = new CustomerInquiryRespondRequest
                {
                    Response = response,
                    Status = status
                };
                return await _api.PostAsync<CustomerInquiryRespondRequest, CustomerInquiryDto>(
                    $"tenant/{companyId}/customer-inquiries/{inquiryId}/respond", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.Respond] {ex.Message}");
                return null;
            }
        }

        // ==================== CHANGE STATUS ====================

        public async Task<bool> ChangeStatusAsync(int companyId, int inquiryId, string newStatus)
        {
            try
            {
                var request = new CustomerInquiryStatusRequest { Status = newStatus };
                await _api.PostAsync<CustomerInquiryStatusRequest, object>(
                    $"tenant/{companyId}/customer-inquiries/{inquiryId}/status", request);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.ChangeStatus] {ex.Message}");
                return false;
            }
        }

        // ==================== DELETE ====================

        public async Task<bool> DeleteAsync(int companyId, int inquiryId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/customer-inquiries/{inquiryId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InquiryService.Delete] {ex.Message}");
                return false;
            }
        }
    }
}