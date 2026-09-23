using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public interface ITenantDatabaseResolver
    {
        Task<TenantDatabaseInfo> GetDatabaseInfoAsync(int companyId);
    }
}