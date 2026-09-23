using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Service for managing memberships and membership plans.
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Create a new membership for a customer.
        /// </summary>
        Task<Membership> CreateMembershipAsync(int companyId, int customerId, int membershipPlanId, DateTime startDate, DateTime? endDate = null);

        /// <summary>
        /// Get a membership by ID.
        /// </summary>
        Task<Membership?> GetMembershipAsync(int companyId, int membershipId);

        /// <summary>
        /// Get active membership for a customer.
        /// </summary>
        Task<Membership?> GetActiveMembershipAsync(int companyId, int customerId);

        /// <summary>
        /// Cancel a membership.
        /// </summary>
        Task<bool> CancelMembershipAsync(int companyId, int membershipId);

        /// <summary>
        /// Renew an expired or expiring membership.
        /// </summary>
        Task<Membership?> RenewMembershipAsync(int companyId, int membershipId, DateTime newEndDate);

        /// <summary>
        /// Get membership plan details.
        /// </summary>
        Task<MembershipPlan?> GetMembershipPlanAsync(int companyId, int planId);

        /// <summary>
        /// Get all active membership plans.
        /// </summary>
        Task<List<MembershipPlan>> GetActiveMembershipPlansAsync(int companyId);

        /// <summary>
        /// Check if customer has active membership.
        /// </summary>
        Task<bool> HasActiveMembershipAsync(int companyId, int customerId);
    }
}
