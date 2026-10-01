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
            var now = DateTime.UtcNow;

            // 1. Seed Super Admin & Default Users
            if (!await _db.AppUsers.AnyAsync(u => u.Role == UserRole.SuperAdmin))
            {
                var superAdmin = new AppUser
                {
                    Email = "superadmin@musicstudio.com",
                    FullName = "Super Admin",
                    PasswordHash = HashPassword("Admin@123"),
                    Role = UserRole.SuperAdmin,
                    CompanyId = null,
                    IsActive = true,
                    CreatedAt = now
                };
                _db.AppUsers.Add(superAdmin);
            }

            // Company 1 Users
            var comp1 = await _db.Companies.FirstOrDefaultAsync(c => c.CompanyCode == "TEN-00001" || c.Subdomain == "soundwave");
            int c1Id = comp1?.CompanyId ?? 1;

            if (!await _db.AppUsers.AnyAsync(u => u.Email == "admin@company1.com"))
            {
                _db.AppUsers.Add(new AppUser
                {
                    Email = "admin@company1.com",
                    FullName = "Company 1 Admin",
                    PasswordHash = HashPassword("Admin@123"),
                    Role = UserRole.Admin,
                    CompanyId = c1Id,
                    IsActive = true,
                    CreatedAt = now
                });
            }

            if (!await _db.AppUsers.AnyAsync(u => u.Email == "staff@company1.com"))
            {
                _db.AppUsers.Add(new AppUser
                {
                    Email = "staff@company1.com",
                    FullName = "Company 1 Staff",
                    PasswordHash = HashPassword("Staff@123"),
                    Role = UserRole.Staff,
                    CompanyId = c1Id,
                    IsActive = true,
                    CreatedAt = now
                });
            }

            // Company 2 Users
            var comp2 = await _db.Companies.FirstOrDefaultAsync(c => c.CompanyCode == "TEN-00002" || c.Subdomain == "harmonysound");
            int c2Id = comp2?.CompanyId ?? 2;

            if (!await _db.AppUsers.AnyAsync(u => u.Email == "admin@company2.com"))
            {
                _db.AppUsers.Add(new AppUser
                {
                    Email = "admin@company2.com",
                    FullName = "Company 2 Admin",
                    PasswordHash = HashPassword("Admin@123"),
                    Role = UserRole.Admin,
                    CompanyId = c2Id,
                    IsActive = true,
                    CreatedAt = now
                });
            }

            if (!await _db.AppUsers.AnyAsync(u => u.Email == "sarah@harmonysound.com"))
            {
                _db.AppUsers.Add(new AppUser
                {
                    Email = "sarah@harmonysound.com",
                    FullName = "Sarah Jenkins",
                    PasswordHash = HashPassword("Admin@123"),
                    Role = UserRole.Admin,
                    CompanyId = c2Id,
                    IsActive = true,
                    CreatedAt = now
                });
            }

            if (!await _db.AppUsers.AnyAsync(u => u.Email == "staff@company2.com"))
            {
                _db.AppUsers.Add(new AppUser
                {
                    Email = "staff@company2.com",
                    FullName = "Company 2 Staff",
                    PasswordHash = HashPassword("Staff@123"),
                    Role = UserRole.Staff,
                    CompanyId = c2Id,
                    IsActive = true,
                    CreatedAt = now
                });
            }

            // Company 3 Users
            var comp3 = await _db.Companies.FirstOrDefaultAsync(c => c.CompanyCode == "TEN-00003" || c.Subdomain == "cadence");
            int c3Id = comp3?.CompanyId ?? 3;

            if (!await _db.AppUsers.AnyAsync(u => u.Email == "admin@company3.com"))
            {
                _db.AppUsers.Add(new AppUser
                {
                    Email = "admin@company3.com",
                    FullName = "Company 3 Admin",
                    PasswordHash = HashPassword("Admin@123"),
                    Role = UserRole.Admin,
                    CompanyId = c3Id,
                    IsActive = true,
                    CreatedAt = now
                });
            }

            if (!await _db.AppUsers.AnyAsync(u => u.Email == "staff@company3.com"))
            {
                _db.AppUsers.Add(new AppUser
                {
                    Email = "staff@company3.com",
                    FullName = "Company 3 Staff",
                    PasswordHash = HashPassword("Staff@123"),
                    Role = UserRole.Staff,
                    CompanyId = c3Id,
                    IsActive = true,
                    CreatedAt = now
                });
            }

            // 2. Seed Default Subscription Plans if empty
            if (!await _db.SubscriptionPlans.AnyAsync())
            {
                var plans = new List<SubscriptionPlan>
                {
                    new()
                    {
                        PlanCode = "PLAN-FREE",
                        PlanName = "Free",
                        Price = 0.00m,
                        BillingCycle = "Monthly",
                        MaxUsers = 2,
                        MaxBookingsPerMonth = 30,
                        MaxStorageMb = 512,
                        Features = "Basic Studio Scheduling, 1 Studio Room, Community Support",
                        IsActive = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    },
                    new()
                    {
                        PlanCode = "PLAN-BASIC",
                        PlanName = "Basic",
                        Price = 1499.00m,
                        BillingCycle = "Monthly",
                        MaxUsers = 5,
                        MaxBookingsPerMonth = 150,
                        MaxStorageMb = 2048,
                        Features = "Up to 3 Studio Rooms, Inventory Tracking, Client Loyalty System, Email Support",
                        IsActive = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    },
                    new()
                    {
                        PlanCode = "PLAN-PRO",
                        PlanName = "Pro",
                        Price = 3499.00m,
                        BillingCycle = "Monthly",
                        MaxUsers = 15,
                        MaxBookingsPerMonth = 500,
                        MaxStorageMb = 10240,
                        Features = "Unlimited Studio Rooms, Advanced CRM Analytics, Promotions Engine, Priority Support",
                        IsActive = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    },
                    new()
                    {
                        PlanCode = "PLAN-ENT",
                        PlanName = "Enterprise",
                        Price = 7999.00m,
                        BillingCycle = "Monthly",
                        MaxUsers = 50,
                        MaxBookingsPerMonth = 5000,
                        MaxStorageMb = 51200,
                        Features = "Multi-Branch Management, Custom Domain/Subdomain, Dedicated SLA, Full API Access",
                        IsActive = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    }
                };
                _db.SubscriptionPlans.AddRange(plans);
            }

            // 3. Seed Platform Terms and Conditions if empty
            if (!await _db.PlatformTermsAndConditions.AnyAsync())
            {
                var terms = new List<PlatformTermsAndConditions>
                {
                    new()
                    {
                        TandCCode = "PTC-00001",
                        TandCType = "PlatformTerms",
                        Title = "Platform Master Service Agreement",
                        Content = "Welcome to the CRM Music Studio Reservation SaaS Platform. By using this service, studio organizations agree to platform uptime terms, acceptable data usage, service quotas, and billing terms.",
                        Version = "v1.0",
                        MajorVersion = 1,
                        MinorVersion = 0,
                        Status = "Published",
                        PublishedAt = now,
                        AuthorName = "Platform Legal",
                        RequiresReAcceptance = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    },
                    new()
                    {
                        TandCCode = "PTC-00002",
                        TandCType = "PrivacyPolicy",
                        Title = "Platform Privacy & Security Policy",
                        Content = "This Privacy Policy governs the collection, processing, and storage of customer, booking, and studio operational telemetry across all tenant instances.",
                        Version = "v1.0",
                        MajorVersion = 1,
                        MinorVersion = 0,
                        Status = "Published",
                        PublishedAt = now,
                        AuthorName = "Platform Legal",
                        RequiresReAcceptance = false,
                        CreatedAt = now,
                        UpdatedAt = now
                    }
                };
                _db.PlatformTermsAndConditions.AddRange(terms);
            }

            await _db.SaveChangesAsync();
        }
    }
}
