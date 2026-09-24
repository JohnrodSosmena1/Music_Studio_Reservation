using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    /// <summary>
    /// Handles membership CRUD + plan listing via the API.
    /// </summary>
    public class MembershipService
    {
        private readonly ApiClient _api;

        public MembershipService(ApiClient api)
        {
            _api = api;
        }

        // ==================== PLANS ====================

        /// <summary>Lists all membership plans for a company.</summary>
        public async Task<List<MembershipPlanDto>> GetPlansAsync(int companyId)
        {
            try
            {
                // Your API returns a paged response for this endpoint.
                var result = await _api.GetAsync<PagingResponse<MembershipPlanDto>>(
                    $"tenant/{companyId}/membership-plans?page=1&pageSize=100");

                return result?.Items ?? new List<MembershipPlanDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipService.GetPlans] {ex.Message}");
                return new List<MembershipPlanDto>();
            }
        }

        // ==================== MEMBERSHIPS ====================

        /// <summary>Lists memberships (paged) for a company.</summary>
        public async Task<List<MembershipResponseDto>> GetAllAsync(int companyId, int page = 1, int pageSize = 50)
        {
            try
            {
                var result = await _api.GetAsync<PagingResponse<MembershipResponseDto>>(
                    $"tenant/{companyId}/memberships?page={page}&pageSize={pageSize}");

                return result?.Items ?? new List<MembershipResponseDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipService.GetAll] {ex.Message}");
                return new List<MembershipResponseDto>();
            }
        }

        /// <summary>Fetches a single membership by ID.</summary>
        public async Task<MembershipResponseDto?> GetByIdAsync(int companyId, int id)
        {
            try
            {
                return await _api.GetAsync<MembershipResponseDto>(
                    $"tenant/{companyId}/memberships/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipService.GetById] {ex.Message}");
                return null;
            }
        }

        /// <summary>Creates a membership (enrolls a customer in a plan).</summary>
        public async Task<MembershipResponseDto?> CreateAsync(int companyId, MembershipCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<MembershipCreateRequest, MembershipResponseDto>(
                    $"tenant/{companyId}/memberships", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipService.Create] {ex.Message}");
                return null;
            }
        }

        /// <summary>Updates a membership (status, end date, points).</summary>
        public async Task<MembershipResponseDto?> UpdateAsync(int companyId, int id, MembershipUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<MembershipUpdateRequest, MembershipResponseDto>(
                    $"tenant/{companyId}/memberships/{id}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipService.Update] {ex.Message}");
                return null;
            }
        }

        /// <summary>Soft-cancels a membership.</summary>
        public async Task<bool> CancelAsync(int companyId, int id)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/memberships/{id}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MembershipService.Cancel] {ex.Message}");
                return false;
            }
        }
    }
}