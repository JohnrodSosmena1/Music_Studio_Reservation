using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class TermsService
    {
        private readonly ApiClient _api;

        public TermsService(ApiClient api)
        {
            _api = api;
        }

        // ==================== LIST ====================

        public async Task<List<TermsListItemDto>> GetAllAsync(
            int companyId, 
            string? type = null, 
            string? status = null, 
            string? search = null, 
            int page = 1, 
            int pageSize = 100)
        {
            try
            {
                var queryParams = new List<string> { $"page={page}", $"pageSize={pageSize}" };

                if (!string.IsNullOrWhiteSpace(type) && type != "All Types")
                    queryParams.Add($"type={Uri.EscapeDataString(type)}");

                if (!string.IsNullOrWhiteSpace(status) && status != "All Statuses")
                    queryParams.Add($"status={Uri.EscapeDataString(status)}");

                if (!string.IsNullOrWhiteSpace(search))
                    queryParams.Add($"search={Uri.EscapeDataString(search)}");

                var url = $"tenant/{companyId}/terms?{string.Join("&", queryParams)}";
                var result = await _api.GetAsync<PagedResponse<TermsListItemDto>>(url);
                return result?.Items ?? new List<TermsListItemDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.GetAll] {ex.Message}");
                return new List<TermsListItemDto>();
            }
        }

        // ==================== GET BY ID ====================

        public async Task<TermsDetailDto?> GetByIdAsync(int companyId, int id)
        {
            try
            {
                return await _api.GetAsync<TermsDetailDto>($"tenant/{companyId}/terms/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.GetById] {ex.Message}");
                return null;
            }
        }

        // ==================== GET PUBLISHED (User/Public) ====================

        public async Task<TermsDetailDto?> GetPublishedAsync(int companyId, string type)
        {
            try
            {
                return await _api.GetAsync<TermsDetailDto>($"tenant/{companyId}/terms/published/{Uri.EscapeDataString(type)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.GetPublished] {ex.Message}");
                return null;
            }
        }

        // ==================== CREATE (Draft) ====================

        public async Task<TermsDetailDto?> CreateDraftAsync(int companyId, TermsCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<TermsCreateRequest, TermsDetailDto>(
                    $"tenant/{companyId}/terms", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.CreateDraft] {ex.Message}");
                throw;
            }
        }

        // ==================== UPDATE (Draft only) ====================

        public async Task<TermsDetailDto?> UpdateDraftAsync(int companyId, int id, TermsUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<TermsUpdateRequest, TermsDetailDto>(
                    $"tenant/{companyId}/terms/{id}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.UpdateDraft] {ex.Message}");
                throw;
            }
        }

        // ==================== SUBMIT FOR APPROVAL ====================

        public async Task<bool> SubmitForApprovalAsync(int companyId, int id)
        {
            try
            {
                await _api.PostAsync<object, object?>($"tenant/{companyId}/terms/{id}/submit", new { });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.SubmitForApproval] {ex.Message}");
                throw;
            }
        }

        // ==================== PUBLISH / APPROVE ====================

        public async Task<bool> PublishAsync(int companyId, int id, string? note = null)
        {
            try
            {
                await _api.PostAsync<TermsPublishRequest, object?>(
                    $"tenant/{companyId}/terms/{id}/publish", new TermsPublishRequest { ApproverNote = note });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.Publish] {ex.Message}");
                throw;
            }
        }

        // ==================== REJECT ====================

        public async Task<bool> RejectAsync(int companyId, int id, string reason)
        {
            try
            {
                await _api.PostAsync<TermsRejectRequest, object?>(
                    $"tenant/{companyId}/terms/{id}/reject", new TermsRejectRequest { Reason = reason });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.Reject] {ex.Message}");
                throw;
            }
        }

        // ==================== ARCHIVE ====================

        public async Task<bool> ArchiveAsync(int companyId, int id)
        {
            try
            {
                await _api.PostAsync<object, object?>($"tenant/{companyId}/terms/{id}/archive", new { });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.Archive] {ex.Message}");
                throw;
            }
        }

        // ==================== ACKNOWLEDGMENTS ====================

        public async Task<List<TermsAcknowledgmentDto>> GetAcknowledgmentsAsync(int companyId, int? tandCId = null, int? customerId = null)
        {
            try
            {
                var queryParams = new List<string> { "pageSize=200" };
                if (tandCId.HasValue) queryParams.Add($"tandCId={tandCId.Value}");
                if (customerId.HasValue) queryParams.Add($"customerId={customerId.Value}");

                var url = $"tenant/{companyId}/terms/acknowledgments?{string.Join("&", queryParams)}";
                var result = await _api.GetAsync<PagedResponse<TermsAcknowledgmentDto>>(url);
                return result?.Items ?? new List<TermsAcknowledgmentDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TermsService.GetAcknowledgments] {ex.Message}");
                return new List<TermsAcknowledgmentDto>();
            }
        }
    }
}
