using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRM_MusicStudioSystem.infrastructure.data;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public interface ITenantDbContextFactory
    {
        Task<TenantCRMDbContext> CreateAsync(int companyId);
    }
}
