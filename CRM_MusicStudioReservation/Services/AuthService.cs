using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CRM.winforms.Services
{
    /// <summary>
    /// Handles authentication: login, token storage, current user info.
    /// Wraps ApiClient to hide the HTTP details from forms.
    /// </summary>
    public class AuthService
    {
        private readonly ApiClient _api;

        public AuthService(ApiClient api)
        {
            _api = api;
        }

        /// <summary>Currently logged-in user, or null if not logged in.</summary>
        public UserInfo? CurrentUser { get; private set; }

        /// <summary>True if a user is logged in.</summary>
        public bool IsLoggedIn => CurrentUser is not null && !string.IsNullOrWhiteSpace(_api.Token);

        /// <summary>
        /// Attempts to log in. Returns true if successful, false otherwise.
        /// </summary>
        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var request = new LoginRequest { Email = email, Password = password };
                var response = await _api.PostAsync<LoginRequest, LoginResponse>("auth/login", request);

                if (response is null || string.IsNullOrWhiteSpace(response.Token))
                    return false;

                _api.SetToken(response.Token);
                CurrentUser = response.User;
                return true;
            }
            catch (ApiException)
            {
                // Login failed — return false rather than crash
                return false;
            }
        }

        /// <summary>Logs out the current user and clears the token.</summary>
        public void Logout()
        {
            _api.ClearToken();
            CurrentUser = null;
        }

        /// <summary>
        /// Refreshes the current user info by calling /auth/me.
        /// Useful after the app resumes or after a period of inactivity.
        /// </summary>
        public async Task<bool> RefreshCurrentUserAsync()
        {
            try
            {
                var user = await _api.GetAsync<UserInfo>("auth/me");
                if (user is not null)
                {
                    CurrentUser = user;
                    return true;
                }
                return false;
            }
            catch (ApiException)
            {
                return false;
            }
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
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("expiresAt")]
        public DateTime ExpiresAt { get; set; }

        [JsonPropertyName("user")]
        public UserInfo User { get; set; } = new();
    }

    public class UserInfo
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("companyId")]
        public int? CompanyId { get; set; }
    }
}
