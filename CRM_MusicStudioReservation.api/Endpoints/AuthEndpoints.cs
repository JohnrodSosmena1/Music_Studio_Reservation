using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM_MusicStudioSystem.infrastructure.services;
using CRM_MusicStudioReservation.domain.entities;
using Microsoft.AspNetCore.Authorization;

namespace CRM_MusicStudioReservation.api.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/auth");

            group.MapPost("/login", Login).AllowAnonymous();
            group.MapGet("/me", GetCurrentUser).RequireAuthorization();
        }

        // ==================== LOGIN ====================
        private static async Task<IResult> Login(
            LoginRequest request,
            IUserService userService,
            ITokenService tokenService,
            IAuditService auditService,
            HttpContext httpContext)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return Results.BadRequest(new { message = "Email and password are required." });

            AppUser? user;
            try
            {
                user = await userService.AuthenticateAsync(request.Email, request.Password);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Results.Problem(
                    statusCode: 503,
                    title: "Database Unavailable",
                    detail: $"Database connection failed (Error {ex.Number}): {ex.Message}. Please check server connectivity or firewall whitelisting.");
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    statusCode: 500,
                    title: "Authentication Error",
                    detail: ex.Message);
            }

            if (user is null)
                return Results.Unauthorized();

            await userService.UpdateLastLoginAsync(user.UserId);

            var token = tokenService.GenerateToken(user);
            var expiresAt = DateTime.UtcNow.AddMinutes(120);

            // 👇 AUDIT LOG
            try
            {
                if (user.CompanyId.HasValue)
                {
                    var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    await auditService.LogActionAsync(
                        companyId: user.CompanyId.Value,
                        userId: user.UserId.ToString(),
                        userEmail: user.Email,
                        userRole: user.Role.ToString(),
                        action: "Login",
                        entityName: "User",
                        entityId: user.UserId,
                        newValue: $"Login successful from {ip}",
                        ipAddress: ip);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Audit] Login log failed: {ex.Message}");
            }

            return Results.Ok(new LoginResponse
            {
                Token = token,
                ExpiresAt = expiresAt,
                User = new UserInfo
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToString(),
                    CompanyId = user.CompanyId
                }
            });
        }

        // ==================== GET CURRENT USER ====================
        private static IResult GetCurrentUser(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var name = user.FindFirst(ClaimTypes.Name)?.Value;
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var companyId = user.FindFirst("companyId")?.Value;

            return Results.Ok(new UserInfo
            {
                UserId = int.TryParse(userId, out var id) ? id : 0,
                Email = email ?? string.Empty,
                FullName = name ?? string.Empty,
                Role = role ?? string.Empty,
                CompanyId = int.TryParse(companyId, out var cid) ? cid : null
            });
        }
    }

    // ==================== DTOs ====================
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public UserInfo User { get; set; } = new();
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? CompanyId { get; set; }
    }
}