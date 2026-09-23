using System;

namespace CRM.winforms.Services
{
    public class SessionManager
    {
        public string? JwtToken { get; private set; }
        public string? Email { get; private set; }
        public string? Role { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(JwtToken);

        public void SetSession(string token)
        {
            JwtToken = token;
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var doc = System.Text.Json.JsonDocument.Parse(json);
                    Email = doc.RootElement.GetProperty("email").GetString();
                    Role = doc.RootElement.GetProperty("role").GetString();
                }
                catch
                {
                    Email = null;
                    Role = null;
                }
            }
        }

        public void Clear()
        {
            JwtToken = null;
            Email = null;
            Role = null;
        }
    }
}
