using System;
using System.Collections.Generic;
using System.Text;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public class TenantDbContextFactory : ITenantDbContextFactory
    {
        private readonly ITenantDatabaseResolver _resolver;
        private readonly IConfiguration _configuration;

        public TenantDbContextFactory(
            ITenantDatabaseResolver resolver,
            IConfiguration configuration)
        {
            _resolver = resolver;
            _configuration = configuration;
        }

        public async Task<TenantCRMDbContext> CreateAsync(int companyId)
        {
            var databaseInfo = await _resolver.GetDatabaseInfoAsync(companyId);

            if (databaseInfo.ServerName.Contains("localdb", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(databaseInfo.CredentialKey, "LocalDB", StringComparison.OrdinalIgnoreCase))
            {
                var localConnString =
                    $"Server={databaseInfo.ServerName};" +
                    $"Database={databaseInfo.DatabaseName};" +
                    $"Integrated Security=True;" +
                    $"TrustServerCertificate=True;" +
                    $"MultipleActiveResultSets=True;";

                var localOptions = new DbContextOptionsBuilder<TenantCRMDbContext>()
                    .UseSqlServer(localConnString)
                    .Options;

                return new TenantCRMDbContext(localOptions);
            }

            var userId = _configuration[
                $"TenantCredentials:{databaseInfo.CredentialKey}:UserId"];

            var password = _configuration[
                $"TenantCredentials:{databaseInfo.CredentialKey}:Password"];

            if (string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    $"Credentials not found for key '{databaseInfo.CredentialKey}'.");
            }

            var connectionString =
                $"Server={databaseInfo.ServerName};" +
                $"Database={databaseInfo.DatabaseName};" +
                $"User Id={userId};" +
                $"Password={password};" +
                $"Encrypt=True;" +
                $"TrustServerCertificate=True;" +
                $"MultipleActiveResultSets=True;";

            var options = new DbContextOptionsBuilder<TenantCRMDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new TenantCRMDbContext(options);
        }
    }
}
