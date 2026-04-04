using System.Net;
using System.Net.Http.Json;
using HealLink.Infrastructure.Data;
using HealLink.Integration.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HealLink.Integration.Tests.Auth
{
    public class RegisterEndpointTests : IClassFixture<HealLinkWebFactory>
    {
        private readonly HealLinkWebFactory _factory;
        private readonly HttpClient _client;

        public RegisterEndpointTests(HealLinkWebFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private MultipartFormDataContent ValidPatientForm(string email = "patient@test.com")
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("testpatient"), "username");
            form.Add(new StringContent("Test@1234"), "Password");
            form.Add(new StringContent(email), "Email");
            form.Add(new StringContent("Patient"), "Role");
            return form;
        }

        private async Task ConfirmEmailAsync(string email)
        {
            await _client.PostAsJsonAsync("api/Auth/confirm-email",
                new { Email = email, Code = FakeEmailService.TestOtpCode });
        }

        // ── Happy path ───────────────────────────────────────────────────────

        [Fact]
        public async Task Register_WithValidPatientPayload_Returns200()
        {
            var response = await _client.PostAsync("api/Auth/register", ValidPatientForm($"ok_{Guid.NewGuid()}@test.com"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Register_WithValidPayload_CreatesUserInDatabase()
        {
            var email = $"db_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("api/Auth/register", ValidPatientForm(email));

            using var db = _factory.CreateDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Register_WithValidPatientPayload_CreatesPatientProfile()
        {
            var email = $"profile_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("api/Auth/register", ValidPatientForm(email));

            using var db = _factory.CreateDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            Assert.NotNull(user);
            var patient = await db.Patients.FirstOrDefaultAsync(p => p.UserId == user.Id);
            Assert.NotNull(patient);
        }

        // ── Confirm email ────────────────────────────────────────────────────

        [Fact]
        public async Task ConfirmEmail_WithValidOtp_Returns200()
        {
            var email = $"confirm_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("api/Auth/register", ValidPatientForm(email));

            var response = await _client.PostAsJsonAsync("api/Auth/confirm-email",
                new { Email = email, Code = FakeEmailService.TestOtpCode });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ConfirmEmail_WithValidOtp_SetsEmailConfirmedInDatabase()
        {
            var email = $"confirmed_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("api/Auth/register", ValidPatientForm(email));
            await ConfirmEmailAsync(email);

            using var db = _factory.CreateDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            Assert.NotNull(user);
            Assert.True(user.EmailConfirmed);
        }

        [Fact]
        public async Task ConfirmEmail_WithWrongOtp_Returns400()
        {
            var email = $"wrongotp_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("api/Auth/register", ValidPatientForm(email));

            var response = await _client.PostAsJsonAsync("api/Auth/confirm-email",
                new { Email = email, Code = "999999" });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ConfirmEmail_WhenAlreadyConfirmed_Returns400()
        {
            var email = $"double_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("api/Auth/register", ValidPatientForm(email));
            await ConfirmEmailAsync(email);

            var response = await _client.PostAsJsonAsync("api/Auth/confirm-email",
                new { Email = email, Code = FakeEmailService.TestOtpCode });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // ── Duplicate email ──────────────────────────────────────────────────

        [Fact]
        public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
        {
            var email = $"dup_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("api/Auth/register", ValidPatientForm(email));
            var secondResponse = await _client.PostAsync("api/Auth/register", ValidPatientForm(email));
            var body = await secondResponse.Content.ReadAsStringAsync();
            Assert.Contains("Email Already Taken", body, StringComparison.OrdinalIgnoreCase);
        }

        // ── Validation ───────────────────────────────────────────────────────

        [Fact]
        public async Task Register_WithEmptyEmail_Returns400()
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("user"), "username");
            form.Add(new StringContent("Test@1234"), "Password");
            form.Add(new StringContent(""), "Email");
            form.Add(new StringContent("Patient"), "Role");

            var response = await _client.PostAsync("api/Auth/register", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_WithInvalidEmailFormat_Returns400()
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("user"), "username");
            form.Add(new StringContent("Test@1234"), "Password");
            form.Add(new StringContent("not-an-email"), "Email");
            form.Add(new StringContent("Patient"), "Role");

            var response = await _client.PostAsync("api/Auth/register", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
