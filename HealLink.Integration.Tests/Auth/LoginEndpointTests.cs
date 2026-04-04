using System.Net;
using System.Net.Http.Json;
using HealLink.Integration.Tests.Infrastructure;
using Xunit;

namespace HealLink.Integration.Tests.Auth
{
    public class LoginEndpointTests : IClassFixture<HealLinkWebFactory>
    {
        private readonly HttpClient _client;

        public LoginEndpointTests(HealLinkWebFactory factory)
        {
            _client = factory.CreateClient();
        }

        private async Task RegisterAndConfirmAsync(string email, string password = "Test@1234")
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("loginuser"), "username");
            form.Add(new StringContent(password), "Password");
            form.Add(new StringContent(email), "Email");
            form.Add(new StringContent("Patient"), "Role");
            await _client.PostAsync("api/Auth/register", form);

            // FakeEmailService always uses "000000" as the OTP code
            await _client.PostAsJsonAsync("api/Auth/confirm-email",
                new { Email = email, Code = FakeEmailService.TestOtpCode });
        }

        // ── Happy path ───────────────────────────────────────────────────────

        [Fact]
        public async Task Login_WithValidCredentials_Returns200()
        {
            var email = $"login_{Guid.NewGuid()}@test.com";
            await RegisterAndConfirmAsync(email);

            var response = await _client.PostAsJsonAsync("api/Auth/login",
                new { Email = email, Password = "Test@1234" });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsNonEmptyBody()
        {
            var email = $"token_{Guid.NewGuid()}@test.com";
            await RegisterAndConfirmAsync(email);

            var response = await _client.PostAsJsonAsync("api/Auth/login",
                new { Email = email, Password = "Test@1234" });

            var body = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(body));
        }

        [Fact]
        public async Task Login_WithUnconfirmedEmail_ReturnsUnauthorized()
        {
            var email = $"unconfirmed_{Guid.NewGuid()}@test.com";
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("loginuser"), "username");
            form.Add(new StringContent("Test@1234"), "Password");
            form.Add(new StringContent(email), "Email");
            form.Add(new StringContent("Patient"), "Role");
            await _client.PostAsync("api/Auth/register", form);
            // deliberately skip confirm-email

            var response = await _client.PostAsJsonAsync("api/Auth/login",
                new { Email = email, Password = "Test@1234" });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // ── Wrong credentials ────────────────────────────────────────────────

        [Fact]
        public async Task Login_WithNonExistentEmail_ReturnsUnauthorized()
        {
            var response = await _client.PostAsJsonAsync("api/Auth/login",
                new { Email = "nobody@nowhere.com", Password = "Test@1234" });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // ── Validation ───────────────────────────────────────────────────────

        [Fact]
        public async Task Login_WithEmptyEmail_Returns400()
        {
            var response = await _client.PostAsJsonAsync("api/Auth/login",
                new { Email = "", Password = "Test@1234" });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithInvalidEmailFormat_Returns400()
        {
            var response = await _client.PostAsJsonAsync("api/Auth/login",
                new { Email = "not-an-email", Password = "Test@1234" });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithEmptyPassword_Returns400()
        {
            var response = await _client.PostAsJsonAsync("api/Auth/login",
                new { Email = "valid@test.com", Password = "" });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
