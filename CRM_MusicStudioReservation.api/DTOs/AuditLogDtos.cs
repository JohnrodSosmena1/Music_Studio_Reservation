using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_MusicStudioReservation.api.DTOs
{
    public class AuditLogCreateDto
    {
        [StringLength(128)]
        public string? UserId { get; set; }

        [EmailAddress]
        [StringLength(256)]
        public string? UserEmail { get; set; }

        [StringLength(50)]
        public string? UserRole { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string EntityName { get; set; } = null!;

        public int? EntityId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }
    }

    public class AuditLogResponseDto
    {
        public int AuditLogId { get; set; }
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserRole { get; set; }
        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public int? EntityId { get; set; }
        public int CompanyId { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? IpAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
