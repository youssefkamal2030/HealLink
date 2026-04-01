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

        // TODO: [TEST-NEXT] Add authenticated connection flow tests:
        //   - Register a Patient and a Doctor, log both in, get their profile IDs from the DB.
        //   - POST /api/Connections/Request with the patient's JWT → assert 200 and connection row in DB with Status=Pending.
        //   - POST /api/Connections/Accept with the doctor's JWT → assert 200, connection Status=Accepted in DB, and Patient.ConnectedDoctorIds updated.
        //   - POST /api/Connections/Reject with the doctor's JWT → assert 200, connection removed from Doctor.PatientConnections.
        //   - POST /api/Connections/Request a second time for the same pair → assert failure "already exists".
        // TODO: [TEST-NEXT] Add a helper method RegisterAndGetProfileId(role) to HealLinkWebFactory or a shared TestHelpers class
        //   that registers a user, logs in, and returns (jwt, profileId) so connection tests don't duplicate that setup.

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
