using System;
using System.Linq;
using System.Threading.Tasks;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public class TenantMigrationService : ITenantMigrationService
    {
        private readonly MasterCRMDbContext _masterDb;
        private readonly ITenantDbContextFactory _tenantFactory;

        public TenantMigrationService(
            MasterCRMDbContext masterDb,
            ITenantDbContextFactory tenantFactory)
        {
            _masterDb = masterDb;
            _tenantFactory = tenantFactory;
        }

        public async Task ApplyMigrationsAsync()
        {
            var companyDbs = await _masterDb.CompanyDatabases
                .AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();

            foreach (var db in companyDbs)
            {
                try
                {
                    var tenantDb = await _tenantFactory.CreateAsync(db.CompanyId);
                    await tenantDb.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    // Log and continue with next tenant
                    Console.WriteLine($"Failed to migrate tenant {db.CompanyId}: {ex.Message}");
                }
            }
        }
    }
}
