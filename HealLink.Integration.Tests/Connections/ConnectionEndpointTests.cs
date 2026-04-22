using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using HealLink.Integration.Tests.Infrastructure;
using Xunit;

namespace HealLink.Integration.Tests.Connections
{
    public class ConnectionEndpointTests : IClassFixture<HealLinkWebFactory>
    {
        private readonly HttpClient _client;

        public ConnectionEndpointTests(HealLinkWebFactory factory)
        {
            _client = factory.CreateClient();
        }

        // TODO: [TEST-NEXT] Implement full happy-path connection flow test:
        //   1. Register a Patient and a Doctor via POST /api/Auth/register
        //   2. Confirm both emails via POST /api/Auth/confirm-email
        //   3. Login both and capture JWTs
        //   4. Resolve patientId and doctorId from DB via factory.CreateDbContext()
        //   5. POST /api/Connections/Request with patient JWT → assert 200, DB row Status=Pending
        //   6. POST /api/Connections/Accept with doctor JWT → assert 200, Status=Accepted in DB,
        //      Patient.ConnectedDoctorIds contains doctorId
        //   7. POST /api/Connections/Request again for same pair → assert 400 "already exists"
        // TODO: [TEST-NEXT] Implement reject flow test:
        //   Same setup as above but POST /api/Connections/Reject with doctor JWT →
        //   assert 200, connection row removed from Doctor.PatientConnections in DB.
        // TODO: [TEST-NEXT] Add RegisterAndLoginAsync(role) helper to HealLinkWebFactory or a shared
        //   TestHelpers static class — registers a user, confirms email, logs in, returns (jwt, userId).
        //   Reuse across ConnectionEndpointTests, PrescriptionEndpointTests, SubscriptionEndpointTests.

        // ── Auth guard ───────────────────────────────────────────────────────

        [Fact]
        public async Task CreateConnection_WithoutToken_Returns401()
        {
            _client.DefaultRequestHeaders.Authorization = null;

            var response = await _client.PostAsJsonAsync("/api/Connections/Request",
                new { DoctorId = Guid.NewGuid(), PatientId = Guid.NewGuid() });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetPendingConnections_WithoutToken_Returns401()
        {
            _client.DefaultRequestHeaders.Authorization = null;

            var response = await _client.GetAsync($"/api/Connections/Doctor/{Guid.NewGuid()}/Pending");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetDoctorConnections_WithoutToken_Returns401()
        {
            _client.DefaultRequestHeaders.Authorization = null;

            var response = await _client.GetAsync($"/api/Connections/Doctor/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetPatientConnections_WithoutToken_Returns401()
        {
            _client.DefaultRequestHeaders.Authorization = null;

            var response = await _client.GetAsync($"/api/Connections/Patient/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
