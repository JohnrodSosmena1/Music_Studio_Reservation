using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.api.Helpers;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/customers")
                .RequireAuthorization(p => p.RequireRole("SuperAdmin", "Admin", "Staff"));

            group.MapGet("", GetAllCustomers);
            group.MapGet("/{id:int}", GetCustomerById);
            group.MapPost("", CreateCustomer);
            group.MapPut("/{id:int}", UpdateCustomer);
            group.MapDelete("/{id:int}", DeleteCustomer);
            group.MapGet("/loyalty", GetCustomerLoyalty);
        }

        // ==================== GET ALL CUSTOMERS ====================
        private static async Task<IResult> GetAllCustomers(
            int companyId,
            string? search,
            ITenantDbContextFactory factory)
        {
            await using var db = await factory.CreateAsync(companyId);

            // If empty, auto-seed sample customers for immediate demo & testing
            if (!await db.Customers.AnyAsync())
            {
                var seedCustomers = new List<Customer>
                {
                    new Customer
                    {
                        CustomerCode = "CUST-00001",
                        FirstName = "John",
                        LastName = "Lennon",
                        CustomerName = "John Lennon",
                        ContactNumber = "+1-555-0101",
                        EmailAddress = "john.lennon@abbeyroad.com",
                        Address = "3 Savile Row, London",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-60),
                        UpdatedAt = DateTime.UtcNow.AddDays(-60)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00002",
                        FirstName = "Paul",
                        LastName = "McCartney",
                        CustomerName = "Paul McCartney",
                        ContactNumber = "+1-555-0102",
                        EmailAddress = "paul.mccartney@mpl.com",
                        Address = "7 Cavendish Avenue, London",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-45),
                        UpdatedAt = DateTime.UtcNow.AddDays(-45)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00003",
                        FirstName = "George",
                        LastName = "Harrison",
                        CustomerName = "George Harrison",
                        ContactNumber = "+1-555-0103",
                        EmailAddress = "george.harrison@darkhorse.com",
                        Address = "Friar Park, Henley-on-Thames",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-30),
                        UpdatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00004",
                        FirstName = "Ringo",
                        LastName = "Starr",
                        CustomerName = "Ringo Starr",
                        ContactNumber = "+1-555-0104",
                        EmailAddress = "ringo.starr@allstarr.com",
                        Address = "Cranleigh, Surrey",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-20),
                        UpdatedAt = DateTime.UtcNow.AddDays(-20)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00005",
                        FirstName = "Freddie",
                        LastName = "Mercury",
                        CustomerName = "Freddie Mercury",
                        ContactNumber = "+1-555-0105",
                        EmailAddress = "freddie.mercury@queenonline.com",
                        Address = "Garden Lodge, Logan Place, Kensington",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-15),
                        UpdatedAt = DateTime.UtcNow.AddDays(-15)
                    }
                };

                db.Customers.AddRange(seedCustomers);
                await db.SaveChangesAsync();
            }

            var query = db.Customers.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLowerInvariant();
                query = query.Where(c => 
                    c.CustomerCode.ToLower().Contains(s) ||
                    c.FirstName.ToLower().Contains(s) ||
                    c.LastName.ToLower().Contains(s) ||
                    c.CustomerName.ToLower().Contains(s) ||
                    (c.EmailAddress != null && c.EmailAddress.ToLower().Contains(s)) ||
                    (c.ContactNumber != null && c.ContactNumber.ToLower().Contains(s)));
            }

            var customers = await query
                .OrderBy(c => c.CustomerId)
                .Select(c => MapToResponse(c))
                .ToListAsync();

            return Results.Ok(customers);
        }

        // ==================== GET BY ID ====================
        private static async Task<IResult> GetCustomerById(
            int companyId,
            int id,
            ITenantDbContextFactory factory)
        {
            await using var db = await factory.CreateAsync(companyId);

            var customer = await db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer is null)
                return Results.NotFound(new { message = $"Customer {id} not found." });

            return Results.Ok(MapToResponse(customer));
        }

        // ==================== CREATE ====================
        private static async Task<IResult> CreateCustomer(
            int companyId,
            CustomerCreateDto dto,
            ITenantDbContextFactory factory,
            IAuditService auditService,
            HttpContext httpContext,
            ClaimsPrincipal user)
        {
            // Server-side validation
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                return Results.BadRequest(new { message = "First Name is required." });

            if (string.IsNullOrWhiteSpace(dto.LastName))
                return Results.BadRequest(new { message = "Last Name is required." });

            if (!string.IsNullOrWhiteSpace(dto.ContactNumber) && !IsValidContact(dto.ContactNumber))
                return Results.BadRequest(new { message = "Contact Number must contain digits only (symbols allowed: +, -, space, brackets)." });

            if (!string.IsNullOrWhiteSpace(dto.EmailAddress) && !IsValidEmail(dto.EmailAddress))
                return Results.BadRequest(new { message = "Email Address must be a valid email address." });

            await using var db = await factory.CreateAsync(companyId);

            // Auto-generate sequential CustomerCode (e.g., CUST-00001)
            var count = await db.Customers.CountAsync();
            var code = $"CUST-{(count + 1):D5}";
            while (await db.Customers.AnyAsync(c => c.CustomerCode == code))
            {
                count++;
                code = $"CUST-{(count + 1):D5}";
            }

            var customer = new Customer
            {
                CustomerCode = code,
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                CustomerName = $"{dto.FirstName.Trim()} {dto.LastName.Trim()}".Trim(),
                ContactNumber = string.IsNullOrWhiteSpace(dto.ContactNumber) ? null : dto.ContactNumber.Trim(),
                EmailAddress = string.IsNullOrWhiteSpace(dto.EmailAddress) ? null : dto.EmailAddress.Trim(),
                Address = string.IsNullOrWhiteSpace(dto.Address) ? null : dto.Address.Trim(),
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            // Audit log
            await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                action: "Create",
                entityName: "Customer",
                entityId: customer.CustomerId,
                newValue: $"Code={customer.CustomerCode}, Name={customer.CustomerName}, Active={customer.IsActive}");

            return Results.Created($"/tenant/{companyId}/customers/{customer.CustomerId}", MapToResponse(customer));
        }

        // ==================== UPDATE ====================
        private static async Task<IResult> UpdateCustomer(
            int companyId,
            int id,
            CustomerUpdateDto dto,
            ITenantDbContextFactory factory,
            IAuditService auditService,
            HttpContext httpContext,
            ClaimsPrincipal user)
        {
            if (!string.IsNullOrWhiteSpace(dto.ContactNumber) && !IsValidContact(dto.ContactNumber))
                return Results.BadRequest(new { message = "Contact Number must contain digits only." });

            if (!string.IsNullOrWhiteSpace(dto.EmailAddress) && !IsValidEmail(dto.EmailAddress))
                return Results.BadRequest(new { message = "Email Address must be a valid email address." });

            await using var db = await factory.CreateAsync(companyId);

            var customer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer is null)
                return Results.NotFound(new { message = $"Customer {id} not found." });

            var oldSnapshot = $"Code={customer.CustomerCode}, Name={customer.CustomerName}, Active={customer.IsActive}";
            var oldIsActive = customer.IsActive;

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                customer.FirstName = dto.FirstName.Trim();

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                customer.LastName = dto.LastName.Trim();

            customer.CustomerName = $"{customer.FirstName} {customer.LastName}".Trim();

            if (dto.ContactNumber != null)
                customer.ContactNumber = string.IsNullOrWhiteSpace(dto.ContactNumber) ? null : dto.ContactNumber.Trim();

            if (dto.EmailAddress != null)
                customer.EmailAddress = string.IsNullOrWhiteSpace(dto.EmailAddress) ? null : dto.EmailAddress.Trim();

            if (dto.Address != null)
                customer.Address = string.IsNullOrWhiteSpace(dto.Address) ? null : dto.Address.Trim();

            if (dto.IsActive.HasValue)
                customer.IsActive = dto.IsActive.Value;

            customer.UpdatedAt = DateTime.UtcNow;

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

            return Results.Ok(MapToResponse(customer));
        }

        // ==================== DELETE (Soft) ====================
        private static async Task<IResult> DeleteCustomer(
            int companyId,
            int id,
            ITenantDbContextFactory factory,
            IAuditService auditService,
            HttpContext httpContext,
            ClaimsPrincipal user)
        {
            await using var db = await factory.CreateAsync(companyId);

            var customer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer is null)
                return Results.NotFound(new { message = $"Customer {id} not found." });

            customer.IsActive = false;
            customer.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();

            await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                action: "Deactivate",
                entityName: "Customer",
                entityId: customer.CustomerId,
                newValue: "Customer deactivated (soft delete)");

            return Results.NoContent();
        }

        // ==================== LOYALTY (derived + adjustment) ====================
        private static async Task<IResult> GetCustomerLoyalty(
            int companyId,
            ITenantDbContextFactory factory)
        {
            await using var db = await factory.CreateAsync(companyId);

            var cancelledStatus = CRM_MusicStudioReservation.domain.enums.BookingStatus.Cancelled;
            var activeMembership = CRM_MusicStudioReservation.domain.enums.MembershipStatus.Active;

            var customers = await db.Customers.AsNoTracking().ToListAsync();

            var bookingCounts = await db.Bookings
                .AsNoTracking()
                .Where(b => b.BookingStatus != cancelledStatus)
                .GroupBy(b => b.CustomerId)
                .Select(g => new { CustomerId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CustomerId, x => x.Count);

            var memberships = await db.Memberships
                .AsNoTracking()
                .Include(m => m.MembershipPlan)
                .Where(m => m.MembershipStatus == activeMembership)
                .ToListAsync();

            var results = customers.Select(c =>
            {
                var bookingCount = bookingCounts.TryGetValue(c.CustomerId, out var cnt) ? cnt : 0;
                var membership = memberships.FirstOrDefault(m => m.CustomerId == c.CustomerId);

                int pointsPerBooking = 5;
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
                    adjustment = membership.LoyaltyPoints;
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

        // ==================== HELPERS ====================
        private static CustomerResponseDto MapToResponse(Customer c) => new()
        {
            CustomerId = c.CustomerId,
            CustomerCode = c.CustomerCode,
            FirstName = c.FirstName,
            LastName = c.LastName,
            CustomerName = string.IsNullOrWhiteSpace(c.CustomerName) ? $"{c.FirstName} {c.LastName}".Trim() : c.CustomerName,
            ContactNumber = c.ContactNumber,
            EmailAddress = c.EmailAddress,
            Address = c.Address,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        };

        private static bool IsValidContact(string contact)
        {
            // Must contain at least 7 digits, allowed formatting symbols: +, -, (, ), space
            var digitsOnly = Regex.Replace(contact, @"[^\d]", "");
            return digitsOnly.Length >= 7 && Regex.IsMatch(contact, @"^[0-9\+\-\(\)\s]+$");
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }

    // ==================== DTOs ====================
    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CustomerCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ContactNumber { get; set; }

        [MaxLength(255)]
        public string? EmailAddress { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class CustomerUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
    }
}