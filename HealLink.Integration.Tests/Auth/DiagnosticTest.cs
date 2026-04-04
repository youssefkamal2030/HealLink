// TODO: [TEST-NEXT] Delete this file once all integration tests are stable.
using HealLink.Integration.Tests.Infrastructure;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace HealLink.Integration.Tests.Auth
{
    public class DiagnosticTest : IClassFixture<HealLinkWebFactory>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public DiagnosticTest(HealLinkWebFactory factory, ITestOutputHelper output)
        {
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

            var confirmResponse = await _client.PostAsJsonAsync("api/Auth/confirm-email",
                new { Email = email, Code = FakeEmailService.TestOtpCode });
            var body = await confirmResponse.Content.ReadAsStringAsync();
            _output.WriteLine($"Status: {confirmResponse.StatusCode}");
            _output.WriteLine($"Body: {body}");
            Assert.True(true);
        }
    }
}
