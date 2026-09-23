using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM_MusicStudioReservation.api.DTOs;
using CRM_MusicStudioReservation.api.Helpers;
using CRM_MusicStudioReservation.domain.entities;
using CRM_MusicStudioSystem.infrastructure.services;
using Microsoft.EntityFrameworkCore;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class LoyaltyEndpoints
    {
        public static void MapLoyaltyEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/loyalty-transactions");

            // ==================== LIST ====================
            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.LoyaltyTransactions.AsNoTracking();

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(lt => lt.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(lt => new LoyaltyTransactionResponseDto
                {
                    LoyaltyTransactionId = lt.LoyaltyTransactionId,
                    CustomerId = lt.CustomerId,
                    Points = lt.Points,
                    TransactionType = lt.TransactionType,
                    Description = lt.Description,
                    CreatedAt = lt.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<LoyaltyTransactionResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // ==================== LIST FOR CUSTOMER ====================
            group.MapGet("/customer/{customerId:int}", async (int companyId, int customerId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.LoyaltyTransactions.AsNoTracking().Where(lt => lt.CustomerId == customerId);

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(lt => lt.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(lt => new LoyaltyTransactionResponseDto
                {
                    LoyaltyTransactionId = lt.LoyaltyTransactionId,
                    CustomerId = lt.CustomerId,
                    Points = lt.Points,
                    TransactionType = lt.TransactionType,
                    Description = lt.Description,
                    CreatedAt = lt.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<LoyaltyTransactionResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // ==================== GET ONE ====================
            group.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var transaction = await db.LoyaltyTransactions.AsNoTracking().FirstOrDefaultAsync(lt => lt.LoyaltyTransactionId == id);

                if (transaction == null)
                    return Results.NotFound();

                return Results.Ok(new LoyaltyTransactionResponseDto
                {
                    LoyaltyTransactionId = transaction.LoyaltyTransactionId,
                    CustomerId = transaction.CustomerId,
                    Points = transaction.Points,
                    TransactionType = transaction.TransactionType,
                    Description = transaction.Description,
                    CreatedAt = transaction.CreatedAt
                });
            });

            // ==================== CREATE ====================
            group.MapPost("", async (
                int companyId,
                LoyaltyTransactionCreateDto createDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var context = new ValidationContext(createDto);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createDto, context, results, true))
                    return Results.BadRequest(results.Select(r => r.ErrorMessage));

                await using var db = await tenantFactory.CreateAsync(companyId);

                var customerExists = await db.Customers.AnyAsync(c => c.CustomerId == createDto.CustomerId);
                if (!customerExists)
                    return Results.BadRequest("Customer not found");

                var transaction = new LoyaltyTransaction
                {
                    CustomerId = createDto.CustomerId,
                    Points = createDto.Points,
                    TransactionType = createDto.TransactionType,
                    Description = createDto.Description
                };

                db.LoyaltyTransactions.Add(transaction);

                // Update customer's membership points
                int? newBalance = null;
                var membership = await db.Memberships.FirstOrDefaultAsync(m => m.CustomerId == createDto.CustomerId);
                if (membership != null)
                {
                    membership.LoyaltyPoints += createDto.Points;
                    newBalance = membership.LoyaltyPoints;
                    db.Memberships.Update(membership);
                }

                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                var sign = createDto.Points >= 0 ? "+" : "";
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "LoyaltyTransaction",
                    entityId: transaction.LoyaltyTransactionId,
                    newValue: $"CustomerId={transaction.CustomerId}, Type={transaction.TransactionType}, Points={sign}{transaction.Points}, Balance={(newBalance.HasValue ? newBalance.Value.ToString() : "no membership")}, Desc={transaction.Description ?? "(none)"}");

                return Results.Created($"/tenant/{companyId}/loyalty-transactions/{transaction.LoyaltyTransactionId}",
                    new LoyaltyTransactionResponseDto
                    {
                        LoyaltyTransactionId = transaction.LoyaltyTransactionId,
                        CustomerId = transaction.CustomerId,
                        Points = transaction.Points,
                        TransactionType = transaction.TransactionType,
                        Description = transaction.Description,
                        CreatedAt = transaction.CreatedAt
                    });
            });

            // ==================== BALANCE ====================
            group.MapGet("/balance/{customerId:int}", async (int companyId, int customerId, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);

                var membership = await db.Memberships.AsNoTracking().FirstOrDefaultAsync(m => m.CustomerId == customerId);
                if (membership == null)
                    return Results.NotFound("Customer does not have a membership");

                return Results.Ok(new
                {
                    customerId,
                    loyaltyPoints = membership.LoyaltyPoints
                });
            });
        }
    }
}