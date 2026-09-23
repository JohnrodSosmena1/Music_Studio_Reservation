using System;
using System.Threading.Tasks;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Service for managing loyalty points and transactions.
    /// </summary>
    public interface ILoyaltyService
    {
        /// <summary>
        /// Get the current loyalty points balance for a customer.
        /// </summary>
        Task<int> GetLoyaltyBalanceAsync(int companyId, int customerId);

        /// <summary>
        /// Add loyalty points to a customer's account.
        /// </summary>
        Task AddLoyaltyPointsAsync(int companyId, int customerId, int points, string description = "Manual adjustment");

        /// <summary>
        /// Deduct loyalty points from a customer's account.
        /// </summary>
        Task<bool> DeductLoyaltyPointsAsync(int companyId, int customerId, int points, string description = "Points redeemed");

        /// <summary>
        /// Award loyalty points for a booking.
        /// </summary>
        Task AwardBookingPointsAsync(int companyId, int customerId, int bookingId, int pointsToAward);

        /// <summary>
        /// Apply loyalty points as a discount to a booking.
        /// </summary>
        Task<bool> ApplyLoyaltyDiscountAsync(int companyId, int customerId, int pointsToRedeem);
    }
}
