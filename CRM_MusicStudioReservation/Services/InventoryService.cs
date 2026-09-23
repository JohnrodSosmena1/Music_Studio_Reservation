using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.winforms.DTOs;

namespace CRM.winforms.Services
{
    public class InventoryService
    {
        private readonly ApiClient _api;

        public InventoryService(ApiClient api)
        {
            _api = api;
        }

        // ==================== CATEGORIES ====================

        public async Task<List<InventoryCategoryDto>> GetCategoriesAsync(int companyId)
        {
            try
            {
                var result = await _api.GetAsync<PagedResponse<InventoryCategoryDto>>(
                    $"tenant/{companyId}/inventory-categories?pageSize=100");
                return result?.Items ?? new List<InventoryCategoryDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InventoryService.GetCategories] {ex.Message}");
                return new List<InventoryCategoryDto>();
            }
        }

        // ==================== ITEMS ====================

        public async Task<List<InventoryItemDto>> GetItemsAsync(int companyId, string? search = null)
        {
            try
            {
                var url = $"tenant/{companyId}/inventory-items?pageSize=500";
                if (!string.IsNullOrWhiteSpace(search))
                    url += $"&search={Uri.EscapeDataString(search)}";

                var result = await _api.GetAsync<PagedResponse<InventoryItemDto>>(url);
                return result?.Items ?? new List<InventoryItemDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InventoryService.GetItems] {ex.Message}");
                return new List<InventoryItemDto>();
            }
        }

        public async Task<InventoryItemDto?> GetItemAsync(int companyId, int itemId)
        {
            try
            {
                return await _api.GetAsync<InventoryItemDto>(
                    $"tenant/{companyId}/inventory-items/{itemId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InventoryService.GetItem] {ex.Message}");
                return null;
            }
        }

        public async Task<InventoryItemDto?> CreateItemAsync(int companyId, InventoryItemCreateRequest request)
        {
            try
            {
                return await _api.PostAsync<InventoryItemCreateRequest, InventoryItemDto>(
                    $"tenant/{companyId}/inventory-items", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InventoryService.CreateItem] {ex.Message}");
                return null;
            }
        }

        public async Task<InventoryItemDto?> UpdateItemAsync(int companyId, int itemId, InventoryItemUpdateRequest request)
        {
            try
            {
                return await _api.PutAsync<InventoryItemUpdateRequest, InventoryItemDto>(
                    $"tenant/{companyId}/inventory-items/{itemId}", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InventoryService.UpdateItem] {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteItemAsync(int companyId, int itemId)
        {
            try
            {
                await _api.DeleteAsync($"tenant/{companyId}/inventory-items/{itemId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InventoryService.DeleteItem] {ex.Message}");
                return false;
            }
        }

        public async Task<InventoryItemDto?> AdjustStockAsync(int companyId, int itemId, int delta, string? notes = null)
        {
            try
            {
                var request = new StockAdjustmentRequest { Delta = delta, Notes = notes };
                return await _api.PostAsync<StockAdjustmentRequest, InventoryItemDto>(
                    $"tenant/{companyId}/inventory-items/{itemId}/adjust-stock", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InventoryService.AdjustStock] {ex.Message}");
                return null;
            }
        }
    }
}