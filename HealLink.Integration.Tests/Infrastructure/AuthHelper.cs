using System.Net.Http.Json;
using System.Text.Json;

namespace HealLink.Integration.Tests.Infrastructure
{
    /// <summary>
    /// Helpers for obtaining JWT tokens in integration tests.
    /// </summary>
    public static class AuthHelper
    {
        public static async Task<string?> RegisterAndLoginAsync(
            HttpClient client,
            string username = "testuser",
            string email = "test@heallink.com",
            string password = "Test@1234",
            string role = "Patient")
        {
            // Register
            var registerPayload = new
            {
                username,
                password,
                email,
                Role = role == "Patient" ? 1 : 2
            };

            await client.PostAsJsonAsync("/api/auth/register", registerPayload);

            // Login
            var loginPayload = new { Email = email, Password = password };
            var loginResponse = await client.PostAsJsonAsync("/api/auth/login", loginPayload);

            if (!loginResponse.IsSuccessStatusCode)
                return null;

            var json = await loginResponse.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            // Try common token field names
            if (doc.RootElement.TryGetProperty("token", out var token))
                return token.GetString();
            if (doc.RootElement.TryGetProperty("Token", out token))
                return token.GetString();

            return null;
        }
    }
}
