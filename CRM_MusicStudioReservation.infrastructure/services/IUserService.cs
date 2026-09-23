using System;
using System.Collections.Generic;
using System.Text;
using CRM_MusicStudioReservation.domain.entities;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public interface IUserService
    {
        /// <summary>
        /// Validates credentials and returns the user, or null if invalid.
        /// </summary>
        Task<AppUser?> AuthenticateAsync(string email, string password);

        /// <summary>
        /// Hashes a plain-text password using BCrypt.
        /// </summary>
        string HashPassword(string password);

        /// <summary>
        /// Finds a user by email. Returns null if not found.
        /// </summary>
        Task<AppUser?> GetByEmailAsync(string email);

        /// <summary>
        /// Records the last login timestamp for a user.
        /// </summary>
        Task UpdateLastLoginAsync(int userId);

        /// <summary>
        /// Ensures the default seed users exist. Creates them if missing.
        /// </summary>
        Task SeedDefaultUsersAsync();
    }
}