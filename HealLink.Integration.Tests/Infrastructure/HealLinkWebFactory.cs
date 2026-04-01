using healLink.Application.Interfaces;
using HealLink.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HealLink.Integration.Tests.Infrastructure
{
    /// <summary>
    /// Spins up the full API with an in-memory database.
    /// Sets environment to "Testing" so Program.cs uses UseInMemoryDatabase instead of SQL Server.
    /// </summary>
    // TODO: [TEST-NEXT] Each IClassFixture<HealLinkWebFactory> shares one in-memory DB across all tests in the class.
    //   Tests that write data can bleed into each other. For full isolation, implement a per-test factory by
    //   having each test class create its own HealLinkWebFactory instance (implement IDisposable) instead of using IClassFixture.
    //   Alternatively, wrap each test in a transaction and roll it back after — but that requires exposing the DbContext transaction.
    // TODO: [TEST-NEXT] Add a helper method: Task<(string jwt, Guid profileId)> RegisterAndLoginAsync(string role)
    //   that registers a user, logs in, queries the DB for the profile ID, and returns both.
    //   This will be needed by ConnectionEndpointTests, ProfileEndpointTests, and NotificationEndpointTests.
    public class HealLinkWebFactory : WebApplicationFactory<HealLink.Api.Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            // Signal Program.cs to use in-memory DB instead of SQL Server
            builder.UseSetting("UseInMemoryDatabase", "true");
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace real email service with no-op to prevent SMTP calls in tests
                var emailDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IEmailService));
                if (emailDescriptor != null) services.Remove(emailDescriptor);
                services.AddScoped<IEmailService, FakeEmailService>();
            });

            return base.CreateHost(builder);
        }

        /// <summary>
        /// Creates a fresh scoped DbContext for direct database assertions in tests.
        /// </summary>
        public HealLinkDbContext CreateDbContext()
        {
            var scope = Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<HealLinkDbContext>();
        }
    }
}
