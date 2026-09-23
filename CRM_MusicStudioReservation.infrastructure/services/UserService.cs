using System;
using System.Collections.Generic;
using System.Text;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    public class UserService : IUserService
    {
        private readonly MasterCRMDbContext _db;

        public UserService(MasterCRMDbContext db)
        {
            _db = db;
        }

        public async Task<AppUser?> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _db.AppUsers
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);

            if (user is null)
                return null;

            // BCrypt verify
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _db.AppUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            var user = await _db.AppUsers.FindAsync(userId);
            if (user is not null)
            {
                user.LastLoginAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
            }
        }

        public async Task SeedDefaultUsersAsync()
        {
            // Only seed if no users exist
            if (await _db.AppUsers.AnyAsync())
                return;

            var users = new List<AppUser>
            {
                new()
                {
                    Email = "superadmin@musicstudio.com",
                    FullName = "Super Admin",
                    PasswordHash = HashPassword("Admin@123"),
                    Role = UserRole.SuperAdmin,
                    CompanyId = null,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Email = "admin@company1.com",
                    FullName = "Company Admin",
                    PasswordHash = HashPassword("Admin@123"),
                    Role = UserRole.Admin,
                    CompanyId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Email = "staff@company1.com",
                    FullName = "Staff Member",
                    PasswordHash = HashPassword("Staff@123"),
                    Role = UserRole.Staff,
                    CompanyId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Email = "client@company1.com",
                    FullName = "Client User",
                    PasswordHash = HashPassword("Client@123"),
                    Role = UserRole.Client,
                    CompanyId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _db.AppUsers.AddRange(users);
            await _db.SaveChangesAsync();
        }
    }
}
