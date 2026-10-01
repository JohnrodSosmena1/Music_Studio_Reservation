using CRM_MusicStudioSystem.infrastructure.data;
using CRM_MusicStudioSystem.infrastructure.services;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.api.Endpoints;
using CRM_MusicStudioReservation.api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Master CRM DbContext (existing)
builder.Services.AddDbContext<MasterCRMDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MasterCRM"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// 👇 Tenant CRM DbContext 👇
builder.Services.AddDbContext<TenantCRMDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TenantCRM")));

// 👇 Register tenant services 👇
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<ITenantDatabaseResolver, TenantDatabaseResolver>();
builder.Services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();
builder.Services.AddScoped<ITenantMigrationService, TenantMigrationService>();
builder.Services.AddScoped<IBookingService, CRM_MusicStudioSystem.infrastructure.services.BookingService>();

// 👇 Register Phase 2 business services 👇
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ILoyaltyService, LoyaltyService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<DataSeeder>();

// 👇 Auth services 👇
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 👇 JWT Authentication Configuration 👇
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key missing from configuration.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.FromMinutes(1)
    };

    // 👇 ADDED — logs why JWT validation fails 👇
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();
            logger.LogWarning("JWT auth failed: {ExceptionType} — {Message}",
                context.Exception.GetType().Name,
                context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// ============ DEBUG LOGGING (TEMPORARY — remove later) ============
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.SetMinimumLevel(LogLevel.Debug);
//builder.Logging.AddFilter("Microsoft.AspNetCore.Authentication", LogLevel.Trace);
//builder.Logging.AddFilter("Microsoft.IdentityModel", LogLevel.Trace);
//builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization", LogLevel.Trace);
// =================================================================

var app = builder.Build();

// ============ STARTUP TASKS ============
using (var scope = app.Services.CreateScope())
{
    var migrationService = scope.ServiceProvider.GetService<ITenantMigrationService>();
    if (migrationService != null)
    {
        try
        {
            await migrationService.ApplyMigrationsAsync();
            Console.WriteLine("[Startup] ✓ Tenant migrations applied.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Startup] ⚠ Migration notice: {ex.Message}");
        }
    }

    try
    {
        var seeder = scope.ServiceProvider.GetService<DataSeeder>();
        if (seeder != null)
        {
            var masterDb = scope.ServiceProvider.GetService<MasterCRMDbContext>();
            if (masterDb != null)
            {
                await seeder.SeedAllCompaniesAsync(masterDb);
                Console.WriteLine("[Startup] ✓ Initial tenant company data & customers seeded.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Startup] ⚠ Seeder notice: {ex.Message}");
    }

    var userService = scope.ServiceProvider.GetService<IUserService>();
    if (userService != null)
    {
        try
        {
            await userService.SeedDefaultUsersAsync();
            Console.WriteLine("[Startup] ✓ Default users seeded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Startup] ⚠ Could not seed users: {ex.Message}");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapStudioEndpoints();
app.MapBookingEndpoints();
app.MapStudioServiceEndpoints();
app.MapMembershipEndpoints();
app.MapMembershipPlanEndpoints();
app.MapLoyaltyEndpoints();
app.MapInventoryEndpoints();
app.MapPromotionEndpoints();
app.MapCustomerReviewEndpoints();
app.MapCustomerFeedbackEndpoints();
app.MapDashboardEndpoints();
app.MapAuthEndpoints();
app.MapCustomerEndpoints();
app.MapReportEndpoints();
app.MapCustomerInquiryEndpoints();
app.MapTandCEndpoints();
app.MapPromotionRationaleEndpoints();

// ==================== Super Admin Platform Endpoints ====================
app.MapSuperAdminDashboardEndpoints();
app.MapSuperAdminOrganizationEndpoints();
app.MapSuperAdminSubscriptionEndpoints();
app.MapSuperAdminTandCEndpoints();

// ==================== Legacy inline endpoints ====================
app.MapPost("/companies", async (
    Company company,
    MasterCRMDbContext db) =>
{
    db.Companies.Add(company);
    await db.SaveChangesAsync();
    return Results.Created($"/companies/{company.CompanyId}", company);
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));

app.MapPost("/devices", async (
    Device device,
    MasterCRMDbContext db) =>
{
    db.Devices.Add(device);
    await db.SaveChangesAsync();
    return Results.Created($"/devices/{device.DeviceId}", device);
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));

app.MapPost("/company-databases", async (
    CompanyDatabase companyDatabase,
    MasterCRMDbContext db) =>
{
    db.CompanyDatabases.Add(companyDatabase);
    await db.SaveChangesAsync();
    return Results.Created(
        $"/company-databases/{companyDatabase.CompanyDatabaseId}",
        companyDatabase);
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));

app.MapGet("/test-tenant/{companyId:int}", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var productCount = await tenantDb.Products.CountAsync();
    return Results.Ok(new { companyId, productCount });
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));

app.MapPost("/tenant/{companyId:int}/products", async (
    int companyId,
    Product product,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    if (string.IsNullOrWhiteSpace(product.ProductCode))
    {
        var count = await tenantDb.Products.CountAsync();
        product.ProductCode = $"PROD-{(count + 1):D5}";
    }
    tenantDb.Products.Add(product);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/products/{product.ProductId}", product);
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));

app.MapGet("/tenant/{companyId:int}/products", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var products = await tenantDb.Products.AsNoTracking().OrderBy(x => x.ProductId).ToListAsync();
    return Results.Ok(products);
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));

app.MapPost("/tenant/{companyId:int}/suppliers", async (
    int companyId,
    Supplier supplier,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    if (string.IsNullOrWhiteSpace(supplier.SupplierCode))
    {
        var supCount = await tenantDb.Suppliers.CountAsync();
        supplier.SupplierCode = $"SUPP-{(supCount + 1):D5}";
    }
    tenantDb.Suppliers.Add(supplier);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/suppliers/{supplier.SupplierId}", supplier);
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));

app.MapGet("/tenant/{companyId:int}/suppliers", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var suppliers = await tenantDb.Suppliers.AsNoTracking().OrderBy(x => x.SupplierId).ToListAsync();
    return Results.Ok(suppliers);
}).RequireAuthorization(p => p.RequireRole("SuperAdmin"));


app.Run();