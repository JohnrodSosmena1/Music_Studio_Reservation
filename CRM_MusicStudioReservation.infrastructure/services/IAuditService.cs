using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Service for managing audit logs in the master database.
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// Log an action to the audit log.
        /// </summary>
        Task LogActionAsync(int companyId, string userId, string userEmail, string userRole, string action, 
            string entityName, int entityId, string? oldValue = null, string? newValue = null, string? ipAddress = null);

        /// <summary>
        /// Get audit logs for a company.
        /// </summary>
        Task<List<AuditLog>> GetAuditLogsAsync(int companyId, int skip = 0, int take = 20);

        /// <summary>
        /// Get audit logs for a specific entity.
        /// </summary>
        Task<List<AuditLog>> GetEntityAuditLogsAsync(int companyId, string entityName, int entityId);

        /// <summary>
        /// Get total count of audit logs for a company.
        /// </summary>
        Task<int> GetAuditLogCountAsync(int companyId);
    }
}
