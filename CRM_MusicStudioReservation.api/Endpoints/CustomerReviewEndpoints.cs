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
    public static class CustomerReviewEndpoints
    {
        public static void MapCustomerReviewEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/tenant/{companyId:int}/customer-reviews");

            // ==================== LIST ====================
            group.MapGet("", async (
                int companyId,
                ITenantDbContextFactory tenantFactory,
                int page = 1,
                int pageSize = 100,
                string? status = null,
                int? rating = null) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var query = db.CustomerReviews.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(status) && status != "All")
                    query = query.Where(r => r.ModerationStatus == status);

                if (rating.HasValue)
                    query = query.Where(r => r.Rating == rating.Value);

                var totalItems = await query.CountAsync();
                var totalPages = (totalItems + pageSize - 1) / pageSize;

                var items = await query
                    .OrderByDescending(r => r.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var results = items.Select(r => new CustomerReviewResponseDto
                {
                    CustomerReviewId = r.CustomerReviewId,
                    CustomerId = r.CustomerId,
                    StudioId = r.StudioId,
                    BookingId = r.BookingId,
                    ReviewType = r.ReviewType,
                    Title = r.Title,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ModerationStatus = r.ModerationStatus,
                    IsVerified = r.IsVerified,
                    AdminReply = r.AdminReply,
                    RepliedAt = r.RepliedAt,
                    CreatedAt = r.CreatedAt
                }).ToList();

                return Results.Ok(new PagingResponse<CustomerReviewResponseDto>
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
                var r = await db.CustomerReviews.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CustomerReviewId == id);

                if (r == null) return Results.NotFound();

                return Results.Ok(new CustomerReviewResponseDto
                {
                    CustomerReviewId = r.CustomerReviewId,
                    CustomerId = r.CustomerId,
                    StudioId = r.StudioId,
                    BookingId = r.BookingId,
                    ReviewType = r.ReviewType,
                    Title = r.Title,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ModerationStatus = r.ModerationStatus,
                    IsVerified = r.IsVerified,
                    AdminReply = r.AdminReply,
                    RepliedAt = r.RepliedAt,
                    CreatedAt = r.CreatedAt
                });
            });

            // ==================== CREATE ====================
            group.MapPost("", async (
                int companyId,
                CustomerReviewCreateDto createDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var ctx = new ValidationContext(createDto);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createDto, ctx, results, true))
                    return Results.BadRequest(results.Select(r => r.ErrorMessage));

                await using var db = await tenantFactory.CreateAsync(companyId);

                var customerExists = await db.Customers.AnyAsync(c => c.CustomerId == createDto.CustomerId);
                if (!customerExists)
                    return Results.BadRequest("Customer not found");

                if (createDto.StudioId.HasValue)
                {
                    var studioExists = await db.Studios.AnyAsync(s => s.StudioId == createDto.StudioId.Value);
                    if (!studioExists)
                        return Results.BadRequest("Studio not found");
                }

                bool isVerified = false;
                if (createDto.BookingId.HasValue)
                {
                    isVerified = await db.Bookings.AnyAsync(b =>
                        b.BookingId == createDto.BookingId.Value &&
                        b.CustomerId == createDto.CustomerId);
                }

                var review = new CustomerReview
                {
                    CustomerId = createDto.CustomerId,
                    StudioId = createDto.StudioId,
                    BookingId = createDto.BookingId,
                    ReviewType = string.IsNullOrWhiteSpace(createDto.ReviewType) ? "General" : createDto.ReviewType,
                    Title = createDto.Title,
                    Rating = createDto.Rating,
                    Comment = createDto.Comment,
                    ModerationStatus = "Pending",
                    IsVerified = isVerified,
                    CreatedAt = DateTime.UtcNow
                };

                db.CustomerReviews.Add(review);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Create",
                    entityName: "CustomerReview",
                    entityId: review.CustomerReviewId,
                    newValue: $"CustomerId={review.CustomerId}, Type={review.ReviewType}, Rating={review.Rating}, Title={review.Title ?? "(none)"}, Verified={review.IsVerified}");

                return Results.Created($"/tenant/{companyId}/customer-reviews/{review.CustomerReviewId}",
                    new CustomerReviewResponseDto
                    {
                        CustomerReviewId = review.CustomerReviewId,
                        CustomerId = review.CustomerId,
                        StudioId = review.StudioId,
                        BookingId = review.BookingId,
                        ReviewType = review.ReviewType,
                        Title = review.Title,
                        Rating = review.Rating,
                        Comment = review.Comment,
                        ModerationStatus = review.ModerationStatus,
                        IsVerified = review.IsVerified,
                        AdminReply = review.AdminReply,
                        RepliedAt = review.RepliedAt,
                        CreatedAt = review.CreatedAt
                    });
            });

            // ==================== UPDATE ====================
            group.MapPut("/{id:int}", async (
                int companyId,
                int id,
                CustomerReviewUpdateDto updateDto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var review = await db.CustomerReviews.FirstOrDefaultAsync(r => r.CustomerReviewId == id);

                if (review == null) return Results.NotFound();

                var oldSnapshot = $"Type={review.ReviewType}, Rating={review.Rating}, Title={review.Title}, Comment={review.Comment}";

                if (updateDto.Title != null) review.Title = updateDto.Title;
                if (updateDto.Rating.HasValue) review.Rating = updateDto.Rating.Value;
                if (!string.IsNullOrWhiteSpace(updateDto.Comment)) review.Comment = updateDto.Comment;
                if (!string.IsNullOrWhiteSpace(updateDto.ReviewType)) review.ReviewType = updateDto.ReviewType;

                db.CustomerReviews.Update(review);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Update",
                    entityName: "CustomerReview",
                    entityId: review.CustomerReviewId,
                    oldValue: oldSnapshot,
                    newValue: $"Type={review.ReviewType}, Rating={review.Rating}, Title={review.Title}, Comment={review.Comment}");

                return Results.Ok(new CustomerReviewResponseDto
                {
                    CustomerReviewId = review.CustomerReviewId,
                    CustomerId = review.CustomerId,
                    StudioId = review.StudioId,
                    BookingId = review.BookingId,
                    ReviewType = review.ReviewType,
                    Title = review.Title,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    ModerationStatus = review.ModerationStatus,
                    IsVerified = review.IsVerified,
                    AdminReply = review.AdminReply,
                    RepliedAt = review.RepliedAt,
                    CreatedAt = review.CreatedAt
                });
            });

            // ==================== MODERATE ====================
            group.MapPost("/{id:int}/moderate", async (
                int companyId,
                int id,
                CustomerReviewModerateDto dto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                var valid = new[] { "Pending", "Approved", "Rejected" };
                if (!valid.Contains(dto.ModerationStatus))
                    return Results.BadRequest("ModerationStatus must be Pending, Approved, or Rejected");

                await using var db = await tenantFactory.CreateAsync(companyId);
                var review = await db.CustomerReviews.FirstOrDefaultAsync(r => r.CustomerReviewId == id);

                if (review == null) return Results.NotFound();

                var oldStatus = review.ModerationStatus;

                review.ModerationStatus = dto.ModerationStatus;
                db.CustomerReviews.Update(review);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG — Moderation action
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: $"Moderate:{oldStatus}->{dto.ModerationStatus}",
                    entityName: "CustomerReview",
                    entityId: review.CustomerReviewId,
                    oldValue: $"Status={oldStatus}",
                    newValue: $"Status={dto.ModerationStatus}");

                return Results.Ok(new { review.CustomerReviewId, review.ModerationStatus });
            });

            // ==================== REPLY ====================
            group.MapPost("/{id:int}/reply", async (
                int companyId,
                int id,
                CustomerReviewReplyDto dto,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                if (string.IsNullOrWhiteSpace(dto.AdminReply))
                    return Results.BadRequest("Reply cannot be empty");

                await using var db = await tenantFactory.CreateAsync(companyId);
                var review = await db.CustomerReviews.FirstOrDefaultAsync(r => r.CustomerReviewId == id);

                if (review == null) return Results.NotFound();

                var oldReply = review.AdminReply;

                review.AdminReply = dto.AdminReply.Trim();
                review.RepliedAt = DateTime.UtcNow;
                db.CustomerReviews.Update(review);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG — Reply action
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: oldReply == null ? "Reply" : "UpdateReply",
                    entityName: "CustomerReview",
                    entityId: review.CustomerReviewId,
                    oldValue: oldReply == null ? null : $"Reply={oldReply}",
                    newValue: $"Reply={review.AdminReply}");

                return Results.Ok(new { review.CustomerReviewId, review.AdminReply, review.RepliedAt });
            });

            // ==================== DELETE ====================
            group.MapDelete("/{id:int}", async (
                int companyId,
                int id,
                ITenantDbContextFactory tenantFactory,
                IAuditService auditService,
                HttpContext httpContext,
                ClaimsPrincipal user) =>
            {
                await using var db = await tenantFactory.CreateAsync(companyId);
                var review = await db.CustomerReviews.FirstOrDefaultAsync(r => r.CustomerReviewId == id);

                if (review == null) return Results.NotFound();

                var oldSnapshot = $"CustomerId={review.CustomerId}, Type={review.ReviewType}, Rating={review.Rating}, Title={review.Title}";

                db.CustomerReviews.Remove(review);
                await db.SaveChangesAsync();

                // 👇 AUDIT LOG
                await AuditHelper.LogAsync(auditService, httpContext, user, companyId,
                    action: "Delete",
                    entityName: "CustomerReview",
                    entityId: id,
                    oldValue: oldSnapshot);

                return Results.NoContent();
            });
        }
    }
}