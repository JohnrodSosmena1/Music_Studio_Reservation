using System;
using System.Collections.Generic;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    /// <summary>
    /// Handles CRUD operations for customers via the API.
    /// </summary>
    public class CustomerService
    {
        private readonly ApiClient _api;

        public CustomerService(ApiClient api)
        {
            _api = api;
        }

        /// <summary>Fetches all customers for a company.</summary>
        public async Task<List<CustomerDto>> GetAllAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<List<CustomerDto>>($"tenant/{companyId}/customers");
                return result ?? new List<CustomerDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CustomerService.GetAll] {ex.Message}");
                return new List<CustomerDto>();
            }
        }

        /// <summary>Fetches a single customer by ID.</summary>
        public async Task<CustomerDto?> GetByIdAsync(int companyId, int customerId)
        {
            try
            {
                return await _api.GetAsync<CustomerDto>($"tenant/{companyId}/customers/{customerId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CustomerService.GetById] {ex.Message}");
                return null;
            }
        }

        /// <summary>Creates a new customer.</summary>
        public async Task<CustomerDto?> CreateAsync(int companyId, CustomerCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<CustomerCreateRequest, CustomerDto>(
                    $"tenant/{companyId}/customers", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CustomerService.Create] {ex.Message}");
                return null;
            }
        }

        /// <summary>Updates an existing customer.</summary>
        public async Task<CustomerDto?> UpdateAsync(int companyId, int customerId, CustomerUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<CustomerUpdateRequest, CustomerDto>(
                    $"tenant/{companyId}/customers/{customerId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CustomerService.Update] {ex.Message}");
                return null;
            }
        }

        /// <summary>Deletes a customer (soft delete).</summary>
        public async Task<bool> DeleteAsync(int companyId, int customerId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/customers/{customerId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CustomerService.Delete] {ex.Message}");
                return false;
            }
        }

        /// <summary>Fetches loyalty points info for all customers (derived from bookings + membership plan).</summary>
        public async Task<List<CustomerLoyaltyDto>> GetLoyaltyAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<List<CustomerLoyaltyDto>>($"tenant/{companyId}/customers/loyalty");
                return result ?? new List<CustomerLoyaltyDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CustomerService.GetLoyalty] {ex.Message}");
                return new List<CustomerLoyaltyDto>();
            }
        }
    }
}