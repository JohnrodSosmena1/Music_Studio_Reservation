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
    public static class CustomerFeedbackEndpoints
    {
        public static void MapCustomerFeedbackEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/customer-feedback");

            // GET /customer-feedback - List all feedback with pagination
            group.MapGet("", async (int companyId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.CustomerFeedbacks.AsNoTracking();

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(cf => cf.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(cf => new CustomerFeedbackResponseDto
                {
                    FeedbackId = cf.FeedbackId,
                    CustomerId = cf.CustomerId,
                    Rating = cf.Rating,
                    Comments = cf.Comments,
                    CreatedAt = cf.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<CustomerFeedbackResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // GET /customer-feedback/{id}
            group.MapGet("/{id:int}", async (int companyId, int id, ITenantDbContextFactory tenantFactory) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var feedback = await db.CustomerFeedbacks.AsNoTracking().FirstOrDefaultAsync(cf => cf.FeedbackId == id);

                if (feedback == null)
                    return Results.NotFound();

                return Results.Ok(new CustomerFeedbackResponseDto
                {
                    FeedbackId = feedback.FeedbackId,
                    CustomerId = feedback.CustomerId,
                    Rating = feedback.Rating,
                    Comments = feedback.Comments,
                    CreatedAt = feedback.CreatedAt
                });
            });

            // GET /customer-feedback/customer/{customerId}
            group.MapGet("/customer/{customerId:int}", async (int companyId, int customerId, ITenantDbContextFactory tenantFactory, int page = 1, int pageSize = 20) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.CustomerFeedbacks.AsNoTracking().Where(cf => cf.CustomerId == customerId);

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(cf => cf.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(cf => new CustomerFeedbackResponseDto
                {
                    FeedbackId = cf.FeedbackId,
                    CustomerId = cf.CustomerId,
                    Rating = cf.Rating,
                    Comments = cf.Comments,
                    CreatedAt = cf.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<CustomerFeedbackResponseDto>
                {
                    Items = results,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                });
            });

            // ==================== POST /customer-feedback ====================
            group.MapPost("", async (
                int companyId,
                CustomerFeedbackCreateDto createDto,
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

                var feedback = new CustomerFeedback
                {
                    CustomerId = createDto.CustomerId,
                    Rating = createDto.Rating,
                    Comments = createDto.Comments
                };

                db.CustomerFeedbacks.Add(feedback);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "CustomerFeedback",
                    entityId: feedback.FeedbackId,
                    newValue: $"CustomerId={feedback.CustomerId}, Rating={feedback.Rating}, Comments={(string.IsNullOrWhiteSpace(feedback.Comments) ? "(none)" : feedback.Comments)}");

                return Results.Created($"/tenant/{companyId}/customer-feedback/{feedback.FeedbackId}",
                    new CustomerFeedbackResponseDto
                    {
                        FeedbackId = feedback.FeedbackId,
                        CustomerId = feedback.CustomerId,
                        Rating = feedback.Rating,
                        Comments = feedback.Comments,
                        CreatedAt = feedback.CreatedAt
                    });
            });

            // ==================== PUT /customer-feedback/{id} ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                CustomerFeedbackCreateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var feedback = await db.CustomerFeedbacks.FirstOrDefaultAsync(cf => cf.FeedbackId == id);

                if (feedback == null)
                    return Results.NotFound();

                var oldSnapshot = $"Rating={feedback.Rating}, Comments={feedback.Comments}";

                feedback.Rating = updateDto.Rating;
                if (!string.IsNullOrWhiteSpace(updateDto.Comments))
                    feedback.Comments = updateDto.Comments;

                db.CustomerFeedbacks.Update(feedback);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Update",
                    entityName: "CustomerFeedback",
                    entityId: feedback.FeedbackId,
                    oldValue: oldSnapshot,
                    newValue: $"Rating={feedback.Rating}, Comments={feedback.Comments}");

                return Results.Ok(new CustomerFeedbackResponseDto
                {
                    FeedbackId = feedback.FeedbackId,
                    CustomerId = feedback.CustomerId,
                    Rating = feedback.Rating,
                    Comments = feedback.Comments,
                    CreatedAt = feedback.CreatedAt
                });
            });

            // ==================== DELETE /customer-feedback/{id} ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var feedback = await db.CustomerFeedbacks.FirstOrDefaultAsync(cf => cf.FeedbackId == id);

                if (feedback == null)
                    return Results.NotFound();

                var oldSnapshot = $"CustomerId={feedback.CustomerId}, Rating={feedback.Rating}, Comments={feedback.Comments}";

                db.CustomerFeedbacks.Remove(feedback);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "CustomerFeedback",
                    entityId: id,
                    oldValue: oldSnapshot);

                return Results.NoContent();
            });
        }
    }
}