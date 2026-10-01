using System;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class SuperAdminAuditLog
    {
        public int SuperAdminAuditLogId { get; set; }

        public int? UserId { get; set; }

        public string UserEmail { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty; // CreateTenant, UpdateTenant, SuspendTenant, ChangePlan, PublishTandC, HardDeleteTenant, Impersonate

        public string TargetType { get; set; } = string.Empty; // Organization, Subscription, TandC, SubscriptionPlan

        public string? TargetId { get; set; }

        public string? Details { get; set; }

        public string? IpAddress { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
