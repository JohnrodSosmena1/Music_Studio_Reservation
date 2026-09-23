using CRM_MusicStudioSystem.infrastructure.data;
using CRM_MusicStudioSystem.infrastructure.services;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioReservation.api.Endpoints;
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
            Console.WriteLine("=== JWT AUTH FAILED ===");
            Console.WriteLine($"Exception: {context.Exception.GetType().Name}");
            Console.WriteLine($"Message: {context.Exception.Message}");
            if (context.Exception.InnerException != null)
            {
                Console.WriteLine($"Inner: {context.Exception.InnerException.Message}");
            }
            Console.WriteLine("=======================");
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

    // TODO: Re-enable when on a network that allows port 1433
    //var migrator = scope.ServiceProvider.GetService<ITenantMigrationService>();
    //if (migrator != null)
    //{
    //    await migrator.ApplyMigrationsAsync();
    //}
    //var seeder = scope.ServiceProvider.GetService<DataSeeder>();
    //if (seeder != null)
    //{
    //    var masterDb = scope.ServiceProvider.GetService<MasterCRMDbContext>();
    //    if (masterDb != null)
    //    {
    //        await seeder.SeedAllCompaniesAsync(masterDb);
    //    }
    //}
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

// ==================== Legacy inline endpoints ====================
app.MapPost("/companies", async (
    Company company,
    MasterCRMDbContext db) =>
{
    db.Companies.Add(company);
    await db.SaveChangesAsync();
    return Results.Created($"/companies/{company.CompanyId}", company);
});

app.MapPost("/devices", async (
    Device device,
    MasterCRMDbContext db) =>
{
    db.Devices.Add(device);
    await db.SaveChangesAsync();
    return Results.Created($"/devices/{device.DeviceId}", device);
});

app.MapPost("/company-databases", async (
    CompanyDatabase companyDatabase,
    MasterCRMDbContext db) =>
{
    db.CompanyDatabases.Add(companyDatabase);
    await db.SaveChangesAsync();
    return Results.Created(
        $"/company-databases/{companyDatabase.CompanyDatabaseId}",
        companyDatabase);
});

app.MapGet("/test-tenant/{companyId:int}", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var productCount = await tenantDb.Products.CountAsync();
    return Results.Ok(new { companyId, productCount });
});

app.MapPost("/tenant/{companyId:int}/products", async (
    int companyId,
    Product product,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    tenantDb.Products.Add(product);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/products/{product.ProductId}", product);
});

app.MapGet("/tenant/{companyId:int}/products", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var products = await tenantDb.Products.AsNoTracking().OrderBy(x => x.ProductId).ToListAsync();
    return Results.Ok(products);
});

app.MapPost("/tenant/{companyId:int}/customers", async (
    int companyId,
    Customer customer,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    tenantDb.Customers.Add(customer);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/customers/{customer.CustomerId}", customer);
});

app.MapGet("/tenant/{companyId:int}/customers", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var customers = await tenantDb.Customers.AsNoTracking().OrderBy(x => x.CustomerId).ToListAsync();
    return Results.Ok(customers);
});

app.MapPost("/tenant/{companyId:int}/suppliers", async (
    int companyId,
    Supplier supplier,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    tenantDb.Suppliers.Add(supplier);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/suppliers/{supplier.SupplierId}", supplier);
});

app.MapGet("/tenant/{companyId:int}/suppliers", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var suppliers = await tenantDb.Suppliers.AsNoTracking().OrderBy(x => x.SupplierId).ToListAsync();
    return Results.Ok(suppliers);
});


app.Run();