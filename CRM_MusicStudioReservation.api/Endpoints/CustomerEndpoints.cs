using System.Security.Claims;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.DTOs;
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

            // 👇 Loyalty points endpoint
            group.MapGet("/loyalty", GetCustomerLoyalty);
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

            var action = "Update";
            if (dto.IsActive.HasValue && dto.IsActive.Value != oldIsActive)
                action = dto.IsActive.Value ? "Activate" : "Deactivate";

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

        // ==================== LOYALTY (derived + adjustment) ====================
        // Rule: every non-cancelled booking = 5 points by default.
        //       If the customer has an Active membership, use the plan's LoyaltyPointsPerBooking.
        //       Plus any stored adjustment from Membership.LoyaltyPoints (admin override).
        private static async Task<IResult> GetCustomerLoyalty(
            int companyId,
            ITenantDbContextFactory factory)
        {
            await using var db = await factory.CreateAsync(companyId);

            // Enum aliases for clarity
            var cancelledStatus = CRM_MusicStudioReservation.domain.enums.BookingStatus.Cancelled;
            var activeMembership = CRM_MusicStudioReservation.domain.enums.MembershipStatus.Active;

            // 1. All customers
            var customers = await db.Customers.AsNoTracking().ToListAsync();

            // 2. Booking counts per customer (exclude Cancelled)
            var bookingCounts = await db.Bookings
                .AsNoTracking()
                .Where(b => b.BookingStatus != cancelledStatus)
                .GroupBy(b => b.CustomerId)
                .Select(g => new { CustomerId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CustomerId, x => x.Count);

            // 3. Active memberships with their plans
            var memberships = await db.Memberships
                .AsNoTracking()
                .Include(m => m.MembershipPlan)
                .Where(m => m.MembershipStatus == activeMembership)
                .ToListAsync();

            // 4. Build response
            var results = customers.Select(c =>
            {
                var bookingCount = bookingCounts.TryGetValue(c.CustomerId, out var cnt) ? cnt : 0;
                var membership = memberships.FirstOrDefault(m => m.CustomerId == c.CustomerId);

                int pointsPerBooking = 5;   // default for non-members
                string? planName = null;
                int? planId = null;
                int? membershipId = null;
                int adjustment = 0;
                string status = "None";

                if (membership?.MembershipPlan != null && membership.MembershipPlan.IsActive)
                {
                    pointsPerBooking = membership.MembershipPlan.LoyaltyPointsPerBooking;
                    planName = membership.MembershipPlan.PlanName;
                    planId = membership.MembershipPlanId;
                    membershipId = membership.MembershipId;
                    adjustment = membership.LoyaltyPoints;   // 👈 stored admin adjustment
                    status = membership.MembershipStatus.ToString();
                }

                var fromBookings = bookingCount * pointsPerBooking;
                var totalPoints = fromBookings + adjustment;

                return new CustomerLoyaltyDto
                {
                    CustomerId = c.CustomerId,
                    CustomerCode = c.CustomerCode ?? string.Empty,
                    CustomerName = c.CustomerName ?? string.Empty,
                    Email = c.EmailAddress,
                    ContactNumber = c.ContactNumber,
                    TotalBookings = bookingCount,
                    MembershipId = membershipId,
                    MembershipPlanId = planId,
                    PlanName = planName,
                    PointsPerBooking = pointsPerBooking,
                    PointsFromBookings = fromBookings,
                    PointsAdjustment = adjustment,
                    TotalPointsEarned = totalPoints,
                    PointsBalance = totalPoints,
                    MembershipStatus = status
                };
            })
            .OrderByDescending(x => x.TotalPointsEarned)
            .ToList();

            return Results.Ok(results);
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