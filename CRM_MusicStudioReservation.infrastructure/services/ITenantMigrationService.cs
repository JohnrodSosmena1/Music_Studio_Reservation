using System.Threading.Tasks;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public interface ITenantMigrationService
    {
        /// <summary>
        /// Applies pending EF Core migrations to all active tenant databases.
        /// </summary>
        Task ApplyMigrationsAsync();
    }
}
