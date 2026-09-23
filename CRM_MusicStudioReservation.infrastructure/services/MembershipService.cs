using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Implementation of IMembershipService for managing memberships.
    /// </summary>
    public class MembershipService : IMembershipService
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public MembershipService(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<Membership> CreateMembershipAsync(int companyId, int customerId, int membershipPlanId, DateTime startDate, DateTime? endDate = null)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            // Verify customer and plan exist
            var customerExists = await db.Customers.AnyAsync(c => c.CustomerId == customerId);
            var planExists = await db.MembershipPlans.AnyAsync(p => p.MembershipPlanId == membershipPlanId);

            if (!customerExists || !planExists)
                throw new InvalidOperationException("Customer or membership plan not found");

            var membership = new Membership
            {
                CustomerId = customerId,
                MembershipPlanId = membershipPlanId,
                StartDate = startDate,
                EndDate = endDate,
                MembershipStatus = MembershipStatus.Active,
                LoyaltyPoints = 0
            };

            db.Memberships.Add(membership);
            await db.SaveChangesAsync();

            return membership;
        }

        public async Task<Membership?> GetMembershipAsync(int companyId, int membershipId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            return await db.Memberships.AsNoTracking().FirstOrDefaultAsync(m => m.MembershipId == membershipId);
        }

        public async Task<Membership?> GetActiveMembershipAsync(int companyId, int customerId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var now = DateTime.UtcNow;
            return await db.Memberships
                .AsNoTracking()
                .FirstOrDefaultAsync(m => 
                    m.CustomerId == customerId && 
                    m.MembershipStatus == MembershipStatus.Active &&
                    m.StartDate <= now &&
                    (m.EndDate == null || m.EndDate > now));
        }

        public async Task<bool> CancelMembershipAsync(int companyId, int membershipId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var membership = await db.Memberships.FirstOrDefaultAsync(m => m.MembershipId == membershipId);
            if (membership == null)
                return false;

            membership.MembershipStatus = MembershipStatus.Cancelled;
            db.Memberships.Update(membership);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<Membership?> RenewMembershipAsync(int companyId, int membershipId, DateTime newEndDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var membership = await db.Memberships.FirstOrDefaultAsync(m => m.MembershipId == membershipId);
            if (membership == null)
                return null;

            membership.EndDate = newEndDate;
            membership.MembershipStatus = MembershipStatus.Active;
            db.Memberships.Update(membership);
            await db.SaveChangesAsync();

            return membership;
        }

        public async Task<MembershipPlan?> GetMembershipPlanAsync(int companyId, int planId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            return await db.MembershipPlans.AsNoTracking().FirstOrDefaultAsync(p => p.MembershipPlanId == planId);
        }

        public async Task<List<MembershipPlan>> GetActiveMembershipPlansAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.MembershipPlans
                .AsNoTracking()
                .Where(p => p.IsActive)
                .OrderBy(p => p.MembershipPlanId)
                .ToListAsync();
        }

        public async Task<bool> HasActiveMembershipAsync(int companyId, int customerId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var now = DateTime.UtcNow;
            return await db.Memberships.AnyAsync(m =>
                m.CustomerId == customerId &&
                m.MembershipStatus == MembershipStatus.Active &&
                m.StartDate <= now &&
                (m.EndDate == null || m.EndDate > now));
        }
    }
}
