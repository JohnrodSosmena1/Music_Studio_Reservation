using CRM_MusicStudioReservation.domain.entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.data
{
    public class TenantCRMDbContext : DbContext
    {
        public TenantCRMDbContext(DbContextOptions<TenantCRMDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Supplier> Suppliers => Set<Supplier>();

        public DbSet<Inventory> Inventories => Set<Inventory>();

        // Music Studio Reservation entities
        public DbSet<Studio> Studios => Set<Studio>();
        public DbSet<StudioService> StudioServices => Set<StudioService>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingService> BookingServices => Set<BookingService>();
        public DbSet<Membership> Memberships => Set<Membership>();
        public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
        public DbSet<LoyaltyTransaction> LoyaltyTransactions => Set<LoyaltyTransaction>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<InventoryCategory> InventoryCategories => Set<InventoryCategory>();
        public DbSet<Promotion> Promotions => Set<Promotion>();
        public DbSet<CustomerFeedback> CustomerFeedbacks => Set<CustomerFeedback>();

        // 👇 NEW — Customer Reviews
        public DbSet<CustomerReview> CustomerReviews => Set<CustomerReview>();

        public DbSet<CustomerInquiry> CustomerInquiries => Set<CustomerInquiry>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ProductId);

                entity.Property(x => x.ProductCode)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.ProductName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(x => x.UnitPrice)
                    .HasPrecision(18, 2);

                entity.HasIndex(x => x.ProductCode)
                    .IsUnique();
            });

            // 👇 NEW — CustomerInquiry configuration
            builder.Entity<CustomerInquiry>(entity =>
            {
                entity.HasKey(x => x.CustomerInquiryId);

                entity.Property(x => x.Subject).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Message).HasMaxLength(4000).IsRequired();
                entity.Property(x => x.Status).HasMaxLength(20).IsRequired();
                entity.Property(x => x.Priority).HasMaxLength(20).IsRequired();
                entity.Property(x => x.Response).HasMaxLength(4000);
                entity.Property(x => x.RespondedBy).HasMaxLength(256);

                entity.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.Status);
                entity.HasIndex(x => x.CustomerId);
                entity.HasIndex(x => x.CreatedAt);
            });


            // Inventory decimal precision config
            builder.Entity<Inventory>()
                .Property(x => x.QuantityOnHand)
                .HasPrecision(18, 2);

            builder.Entity<Inventory>()
                .Property(x => x.ReorderLevel)
                .HasPrecision(18, 2);

            // Music Studio Reservation entity configurations
            builder.Entity<Studio>(entity =>
            {
                entity.HasKey(x => x.StudioId);
                entity.Property(x => x.StudioCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.StudioName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.HourlyRate).HasPrecision(18, 2);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.HasIndex(x => x.StudioCode).IsUnique();
            });

            builder.Entity<StudioService>(entity =>
            {
                entity.HasKey(x => x.StudioServiceId);
                entity.Property(x => x.ServiceCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.ServiceName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Price).HasPrecision(18, 2);
                entity.HasIndex(x => x.ServiceCode).IsUnique();
            });

            builder.Entity<Booking>(entity =>
            {
                entity.HasKey(x => x.BookingId);
                entity.Property(x => x.BookingCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.TotalAmount).HasPrecision(18, 2);
                entity.Property(x => x.Notes).HasMaxLength(2000);
                entity.HasIndex(x => x.BookingCode).IsUnique();

                entity.HasOne(x => x.Studio)
                    .WithMany(s => s.Bookings)
                    .HasForeignKey(x => x.StudioId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BookingService>(entity =>
            {
                entity.HasKey(x => x.BookingServiceId);
                entity.Property(x => x.UnitPrice).HasPrecision(18, 2);

                entity.HasOne(x => x.Booking)
                    .WithMany(b => b.BookingServices)
                    .HasForeignKey(x => x.BookingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.StudioService)
                    .WithMany(s => s.BookingServices)
                    .HasForeignKey(x => x.StudioServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Membership>(entity =>
            {
                entity.HasKey(x => x.MembershipId);
                entity.Property(x => x.LoyaltyPoints).HasDefaultValue(0);

                entity.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.MembershipPlan)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(x => x.MembershipPlanId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<MembershipPlan>(entity =>
            {
                entity.HasKey(x => x.MembershipPlanId);
                entity.Property(x => x.PlanName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.MonthlyFee).HasPrecision(18, 2);
                entity.Property(x => x.Benefits).HasMaxLength(2000);
            });

            builder.Entity<LoyaltyTransaction>(entity =>
            {
                entity.HasKey(x => x.LoyaltyTransactionId);
                entity.Property(x => x.Points).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(1000);

                entity.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InventoryCategory>(entity =>
            {
                entity.HasKey(x => x.InventoryCategoryId);
                entity.Property(x => x.CategoryName).HasMaxLength(200).IsRequired();
            });

            builder.Entity<InventoryItem>(entity =>
            {
                entity.HasKey(x => x.InventoryItemId);
                entity.Property(x => x.ItemCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.ItemName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.UnitCost).HasPrecision(18, 2);

                entity.HasOne(x => x.InventoryCategory)
                    .WithMany(c => c.InventoryItems)
                    .HasForeignKey(x => x.InventoryCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Promotion>(entity =>
            {
                entity.HasKey(x => x.PromotionId);
                entity.Property(x => x.PromotionCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.PromotionName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.DiscountPercent).HasPrecision(5, 2);
                entity.HasIndex(x => x.PromotionCode).IsUnique();
            });

            builder.Entity<CustomerFeedback>(entity =>
            {
                entity.HasKey(x => x.FeedbackId);
                entity.Property(x => x.Rating).IsRequired();
                entity.Property(x => x.Comments).HasMaxLength(2000);

                entity.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 👇 NEW — CustomerReview configuration
            builder.Entity<CustomerReview>(entity =>
            {
                entity.HasKey(x => x.CustomerReviewId);

                entity.Property(x => x.ReviewType).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Title).HasMaxLength(200);
                entity.Property(x => x.Comment).HasMaxLength(4000).IsRequired();
                entity.Property(x => x.ModerationStatus).HasMaxLength(20).IsRequired();
                entity.Property(x => x.AdminReply).HasMaxLength(2000);

                entity.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Studio)
                    .WithMany()
                    .HasForeignKey(x => x.StudioId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.ModerationStatus);
                entity.HasIndex(x => x.CustomerId);
                entity.HasIndex(x => x.CreatedAt);
            });
        }
    }
}