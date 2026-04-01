using System.Net;
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

        // Register uses [FromForm] so we send multipart/form-data
        private MultipartFormDataContent ValidPatientForm(string email = "patient@test.com")
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("testpatient"), "username");
            form.Add(new StringContent("Test@1234"), "Password");
            form.Add(new StringContent(email), "Email");
            form.Add(new StringContent("Patient"), "Role");
            return form;
        }

        // ── Happy path ───────────────────────────────────────────────────────

        [Fact]
        public async Task Register_WithValidPatientPayload_Returns200()
        {
            var response = await _client.PostAsync("/Auth/register", ValidPatientForm($"ok_{Guid.NewGuid()}@test.com"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Register_WithValidPayload_CreatesUserInDatabase()
        {
            var email = $"db_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("/Auth/register", ValidPatientForm(email));

            using var db = _factory.CreateDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

            Assert.NotNull(user);
        }

        [Fact]
        public async Task Register_WithValidPatientPayload_CreatesPatientProfile()
        {
            var email = $"profile_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("/Auth/register", ValidPatientForm(email));

            using var db = _factory.CreateDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            Assert.NotNull(user);

            var patient = await db.Patients.FirstOrDefaultAsync(p => p.UserId == user.Id);
            Assert.NotNull(patient);
        }

        // ── Duplicate email ──────────────────────────────────────────────────

        [Fact]
        public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
        {
            var email = $"dup_{Guid.NewGuid()}@test.com";
            await _client.PostAsync("/Auth/register", ValidPatientForm(email));

            var secondResponse = await _client.PostAsync("/Auth/register", ValidPatientForm(email));
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

            var response = await _client.PostAsync("/Auth/register", form);

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

            var response = await _client.PostAsync("/Auth/register", form);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
