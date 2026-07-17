    // [TEST-CLEANUP] This diagnostic test file should be deleted once integration tests are stable
    // PURPOSE: Used during development to verify test infrastructure setup
    // ACTION-NEEDED: Delete this file after verifying RegisterEndpointTests and LoginEndpointTests pass
    // STATUS: Can be deleted now - integration tests are working
using HealLink.Integration.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace HealLink.Integration.Tests.Auth
{
    public class DiagnosticTest : IClassFixture<HealLinkWebFactory>
    {
        private readonly HealLinkWebFactory _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public DiagnosticTest(HealLinkWebFactory factory, ITestOutputHelper output)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _output = output;
        }

        [Fact]
        public async Task Register_PrintsResponseBody()
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("testuser"), "username");
            form.Add(new StringContent("Test@1234"), "Password");
            form.Add(new StringContent($"diag_{Guid.NewGuid()}@test.com"), "Email");
            form.Add(new StringContent("Patient"), "Role");

            var response = await _client.PostAsync("api/Auth/register", form);
            var body = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Status: {response.StatusCode}");
            _output.WriteLine($"Body: {body}");
            Assert.True(true);
        }

        [Fact]
        public async Task ConfirmEmail_PrintsResponseBody()
        {
            var email = $"diag2_{Guid.NewGuid()}@test.com";
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("testuser"), "username");
            form.Add(new StringContent("Test@1234"), "Password");
            form.Add(new StringContent(email), "Email");
            form.Add(new StringContent("Patient"), "Role");
            await _client.PostAsync("api/Auth/register", form);

            // Fetch the real OTP from the database
            using var db = _factory.CreateDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            var otp = await db.OTPs
                .Where(o => o.UserId == user!.Id && !o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            var confirmResponse = await _client.PostAsJsonAsync("api/Auth/confirm-email",
                new { Email = email, Code = otp?.Code ?? "000000" });
            var body = await confirmResponse.Content.ReadAsStringAsync();
            _output.WriteLine($"Status: {confirmResponse.StatusCode}");
            _output.WriteLine($"Body: {body}");
            Assert.True(true);
        }
    }
}
