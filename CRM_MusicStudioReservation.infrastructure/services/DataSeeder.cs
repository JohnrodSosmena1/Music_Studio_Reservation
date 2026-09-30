using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.domain.enums;
using CRM_MusicStudioSystem.infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioSystem.infrastructure.services
{
    /// <summary>
    /// Seeder for initializing sample data in tenant databases.
    /// </summary>
    public class DataSeeder
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public DataSeeder(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        /// <summary>
        /// Seed initial data for a company if it doesn't already exist.
        /// </summary>
        public async Task SeedCompanyDataAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            // Seed membership plans if none exist
            if (!await db.MembershipPlans.AnyAsync())
            {
                var plans = new List<MembershipPlan>
                {
                    new MembershipPlan
                    {
                        PlanName = "Basic",
                        Description = "Basic membership plan for casual users",
                        MonthlyFee = 29.99m,
                        LoyaltyPointsPerBooking = 5,
                        Benefits = "5% discount on all bookings",
                        IsActive = true
                    },
                    new MembershipPlan
                    {
                        PlanName = "Premium",
                        Description = "Premium membership plan for frequent users",
                        MonthlyFee = 79.99m,
                        LoyaltyPointsPerBooking = 15,
                        Benefits = "15% discount on all bookings, Priority booking, Free cancellation",
                        IsActive = true
                    },
                    new MembershipPlan
                    {
                        PlanName = "Professional",
                        Description = "Professional membership plan for studios and bands",
                        MonthlyFee = 199.99m,
                        LoyaltyPointsPerBooking = 30,
                        Benefits = "25% discount on all bookings, Priority booking, Free cancellation, Dedicated support",
                        IsActive = true
                    }
                };

                db.MembershipPlans.AddRange(plans);
                await db.SaveChangesAsync();
            }

            // Seed studios if none exist
            if (!await db.Studios.AnyAsync())
            {
                var studios = new List<Studio>
                {
                    new Studio
                    {
                        StudioCode = "STD001",
                        StudioName = "Studio A",
                        StudioType = StudioType.Rehearsal,
                        HourlyRate = 500m,
                        Capacity = 6,
                        Description = "Spacious rehearsal room with acoustic treatment and full backline.",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Studio
                    {
                        StudioCode = "STD002",
                        StudioName = "Studio B",
                        StudioType = StudioType.Recording,
                        HourlyRate = 850m,
                        Capacity = 8,
                        Description = "Multi-track recording live room with isolation booth.",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Studio
                    {
                        StudioCode = "STD003",
                        StudioName = "Studio C",
                        StudioType = StudioType.Vocal,
                        HourlyRate = 600m,
                        Capacity = 4,
                        Description = "Tuned vocal booth and voice-over tracking suite.",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Studio
                    {
                        StudioCode = "STD004",
                        StudioName = "Studio D",
                        StudioType = StudioType.Mixing,
                        HourlyRate = 750m,
                        Capacity = 5,
                        Description = "Mixing & mastering control room with calibrated 5.1 monitors.",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Studio
                    {
                        StudioCode = "STD005",
                        StudioName = "Studio E",
                        StudioType = StudioType.Mastering,
                        HourlyRate = 1200m,
                        Capacity = 10,
                        Description = "Flagship production studio with grand piano and analog console.",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                };

                db.Studios.AddRange(studios);
                await db.SaveChangesAsync();
            }

            // Seed inventory categories if none exist
            if (!await db.InventoryCategories.AnyAsync())
            {
                var categories = new List<InventoryCategory>
                {
                    new InventoryCategory
                    {
                        CategoryName = "Instruments",
                        Description = "Musical instruments available for use"
                    },
                    new InventoryCategory
                    {
                        CategoryName = "Equipment",
                        Description = "Recording and sound equipment"
                    },
                    new InventoryCategory
                    {
                        CategoryName = "Accessories",
                        Description = "Cables, stands, and other accessories"
                    },
                    new InventoryCategory
                    {
                        CategoryName = "Supplies",
                        Description = "Consumable supplies"
                    }
                };

                db.InventoryCategories.AddRange(categories);
                await db.SaveChangesAsync();
            }

            // Seed inventory items if none exist
            if (!await db.InventoryItems.AnyAsync() && await db.InventoryCategories.AnyAsync())
            {
                var instrumentsCategory = await db.InventoryCategories
                    .FirstOrDefaultAsync(c => c.CategoryName == "Instruments");
                var equipmentCategory = await db.InventoryCategories
                    .FirstOrDefaultAsync(c => c.CategoryName == "Equipment");

                var studioA = await db.Studios.FirstOrDefaultAsync(s => s.StudioCode == "STD001" || s.StudioName == "Studio A");
                var studioB = await db.Studios.FirstOrDefaultAsync(s => s.StudioCode == "STD002" || s.StudioName == "Studio B");
                var studioC = await db.Studios.FirstOrDefaultAsync(s => s.StudioCode == "STD003" || s.StudioName == "Studio C");
                var studioD = await db.Studios.FirstOrDefaultAsync(s => s.StudioCode == "STD004" || s.StudioName == "Studio D");
                var studioE = await db.Studios.FirstOrDefaultAsync(s => s.StudioCode == "STD005" || s.StudioName == "Studio E");

                if (instrumentsCategory != null && equipmentCategory != null)
                {
                    var items = new List<InventoryItem>
                    {
                        new InventoryItem
                        {
                            ItemCode = "DRUM001",
                            ItemName = "Ludwig Classic Drums",
                            InventoryCategoryId = instrumentsCategory.InventoryCategoryId,
                            StudioId = studioA?.StudioId,
                            Location = studioA?.StudioName ?? "Studio A",
                            Condition = "Good",
                            Availability = "Available",
                            QuantityOnHand = 2,
                            ReorderLevel = 1,
                            UnitCost = 3500m,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new InventoryItem
                        {
                            ItemCode = "BASS001",
                            ItemName = "Fender Bass Guitar",
                            InventoryCategoryId = instrumentsCategory.InventoryCategoryId,
                            StudioId = studioB?.StudioId,
                            Location = studioB?.StudioName ?? "Studio B",
                            Condition = "Good",
                            Availability = "Available",
                            QuantityOnHand = 3,
                            ReorderLevel = 1,
                            UnitCost = 1200m,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new InventoryItem
                        {
                            ItemCode = "MIC001",
                            ItemName = "Neumann U87 Microphone",
                            InventoryCategoryId = equipmentCategory.InventoryCategoryId,
                            StudioId = studioC?.StudioId,
                            Location = studioC?.StudioName ?? "Studio C",
                            Condition = "New",
                            Availability = "Available",
                            QuantityOnHand = 2,
                            ReorderLevel = 1,
                            UnitCost = 3200m,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new InventoryItem
                        {
                            ItemCode = "MIC002",
                            ItemName = "Shure SM7B Vocal Mic",
                            InventoryCategoryId = equipmentCategory.InventoryCategoryId,
                            StudioId = studioC?.StudioId,
                            Location = studioC?.StudioName ?? "Studio C",
                            Condition = "Good",
                            Availability = "Available",
                            QuantityOnHand = 4,
                            ReorderLevel = 1,
                            UnitCost = 400m,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new InventoryItem
                        {
                            ItemCode = "AMP001",
                            ItemName = "Marshall JCM800 Half Stack",
                            InventoryCategoryId = equipmentCategory.InventoryCategoryId,
                            StudioId = studioA?.StudioId,
                            Location = studioA?.StudioName ?? "Studio A",
                            Condition = "Good",
                            Availability = "Available",
                            QuantityOnHand = 2,
                            ReorderLevel = 1,
                            UnitCost = 1800m,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new InventoryItem
                        {
                            ItemCode = "MON001",
                            ItemName = "Yamaha HS8 Active Studio Monitors",
                            InventoryCategoryId = equipmentCategory.InventoryCategoryId,
                            StudioId = studioD?.StudioId,
                            Location = studioD?.StudioName ?? "Studio D",
                            Condition = "New",
                            Availability = "Available",
                            QuantityOnHand = 2,
                            ReorderLevel = 1,
                            UnitCost = 800m,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new InventoryItem
                        {
                            ItemCode = "KEY001",
                            ItemName = "Nord Stage 3 88-Key Stage Keyboard",
                            InventoryCategoryId = instrumentsCategory.InventoryCategoryId,
                            StudioId = studioE?.StudioId,
                            Location = studioE?.StudioName ?? "Studio E",
                            Condition = "New",
                            Availability = "Available",
                            QuantityOnHand = 1,
                            ReorderLevel = 1,
                            UnitCost = 4500m,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        }
                    };

                    db.InventoryItems.AddRange(items);
                    await db.SaveChangesAsync();
                }
            }
            else if (await db.InventoryItems.AnyAsync())
            {
                // Ensure existing items are linked to studios if StudioId is null
                var unlinkedItems = await db.InventoryItems.Where(i => i.StudioId == null).ToListAsync();
                if (unlinkedItems.Count > 0)
                {
                    var studios = await db.Studios.ToListAsync();
                    bool changed = false;
                    foreach (var item in unlinkedItems)
                    {
                        var matchingStudio = studios.FirstOrDefault(s =>
                            s.StudioName.Equals(item.Location, StringComparison.OrdinalIgnoreCase) ||
                            s.StudioCode.Equals(item.Location, StringComparison.OrdinalIgnoreCase));

                        if (matchingStudio != null)
                        {
                            item.StudioId = matchingStudio.StudioId;
                            item.Location = matchingStudio.StudioName;
                            changed = true;
                        }
                    }
                    if (changed)
                    {
                        await db.SaveChangesAsync();
                    }
                }
            }

            // Seed promotions if none exist
            if (!await db.Promotions.AnyAsync())
            {
                var now = DateTime.UtcNow;
                var promotions = new List<Promotion>
                {
                    new Promotion
                    {
                        PromotionCode = "WELCOME10",
                        PromotionName = "Welcome Discount",
                        Description = "10% discount for new customers",
                        DiscountPercent = 10,
                        StartDate = now,
                        EndDate = now.AddMonths(1),
                        IsActive = true
                    },
                    new Promotion
                    {
                        PromotionCode = "SUMMER20",
                        PromotionName = "Summer Special",
                        Description = "20% discount during summer months",
                        DiscountPercent = 20,
                        StartDate = now,
                        EndDate = now.AddMonths(3),
                        IsActive = true
                    },
                    new Promotion
                    {
                        PromotionCode = "LOYALTY50",
                        PromotionName = "Loyalty Rewards",
                        Description = "50% discount for loyalty members",
                        DiscountPercent = 50,
                        StartDate = now,
                        EndDate = now.AddMonths(6),
                        IsActive = true
                    }
                };

                db.Promotions.AddRange(promotions);
                await db.SaveChangesAsync();
            }

            // Seed customers if none exist
            if (!await db.Customers.AnyAsync())
            {
                var customers = new List<Customer>
                {
                    new Customer
                    {
                        CustomerCode = "CUST-00001",
                        FirstName = "John",
                        LastName = "Lennon",
                        CustomerName = "John Lennon",
                        ContactNumber = "+1-555-0101",
                        EmailAddress = "john.lennon@abbeyroad.com",
                        Address = "3 Savile Row, London",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-60),
                        UpdatedAt = DateTime.UtcNow.AddDays(-60)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00002",
                        FirstName = "Paul",
                        LastName = "McCartney",
                        CustomerName = "Paul McCartney",
                        ContactNumber = "+1-555-0102",
                        EmailAddress = "paul.mccartney@mpl.com",
                        Address = "7 Cavendish Avenue, London",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-45),
                        UpdatedAt = DateTime.UtcNow.AddDays(-45)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00003",
                        FirstName = "George",
                        LastName = "Harrison",
                        CustomerName = "George Harrison",
                        ContactNumber = "+1-555-0103",
                        EmailAddress = "george.harrison@darkhorse.com",
                        Address = "Friar Park, Henley-on-Thames",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-30),
                        UpdatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00004",
                        FirstName = "Ringo",
                        LastName = "Starr",
                        CustomerName = "Ringo Starr",
                        ContactNumber = "+1-555-0104",
                        EmailAddress = "ringo.starr@allstarr.com",
                        Address = "Cranleigh, Surrey",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-20),
                        UpdatedAt = DateTime.UtcNow.AddDays(-20)
                    },
                    new Customer
                    {
                        CustomerCode = "CUST-00005",
                        FirstName = "Freddie",
                        LastName = "Mercury",
                        CustomerName = "Freddie Mercury",
                        ContactNumber = "+1-555-0105",
                        EmailAddress = "freddie.mercury@queenonline.com",
                        Address = "Garden Lodge, Logan Place, Kensington",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-15),
                        UpdatedAt = DateTime.UtcNow.AddDays(-15)
                    }
                };

                db.Customers.AddRange(customers);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Seed all companies with initial data.
        /// </summary>
        public async Task SeedAllCompaniesAsync(MasterCRMDbContext masterDb)
        {
            var companies = await masterDb.Companies.AsNoTracking().ToListAsync();

            foreach (var company in companies)
            {
                await SeedCompanyDataAsync(company.CompanyId);
            }
        }
    }
}
