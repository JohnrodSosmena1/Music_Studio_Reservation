using System.Security.Claims;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.Helpers;
using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/customers");

            group.MapPut("/{id:int}", UpdateCustomer);
        }

        // ==================== UPDATE (also handles Enable/Disable) ====================
        private static async Task<IResult> UpdateCustomer(
            int companyId,
            int id,
            CustomerUpdateDto dto,
            ITenantDbContextFactory factory,
            IAuditService auditService,
            HttpContext httpContext,
            ClaimsPrincipal user)
        {
            await using var db = await factory.CreateAsync(companyId);

            var customer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer is null)
                return Results.NotFound(new { message = $"Customer {id} not found." });

            // 👇 Snapshot before changes
            var oldSnapshot = $"Code={customer.CustomerCode}, Name={customer.CustomerName}, Active={customer.IsActive}";
            var oldIsActive = customer.IsActive;

            if (!string.IsNullOrWhiteSpace(dto.CustomerCode))
                customer.CustomerCode = dto.CustomerCode;

            if (!string.IsNullOrWhiteSpace(dto.CustomerName))
                customer.CustomerName = dto.CustomerName;

            if (dto.ContactNumber != null)
                customer.ContactNumber = dto.ContactNumber;

            if (dto.EmailAddress != null)
                customer.EmailAddress = dto.EmailAddress;

            if (dto.Address != null)
                customer.Address = dto.Address;

            if (dto.IsActive.HasValue)
                customer.IsActive = dto.IsActive.Value;

            await db.SaveChangesAsync();

            // 👇 Determine action type
            var action = "Update";
            if (dto.IsActive.HasValue && dto.IsActive.Value != oldIsActive)
                action = dto.IsActive.Value ? "Activate" : "Deactivate";

            // 👇 AUDIT LOG
            await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                action: action,
                entityName: "Customer",
                entityId: customer.CustomerId,
                oldValue: oldSnapshot,
                newValue: $"Code={customer.CustomerCode}, Name={customer.CustomerName}, Active={customer.IsActive}");

            return Results.Ok(new
            {
                customerId = customer.CustomerId,
                customerCode = customer.CustomerCode,
                customerName = customer.CustomerName,
                contactNumber = customer.ContactNumber,
                emailAddress = customer.EmailAddress,
                address = customer.Address,
                isActive = customer.IsActive,
                createdAt = customer.CreatedAt
            });
        }
    }

    // ==================== DTO ====================
    public class CustomerUpdateDto
    {
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
    }
}