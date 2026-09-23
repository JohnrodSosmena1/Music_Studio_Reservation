using System;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Implementation of ILoyaltyService for managing loyalty points.
    /// </summary>
    public class LoyaltyService : ILoyaltyService
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public LoyaltyService(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<int> GetLoyaltyBalanceAsync(int companyId, int customerId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var membership = await db.Memberships
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CustomerId == customerId);

            return membership?.LoyaltyPoints ?? 0;
        }

        public async Task AddLoyaltyPointsAsync(int companyId, int customerId, int points, string description = "Manual adjustment")
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            // Create loyalty transaction
            var transaction = new LoyaltyTransaction
            {
                CustomerId = customerId,
                Points = points,
                TransactionType = LoyaltyTransactionType.Earned,
                Description = description
            };

            db.LoyaltyTransactions.Add(transaction);

            // Update membership loyalty points
            var membership = await db.Memberships.FirstOrDefaultAsync(m => m.CustomerId == customerId);
            if (membership != null)
            {
                membership.LoyaltyPoints += points;
                db.Memberships.Update(membership);
            }

            await db.SaveChangesAsync();
        }

        public async Task<bool> DeductLoyaltyPointsAsync(int companyId, int customerId, int points, string description = "Points redeemed")
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var membership = await db.Memberships.FirstOrDefaultAsync(m => m.CustomerId == customerId);
            if (membership == null || membership.LoyaltyPoints < points)
                return false;

            // Create loyalty transaction
            var transaction = new LoyaltyTransaction
            {
                CustomerId = customerId,
                Points = -points,
                TransactionType = LoyaltyTransactionType.Redeemed,
                Description = description
            };

            db.LoyaltyTransactions.Add(transaction);

            // Deduct from membership
            membership.LoyaltyPoints -= points;
            db.Memberships.Update(membership);

            await db.SaveChangesAsync();
            return true;
        }

        public async Task AwardBookingPointsAsync(int companyId, int customerId, int bookingId, int pointsToAward)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            // Create loyalty transaction
            var transaction = new LoyaltyTransaction
            {
                CustomerId = customerId,
                Points = pointsToAward,
                TransactionType = LoyaltyTransactionType.Earned,
                Description = $"Booking #{bookingId} completed"
            };

            db.LoyaltyTransactions.Add(transaction);

            // Update membership loyalty points
            var membership = await db.Memberships.FirstOrDefaultAsync(m => m.CustomerId == customerId);
            if (membership != null)
            {
                membership.LoyaltyPoints += pointsToAward;
                db.Memberships.Update(membership);
            }

            await db.SaveChangesAsync();
        }

        public async Task<bool> ApplyLoyaltyDiscountAsync(int companyId, int customerId, int pointsToRedeem)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var membership = await db.Memberships.FirstOrDefaultAsync(m => m.CustomerId == customerId);
            if (membership == null || membership.LoyaltyPoints < pointsToRedeem)
                return false;

            // Create loyalty transaction
            var transaction = new LoyaltyTransaction
            {
                CustomerId = customerId,
                Points = -pointsToRedeem,
                TransactionType = LoyaltyTransactionType.Redeemed,
                Description = "Loyalty points applied as discount"
            };

            db.LoyaltyTransactions.Add(transaction);

            // Deduct from membership
            membership.LoyaltyPoints -= pointsToRedeem;
            db.Memberships.Update(membership);

            await db.SaveChangesAsync();
            return true;
        }
    }
}
