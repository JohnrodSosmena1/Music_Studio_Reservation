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

                entity.HasIndex(x => x.CompanyCode)
                  .IsUnique();
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

            // 👇 ADDED: AppUser Configuration 👇
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
        }
    }
}