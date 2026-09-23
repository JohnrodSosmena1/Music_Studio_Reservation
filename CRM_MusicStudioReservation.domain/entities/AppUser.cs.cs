using System;
using CRM_MusicStudioReservation.domain.enums;

namespace CRM_MusicStudioReservation.domain.entities
{
    public class AppUser
    {
        public int UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        /// <summary>
        /// For Admin/Staff/Client users, which company (tenant) they belong to.
        /// SuperAdmin has CompanyId = null (access to all).
        /// </summary>
        public int? CompanyId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }
    }
}