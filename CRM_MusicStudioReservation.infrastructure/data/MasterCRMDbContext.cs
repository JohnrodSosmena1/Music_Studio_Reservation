using CRM_MusicStudioReservation.domain.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_MusicStudioSystem.infrastructure.data
{
    public class MasterCRMDbContext : IdentityDbContext
    {
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<CompanyDatabase> CompanyDatabases => Set<CompanyDatabase>();
        public DbSet<Device> Devices { get; set; }
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        // Super Admin & Platform-wide DbSets
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<SubscriptionInvoice> SubscriptionInvoices => Set<SubscriptionInvoice>();
        public DbSet<PlatformTermsAndConditions> PlatformTermsAndConditions => Set<PlatformTermsAndConditions>();
        public DbSet<PlatformTandCAcknowledgment> PlatformTandCAcknowledgments => Set<PlatformTandCAcknowledgment>();
        public DbSet<SuperAdminAuditLog> SuperAdminAuditLogs => Set<SuperAdminAuditLog>();

        public MasterCRMDbContext(DbContextOptions<MasterCRMDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.CompanyId);

                entity.Property(x => x.CompanyCode)
                  .HasMaxLength(50)
                  .IsRequired();

                entity.Property(x => x.CompanyName)
                  .HasMaxLength(200)
                  .IsRequired();

                entity.Property(x => x.Subdomain)
                  .HasMaxLength(100);

                entity.Property(x => x.OwnerFirstName)
                  .HasMaxLength(100);

                entity.Property(x => x.OwnerLastName)
                  .HasMaxLength(100);

                entity.Property(x => x.OwnerEmail)
                  .HasMaxLength(250);

                entity.Property(x => x.ContactNumber)
                  .HasMaxLength(50);

                entity.Property(x => x.TimeZone)
                  .HasMaxLength(100)
                  .HasDefaultValue("Asia/Manila");

                entity.Property(x => x.Status)
                  .HasMaxLength(50)
                  .HasDefaultValue("Active");

                entity.HasIndex(x => x.CompanyCode)
                  .IsUnique();

                entity.HasIndex(x => x.Subdomain)
                  .IsUnique()
                  .HasFilter("[Subdomain] IS NOT NULL");

                entity.HasOne(x => x.SubscriptionPlan)
                  .WithMany(p => p.Companies)
                  .HasForeignKey(x => x.SubscriptionPlanId)
                  .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<CompanyDatabase>(entity =>
            {
                entity.HasKey(x => x.CompanyDatabaseId);

                entity.Property(x => x.ServerName)
                  .HasMaxLength(200)
                  .IsRequired();

                entity.Property(x => x.DatabaseName)
                  .HasMaxLength(200)
                  .IsRequired();

                entity.HasOne(x => x.Company)
                  .WithMany()
                  .HasForeignKey(x => x.CompanyId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Device>(entity =>
            {
                entity.HasKey(x => x.DeviceId);

                entity.Property(x => x.DeviceCode)
                  .HasMaxLength(50)
                  .IsRequired();

                entity.Property(x => x.DeviceName)
                  .HasMaxLength(200)
                  .IsRequired();

                entity.HasOne(x => x.Company)
                  .WithMany(x => x.Devices)
                  .HasForeignKey(x => x.CompanyId)
                  .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => new { x.CompanyId, x.DeviceCode })
                  .IsUnique();
            });

            builder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(x => x.AuditLogId);

                entity.Property(x => x.UserId).HasMaxLength(128);
                entity.Property(x => x.UserEmail).HasMaxLength(256);
                entity.Property(x => x.UserRole).HasMaxLength(50);
                entity.Property(x => x.Action).HasMaxLength(50).IsRequired();
                entity.Property(x => x.EntityName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.IpAddress).HasMaxLength(50);
                entity.Property(x => x.CompanyId).IsRequired();

                entity.HasIndex(x => x.CompanyId);
                entity.HasIndex(x => x.EntityName);
                entity.HasIndex(x => x.CreatedAt);
            });

            builder.Entity<AppUser>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Email)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(u => u.FullName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(u => u.Role)
                    .HasConversion<string>();
            });

            // SubscriptionPlan
            builder.Entity<SubscriptionPlan>(entity =>
            {
                entity.HasKey(p => p.SubscriptionPlanId);
                entity.Property(p => p.PlanCode).HasMaxLength(50).IsRequired();
                entity.Property(p => p.PlanName).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.BillingCycle).HasMaxLength(20).HasDefaultValue("Monthly");
                entity.HasIndex(p => p.PlanCode).IsUnique();
            });

            // Subscription
            builder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => s.SubscriptionId);
                entity.Property(s => s.Status).HasMaxLength(50).HasDefaultValue("Active");

                entity.HasOne(s => s.Company)
                    .WithMany(c => c.Subscriptions)
                    .HasForeignKey(s => s.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.SubscriptionPlan)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(s => s.SubscriptionPlanId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => s.CompanyId);
                entity.HasIndex(s => s.Status);
            });

            // SubscriptionInvoice
            builder.Entity<SubscriptionInvoice>(entity =>
            {
                entity.HasKey(i => i.SubscriptionInvoiceId);
                entity.Property(i => i.InvoiceNumber).HasMaxLength(50).IsRequired();
                entity.Property(i => i.Amount).HasPrecision(18, 2);
                entity.Property(i => i.Status).HasMaxLength(50).HasDefaultValue("Paid");

                entity.HasOne(i => i.Company)
                    .WithMany(c => c.Invoices)
                    .HasForeignKey(i => i.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.Subscription)
                    .WithMany()
                    .HasForeignKey(i => i.SubscriptionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(i => i.InvoiceNumber).IsUnique();
                entity.HasIndex(i => i.CompanyId);
            });

            // PlatformTermsAndConditions
            builder.Entity<PlatformTermsAndConditions>(entity =>
            {
                entity.HasKey(t => t.PlatformTandCId);
                entity.Property(t => t.TandCCode).HasMaxLength(50).IsRequired();
                entity.Property(t => t.TandCType).HasMaxLength(50).IsRequired();
                entity.Property(t => t.Title).HasMaxLength(300).IsRequired();
                entity.Property(t => t.Version).HasMaxLength(20).IsRequired();
                entity.Property(t => t.Status).HasMaxLength(30).IsRequired();
                entity.HasIndex(t => t.TandCCode).IsUnique();
                entity.HasIndex(t => new { t.TandCType, t.Status });
            });

            // PlatformTandCAcknowledgment
            builder.Entity<PlatformTandCAcknowledgment>(entity =>
            {
                entity.HasKey(a => a.AcknowledgmentId);
                entity.Property(a => a.AcknowledgedByEmail).HasMaxLength(250).IsRequired();
                entity.Property(a => a.AcknowledgedByName).HasMaxLength(200);
                entity.Property(a => a.Version).HasMaxLength(20).IsRequired();

                entity.HasOne(a => a.PlatformTermsAndConditions)
                    .WithMany(t => t.Acknowledgments)
                    .HasForeignKey(a => a.PlatformTandCId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Company)
                    .WithMany()
                    .HasForeignKey(a => a.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(a => a.CompanyId);
                entity.HasIndex(a => a.PlatformTandCId);
            });

            // SuperAdminAuditLog
            builder.Entity<SuperAdminAuditLog>(entity =>
            {
                entity.HasKey(l => l.SuperAdminAuditLogId);
                entity.Property(l => l.UserEmail).HasMaxLength(250).IsRequired();
                entity.Property(l => l.Action).HasMaxLength(100).IsRequired();
                entity.Property(l => l.TargetType).HasMaxLength(100).IsRequired();
                entity.Property(l => l.TargetId).HasMaxLength(100);
                entity.Property(l => l.IpAddress).HasMaxLength(50);
                entity.HasIndex(l => l.Action);
                entity.HasIndex(l => l.CreatedAt);
            });
        }
    }
}