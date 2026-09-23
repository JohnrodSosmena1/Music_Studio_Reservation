using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    /// <summary>
    /// Master database audit log for tracking system-wide changes across all tenants.
    /// Records who performed what action on which entity in which tenant database.
    /// </summary>
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        // User information
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserRole { get; set; }

        // Action information
        public string Action { get; set; } = null!;  // e.g., "Create", "Update", "Delete"
        public string EntityName { get; set; } = null!;  // e.g., "Booking", "Studio"
        public int? EntityId { get; set; }

        // Tenant information
        public int CompanyId { get; set; }

        // Change tracking
        public string? OldValue { get; set; }  // JSON serialized old data
        public string? NewValue { get; set; }  // JSON serialized new data

        // Network information
        public string? IpAddress { get; set; }

        // Timestamp
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
