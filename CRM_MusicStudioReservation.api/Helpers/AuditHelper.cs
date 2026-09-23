using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM_MusicStudioReservation.api.Helpers
{
    /// <summary>
    /// Central helper for logging audit events across all endpoints.
    /// Extracts user info from JWT claims and writes to the master AuditLogs table.
    /// Never throws — audit failures never break the main request.
    /// </summary>
    public static class AuditHelper
    {
        /// <summary>
        /// Log an audit event. Safe to call from any endpoint — failures are swallowed.
        /// </summary>
        public static async Task LogAsync(
            IAuditService auditService,
            HttpContext httpContext,
            ClaimsPrincipal user,
            int companyId,
            string action,
            string entityName,
            int entityId,
            string? oldValue = null,
            string? newValue = null)
        {
            try
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "unknown";
                var email = user.FindFirst(ClaimTypes.Email)?.Value ?? "unknown@unknown";
                var role = user.FindFirst(ClaimTypes.Role)?.Value ?? "Unknown";
                var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                await auditService.LogActionAsync(
                    companyId: companyId,
                    userId: userId,
                    userEmail: email,
                    userRole: role,
                    action: action,
                    entityName: entityName,
                    entityId: entityId,
                    oldValue: oldValue,
                    newValue: newValue,
                    ipAddress: ip);
            }
            catch (Exception ex)
            {
                // Never let audit failures break the main flow
                Console.WriteLine($"[AuditHelper] Failed to log {entityName} #{entityId} ({action}): {ex.Message}");
            }
        }
    }
}