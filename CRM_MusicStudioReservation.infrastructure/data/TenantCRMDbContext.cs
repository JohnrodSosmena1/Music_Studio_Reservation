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

        // 👇 NEW — Terms & Conditions + Promotion Rationale
        public DbSet<TermsAndConditions> TermsAndConditions => Set<TermsAndConditions>();
        public DbSet<UserTandCAcknowledgment> UserTandCAcknowledgments => Set<UserTandCAcknowledgment>();
        public DbSet<PromotionRationale> PromotionRationales => Set<PromotionRationale>();
        public DbSet<PromotionSegment> PromotionSegments => Set<PromotionSegment>();

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
                entity.Property(x => x.UpdatedAt);
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
                entity.Property(x => x.Condition).HasMaxLength(50);
                entity.Property(x => x.Availability).HasMaxLength(50);
                entity.Property(x => x.Location).HasMaxLength(200);

                entity.HasIndex(x => x.ItemCode).IsUnique();
                entity.HasIndex(x => x.StudioId);

                entity.HasOne(x => x.InventoryCategory)
                    .WithMany(c => c.InventoryItems)
                    .HasForeignKey(x => x.InventoryCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Studio)
                    .WithMany(s => s.InventoryItems)
                    .HasForeignKey(x => x.StudioId)
                    .OnDelete(DeleteBehavior.SetNull);
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

            // 👇 NEW — TermsAndConditions configuration
            builder.Entity<TermsAndConditions>(entity =>
            {
                entity.HasKey(x => x.TandCId);

                entity.Property(x => x.TandCCode).HasMaxLength(50).IsRequired();
                entity.HasIndex(x => x.TandCCode).IsUnique();

                entity.Property(x => x.TandCType).HasMaxLength(50).IsRequired();
                entity.HasIndex(x => x.TandCType);

                entity.Property(x => x.Title).HasMaxLength(300).IsRequired();

                entity.Property(x => x.Content).HasColumnType("nvarchar(max)").IsRequired();

                entity.Property(x => x.Version).HasMaxLength(20).IsRequired();

                entity.Property(x => x.Status).HasMaxLength(30).IsRequired();
                entity.HasIndex(x => x.Status);

                entity.Property(x => x.AuthorUserId).HasMaxLength(128);
                entity.Property(x => x.AuthorName).HasMaxLength(200);
                entity.Property(x => x.ApprovedByUserId).HasMaxLength(128);
                entity.Property(x => x.ApprovedByName).HasMaxLength(200);
                entity.Property(x => x.ChangeNotes).HasMaxLength(2000);

                entity.HasIndex(x => new { x.TandCType, x.Status });
            });

            // 👇 NEW — UserTandCAcknowledgment configuration
            builder.Entity<UserTandCAcknowledgment>(entity =>
            {
                entity.HasKey(x => x.AcknowledgmentId);

                entity.Property(x => x.TandCVersion).HasMaxLength(20).IsRequired();
                entity.Property(x => x.TandCType).HasMaxLength(50).IsRequired();
                entity.Property(x => x.AcknowledgmentContext).HasMaxLength(50).IsRequired();
                entity.Property(x => x.IpAddress).HasMaxLength(50);
                entity.Property(x => x.UserAgent).HasMaxLength(500);

                entity.HasOne(x => x.TermsAndConditions)
                    .WithMany(t => t.Acknowledgments)
                    .HasForeignKey(x => x.TandCId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.TandCId);
                entity.HasIndex(x => x.CustomerId);
                entity.HasIndex(x => x.AcknowledgedAt);
            });

            // 👇 NEW — PromotionRationale configuration
            builder.Entity<PromotionRationale>(entity =>
            {
                entity.HasKey(x => x.PromotionRationaleId);

                entity.Property(x => x.PurposeType).HasMaxLength(100).IsRequired();
                entity.Property(x => x.TargetAudience).HasMaxLength(200);
                entity.Property(x => x.TriggerCondition).HasMaxLength(500);
                entity.Property(x => x.ExpectedKpi).HasMaxLength(500);
                entity.Property(x => x.Budget).HasPrecision(18, 2);
                entity.Property(x => x.WorkflowStatus).HasMaxLength(30).IsRequired();
                entity.HasIndex(x => x.WorkflowStatus);
                entity.Property(x => x.ApprovedByUserId).HasMaxLength(128);
                entity.Property(x => x.ApprovedByName).HasMaxLength(200);
                entity.Property(x => x.RejectionReason).HasMaxLength(1000);
                entity.Property(x => x.RationaleNotes).HasMaxLength(2000);
                entity.Property(x => x.ActualRevenueDelta).HasPrecision(18, 2);
                entity.Property(x => x.RoiSummary).HasMaxLength(2000);

                entity.HasOne(x => x.Promotion)
                    .WithMany()
                    .HasForeignKey(x => x.PromotionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.PromotionId).IsUnique();
            });

            // 👇 NEW — PromotionSegment configuration
            builder.Entity<PromotionSegment>(entity =>
            {
                entity.HasKey(x => x.PromotionSegmentId);

                entity.Property(x => x.SegmentName).HasMaxLength(100).IsRequired();

                entity.HasOne(x => x.PromotionRationale)
                    .WithMany(r => r.Segments)
                    .HasForeignKey(x => x.PromotionRationaleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 👇 Customer configuration (3NF, unique auto-gen code, split names)
            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.CustomerId);
                entity.Property(x => x.CustomerCode).HasMaxLength(50).IsRequired();
                entity.HasIndex(x => x.CustomerCode).IsUnique();
                entity.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.CustomerName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.ContactNumber).HasMaxLength(50);
                entity.Property(x => x.EmailAddress).HasMaxLength(255);
                entity.Property(x => x.Address).HasMaxLength(500);
            });
        }
    }
}