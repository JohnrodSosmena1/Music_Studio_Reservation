using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class SuperAdminService
    {
        private readonly ApiClient _api;

        public SuperAdminService(ApiClient api)
        {
            _api = api;
        }

        // ==================== DASHBOARD ====================

        public async Task<SuperAdminDashboardDto?> GetDashboardAsync()
        {
            try
            {
                return await _api.GetAsync<SuperAdminDashboardDto>("superadmin/dashboard");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.GetDashboard] {ex.Message}");
                return null;
            }
        }

        // ==================== ORGANIZATIONS ====================

        public async Task<OrganizationPagedResponse?> GetOrganizationsAsync(
            string? search = null,
            string? status = null,
            int? planId = null,
            int page = 1,
            int pageSize = 20)
        {
            try
            {
                var query = $"superadmin/organizations?page={page}&pageSize={pageSize}";
                if (!string.IsNullOrWhiteSpace(search)) query += $"&search={Uri.EscapeDataString(search)}";
                if (!string.IsNullOrWhiteSpace(status)) query += $"&status={Uri.EscapeDataString(status)}";
                if (planId.HasValue && planId.Value > 0) query += $"&planId={planId.Value}";

                return await _api.GetAsync<OrganizationPagedResponse>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.GetOrganizations] {ex.Message}");
                return null;
            }
        }

        public async Task<OrganizationDetailDto?> GetOrganizationDetailAsync(int id)
        {
            try
            {
                return await _api.GetAsync<OrganizationDetailDto>($"superadmin/organizations/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.GetOrganizationDetail] {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateOrganizationAsync(OrganizationCreateDto dto)
        {
            try
            {
                var res = await _api.PostAsync<OrganizationCreateDto, object>("superadmin/organizations", dto);
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.CreateOrganization] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateOrganizationAsync(int id, OrganizationUpdateDto dto)
        {
            try
            {
                var res = await _api.PutAsync<OrganizationUpdateDto, object>($"superadmin/organizations/{id}", dto);
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.UpdateOrganization] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActivateOrganizationAsync(int id)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/organizations/{id}/activate", new { });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.ActivateOrganization] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeactivateOrganizationAsync(int id)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/organizations/{id}/deactivate", new { });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.DeactivateOrganization] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SuspendOrganizationAsync(int id)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/organizations/{id}/suspend", new { });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.SuspendOrganization] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ArchiveOrganizationAsync(int id)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/organizations/{id}/archive", new { });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.ArchiveOrganization] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SoftDeleteOrganizationAsync(int id)
        {
            try
            {
                await _api.DeleteAsync($"superadmin/organizations/{id}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.SoftDeleteOrganization] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> HardDeleteOrganizationAsync(int id, string confirmationPhrase)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/organizations/{id}/hard-delete", new { confirmationPhrase });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.HardDeleteOrganization] {ex.Message}");
                return false;
            }
        }

        // ==================== SUBSCRIPTION PLANS ====================

        public async Task<List<SubscriptionPlanDto>> GetPlansAsync()
        {
            try
            {
                var list = await _api.GetAsync<List<SubscriptionPlanDto>>("superadmin/subscription-plans");
                return list ?? new List<SubscriptionPlanDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.GetPlans] {ex.Message}");
                return new List<SubscriptionPlanDto>();
            }
        }

        public async Task<bool> CreatePlanAsync(SubscriptionPlanCreateUpdateDto dto)
        {
            try
            {
                var res = await _api.PostAsync<SubscriptionPlanCreateUpdateDto, object>("superadmin/subscription-plans", dto);
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.CreatePlan] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdatePlanAsync(int id, SubscriptionPlanCreateUpdateDto dto)
        {
            try
            {
                var res = await _api.PutAsync<SubscriptionPlanCreateUpdateDto, object>($"superadmin/subscription-plans/{id}", dto);
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.UpdatePlan] {ex.Message}");
                return false;
            }
        }

        // ==================== SUBSCRIPTIONS & INVOICES ====================

        public async Task<List<SubscriptionListItemDto>> GetSubscriptionsAsync(string? status = null)
        {
            try
            {
                var url = "superadmin/subscriptions";
                if (!string.IsNullOrWhiteSpace(status)) url += $"?status={Uri.EscapeDataString(status)}";
                var list = await _api.GetAsync<List<SubscriptionListItemDto>>(url);
                return list ?? new List<SubscriptionListItemDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.GetSubscriptions] {ex.Message}");
                return new List<SubscriptionListItemDto>();
            }
        }

        public async Task<bool> AssignPlanAsync(AssignSubscriptionDto dto)
        {
            try
            {
                var res = await _api.PostAsync<AssignSubscriptionDto, object>("superadmin/subscriptions/assign", dto);
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.AssignPlan] {ex.Message}");
                return false;
            }
        }

        public async Task<List<SubscriptionInvoiceDto>> GetInvoicesAsync(int? companyId = null, string? status = null)
        {
            try
            {
                var url = "superadmin/subscriptions/invoices?";
                if (companyId.HasValue && companyId.Value > 0) url += $"companyId={companyId.Value}&";
                if (!string.IsNullOrWhiteSpace(status)) url += $"status={Uri.EscapeDataString(status)}";

                var list = await _api.GetAsync<List<SubscriptionInvoiceDto>>(url);
                return list ?? new List<SubscriptionInvoiceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.GetInvoices] {ex.Message}");
                return new List<SubscriptionInvoiceDto>();
            }
        }

        public async Task<bool> MarkInvoicePaidAsync(int id)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/subscriptions/invoices/{id}/mark-paid", new { });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.MarkInvoicePaid] {ex.Message}");
                return false;
            }
        }

        // ==================== PLATFORM TERMS & CONDITIONS ====================

        public async Task<List<PlatformTandCDto>> GetTermsAsync(string? type = null, string? status = null)
        {
            try
            {
                var url = "superadmin/terms?";
                if (!string.IsNullOrWhiteSpace(type)) url += $"type={Uri.EscapeDataString(type)}&";
                if (!string.IsNullOrWhiteSpace(status)) url += $"status={Uri.EscapeDataString(status)}";

                var list = await _api.GetAsync<List<PlatformTandCDto>>(url);
                return list ?? new List<PlatformTandCDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.GetTerms] {ex.Message}");
                return new List<PlatformTandCDto>();
            }
        }

        public async Task<bool> CreateDraftTermsAsync(PlatformTandCCreateDto dto)
        {
            try
            {
                var res = await _api.PostAsync<PlatformTandCCreateDto, object>("superadmin/terms", dto);
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.CreateDraftTerms] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateDraftTermsAsync(int id, PlatformTandCUpdateDto dto)
        {
            try
            {
                var res = await _api.PutAsync<PlatformTandCUpdateDto, object>($"superadmin/terms/{id}", dto);
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.UpdateDraftTerms] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PublishTermsAsync(int id)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/terms/{id}/publish", new { });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.PublishTerms] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ArchiveTermsAsync(int id)
        {
            try
            {
                var res = await _api.PostAsync<object, object>($"superadmin/terms/{id}/archive", new { });
                return res != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminService.ArchiveTerms] {ex.Message}");
                return false;
            }
        }
    }
}
