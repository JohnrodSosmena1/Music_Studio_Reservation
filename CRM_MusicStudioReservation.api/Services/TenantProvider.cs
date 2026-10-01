using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CRM_MusicStudioReservation.api.Services
{
    public interface ITenantProvider
    {
        int? CurrentCompanyId { get; }
        bool IsSuperAdmin { get; }
        string? Subdomain { get; }
        bool HasValidAccess(int requestedCompanyId);
    }

    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsSuperAdmin
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !user.Identity?.IsAuthenticated == true) return false;
                return user.IsInRole("SuperAdmin");
            }
        }

        public int? CurrentCompanyId
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context == null) return null;

                var user = context.User;
                if (user?.Identity?.IsAuthenticated == true)
                {
                    var claim = user.FindFirst("companyId")?.Value;
                    if (int.TryParse(claim, out var cid))
                    {
                        return cid;
                    }

                    // SuperAdmin may scope to a company via route or header
                    if (IsSuperAdmin)
                    {
                        if (context.Request.RouteValues.TryGetValue("companyId", out var rVal) &&
                            int.TryParse(rVal?.ToString(), out var routeCompanyId))
                        {
                            return routeCompanyId;
                        }

                        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var headerVal) &&
                            int.TryParse(headerVal, out var headerCompanyId))
                        {
                            return headerCompanyId;
                        }
                    }
                }

                // Header or route resolution for anonymous requests (e.g. public booking)
                if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var hVal) &&
                    int.TryParse(hVal, out var pubCid))
                {
                    return pubCid;
                }

                return null;
            }
        }

        public string? Subdomain
        {
            get
            {
                var host = _httpContextAccessor.HttpContext?.Request.Host.Host;
                if (string.IsNullOrEmpty(host)) return null;

                // e.g. studio1.crmapp.com -> studio1
                var parts = host.Split('.');
                if (parts.Length >= 3 && parts[0] != "www" && parts[0] != "api")
                {
                    return parts[0].ToLowerInvariant();
                }

                return null;
            }
        }

        public bool HasValidAccess(int requestedCompanyId)
        {
            if (IsSuperAdmin) return true;
            return CurrentCompanyId.HasValue && CurrentCompanyId.Value == requestedCompanyId;
        }
    }
}
