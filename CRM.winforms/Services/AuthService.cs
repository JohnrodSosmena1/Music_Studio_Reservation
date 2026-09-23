using System;
using System.Threading.Tasks;

namespace CRM.winforms.Services
{
    /// <summary>
    /// Mock authentication service for initial development.
    /// Returns a fake JWT token and seeds role based on email.
    /// </summary>
    public class AuthService : IAuthService
    {
        public Task<string?> LoginAsync(string email, string password, bool rememberMe = false)
        {
            // Simple mock rules
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return Task.FromResult<string?>(null);

            email = email.Trim().ToLowerInvariant();

            if (email == "superadmin@musicstudio.com" && password == "Admin@123")
            {
                return Task.FromResult<string?>(GenerateToken("superadmin@musicstudio.com", "SuperAdmin"));
            }

            if (email == "admin@company1.com" && password == "Admin@123")
            {
                return Task.FromResult<string?>(GenerateToken("admin@company1.com", "Admin"));
            }

            if (email == "staff@company1.com" && password == "Staff@123")
            {
                return Task.FromResult<string?>(GenerateToken("staff@company1.com", "Staff"));
            }

            if (email == "client@company1.com" && password == "Client@123")
            {
                return Task.FromResult<string?>(GenerateToken("client@company1.com", "Client"));
            }

            // For any other combination, accept as Client for mock purposes
            return Task.FromResult<string?>(GenerateToken(email, "Client"));
        }

        private string GenerateToken(string email, string role)
        {
            // Fake token payload, not a real JWT. Good enough for UI flows.
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{{\"email\":\"{email}\",\"role\":\"{role}\",\"exp\":\"{DateTime.UtcNow.AddHours(8):O}\"}}"));
        }
    }
}
