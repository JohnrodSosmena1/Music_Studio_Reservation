using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace CRM.winforms.Services
{
    /// <summary>
    /// Central HTTP client for talking to the CRM API.
    /// Handles JWT token injection, JSON serialization, and error responses.
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        public string? Token { get; private set; }

        public ApiClient(string baseUrl)
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/"),
                Timeout = TimeSpan.FromSeconds(30)
            };

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>Sets the JWT bearer token for subsequent requests.</summary>
        public void SetToken(string? token)
        {
            Token = token;
        }

        /// <summary>Clears the token (used on logout).</summary>
        public void ClearToken()
        {
            Token = null;
        }

        // ==================== GENERIC HELPERS ====================

        public async Task<T?> GetAsync<T>(string path)
        {
            using var req = BuildRequest(HttpMethod.Get, path);
            using var res = await _http.SendAsync(req);
            return await ReadResponse<T>(res);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string path, TRequest body)
        {
            using var req = BuildRequest(HttpMethod.Post, path);
            req.Content = JsonContent.Create(body, options: _jsonOptions);

            using var res = await _http.SendAsync(req);
            return await ReadResponse<TResponse>(res);
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string path, TRequest body)
        {
            using var req = BuildRequest(HttpMethod.Put, path);
            req.Content = JsonContent.Create(body, options: _jsonOptions);

            using var res = await _http.SendAsync(req);
            return await ReadResponse<TResponse>(res);
        }

        public async Task DeleteAsync(string path)
        {
            using var req = BuildRequest(HttpMethod.Delete, path);
            using var res = await _http.SendAsync(req);

            if (!res.IsSuccessStatusCode)
            {
                var body = await res.Content.ReadAsStringAsync();
                throw new ApiException(res.StatusCode, body);
            }
        }

        // ==================== INTERNALS ====================

        private HttpRequestMessage BuildRequest(HttpMethod method, string path)
        {
            var req = new HttpRequestMessage(method, path.TrimStart('/'));

            if (!string.IsNullOrWhiteSpace(Token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return req;
        }

        private async Task<T?> ReadResponse<T>(HttpResponseMessage res)
        {
            var content = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                throw new ApiException(res.StatusCode, content);
            }

            if (string.IsNullOrWhiteSpace(content))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(content, _jsonOptions);
            }
            catch (JsonException ex)
            {
                throw new ApiException(res.StatusCode, $"Invalid JSON: {ex.Message}\nBody: {content}");
            }
        }
    }

    /// <summary>Custom exception for API errors.</summary>
    public class ApiException : Exception
    {
        public System.Net.HttpStatusCode StatusCode { get; }

        public ApiException(System.Net.HttpStatusCode statusCode, string body)
            : base(BuildMessage(statusCode, body))
        {
            StatusCode = statusCode;
        }

        private static string BuildMessage(System.Net.HttpStatusCode code, string body)
        {
            var message = code switch
            {
                System.Net.HttpStatusCode.Unauthorized => "Invalid email or password (401).",
                System.Net.HttpStatusCode.Forbidden => "You don't have permission (403).",
                System.Net.HttpStatusCode.NotFound => "Resource not found (404).",
                System.Net.HttpStatusCode.Conflict => "Conflict — duplicate or invalid state (409).",
                System.Net.HttpStatusCode.BadRequest => "Invalid request (400).",
                System.Net.HttpStatusCode.InternalServerError => "Server error (500).",
                _ => $"Request failed with status {code}."
            };

            if (!string.IsNullOrWhiteSpace(body) && body.Length < 500)
            {
                message += "\n" + body;
            }

            return message;
        }
    }
}
