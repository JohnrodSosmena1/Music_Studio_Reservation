using System;
using System.Collections.Generic;
using System.Text;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public class TenantDatabaseResolver : ITenantDatabaseResolver
    {
        private readonly MasterCRMDbContext _masterDb;

        public TenantDatabaseResolver(MasterCRMDbContext masterDb)
        {
            _masterDb = masterDb;
        }

        public async Task<TenantDatabaseInfo> GetDatabaseInfoAsync(int companyId)
        {
            var tenantDatabase = await _masterDb.CompanyDatabases
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.CompanyId == companyId &&
                    x.IsActive);

            if (tenantDatabase == null)
            {
                throw new InvalidOperationException(
                    $"No active tenant database found for CompanyId {companyId}.");
            }

            return new TenantDatabaseInfo
            {
                ServerName = tenantDatabase.ServerName,
                DatabaseName = tenantDatabase.DatabaseName,
                CredentialKey = tenantDatabase.CredentialKey
            };
        }
    }
}