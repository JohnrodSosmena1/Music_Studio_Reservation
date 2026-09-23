using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Implementation of IAuditService for logging actions to the master database.
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly MasterCRMDbContext _dbContext;

        public AuditService(MasterCRMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogActionAsync(int companyId, string userId, string userEmail, string userRole, string action,
            string entityName, int entityId, string? oldValue = null, string? newValue = null, string? ipAddress = null)
        {
            var auditLog = new AuditLog
            {
                CompanyId = companyId,
                UserId = userId,
                UserEmail = userEmail,
                UserRole = userRole,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                OldValue = oldValue,
                NewValue = newValue,
                IpAddress = ipAddress,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.AuditLogs.Add(auditLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetAuditLogsAsync(int companyId, int skip = 0, int take = 20)
        {
            return await _dbContext.AuditLogs
                .AsNoTracking()
                .Where(al => al.CompanyId == companyId)
                .OrderByDescending(al => al.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<AuditLog>> GetEntityAuditLogsAsync(int companyId, string entityName, int entityId)
        {
            return await _dbContext.AuditLogs
                .AsNoTracking()
                .Where(al => al.CompanyId == companyId && al.EntityName == entityName && al.EntityId == entityId)
                .OrderByDescending(al => al.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetAuditLogCountAsync(int companyId)
        {
            return await _dbContext.AuditLogs
                .Where(al => al.CompanyId == companyId)
                .CountAsync();
        }
    }
}
