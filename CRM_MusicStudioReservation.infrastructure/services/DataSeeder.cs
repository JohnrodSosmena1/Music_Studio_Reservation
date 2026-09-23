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

                if (instrumentsCategory != null && equipmentCategory != null)
                {
                    var items = new List<InventoryItem>
                    {
                        new InventoryItem
                        {
                            ItemCode = "DRUM001",
                            ItemName = "Ludwig Classic Drums",
                            InventoryCategoryId = instrumentsCategory.InventoryCategoryId,
                            QuantityOnHand = 3,
                            ReorderLevel = 1,
                            UnitCost = 3500m,
                            IsActive = true
                        },
                        new InventoryItem
                        {
                            ItemCode = "BASS001",
                            ItemName = "Fender Bass Guitar",
                            InventoryCategoryId = instrumentsCategory.InventoryCategoryId,
                            QuantityOnHand = 5,
                            ReorderLevel = 2,
                            UnitCost = 1200m,
                            IsActive = true
                        },
                        new InventoryItem
                        {
                            ItemCode = "MIC001",
                            ItemName = "Neumann U87 Microphone",
                            InventoryCategoryId = equipmentCategory.InventoryCategoryId,
                            QuantityOnHand = 2,
                            ReorderLevel = 1,
                            UnitCost = 3200m,
                            IsActive = true
                        },
                        new InventoryItem
                        {
                            ItemCode = "AMP001",
                            ItemName = "Marshall Amplifier",
                            InventoryCategoryId = equipmentCategory.InventoryCategoryId,
                            QuantityOnHand = 4,
                            ReorderLevel = 1,
                            UnitCost = 1800m,
                            IsActive = true
                        }
                    };

                    db.InventoryItems.AddRange(items);
                    await db.SaveChangesAsync();
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
