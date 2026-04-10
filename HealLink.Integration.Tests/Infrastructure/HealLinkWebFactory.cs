using healLink.Application.Interfaces;
using HealLink.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace HealLink.Integration.Tests.Infrastructure
{
    /// <summary>
    /// Spins up the full API against a real SQL Server / LocalDB database.
    /// Implements IAsyncLifetime so xunit calls InitializeAsync before the first
    /// test in each IClassFixture class — this drops and re-creates the database
    /// and applies all migrations, giving per-class isolation.
    ///
    /// Connection string resolution order:
    ///   1. Environment variable  HEALLINK_TEST_CONNECTION_STRING
    ///   2. appsettings.Testing.json  ConnectionStrings:TestConnection
    ///   3. InvalidOperationException (neither source found)
    /// </summary>
    public class HealLinkWebFactory : WebApplicationFactory<HealLink.Api.Program>, IAsyncLifetime
    {
        private readonly string _connectionString;

        // Serialises DB drop+recreate across test classes that run in parallel
        private static readonly SemaphoreSlim _dbLock = new(1, 1);

        public HealLinkWebFactory()
        {
            _connectionString = ResolveConnectionString();
        }

        private static string ResolveConnectionString()
        {
            // 1. Environment variable — used in CI
            var envVar = Environment.GetEnvironmentVariable("HEALLINK_TEST_CONNECTION_STRING");
            if (!string.IsNullOrWhiteSpace(envVar))
                return envVar;

            // 2. appsettings.Testing.json — used locally
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Testing.json", optional: true)
                .Build();

            var fromFile = config.GetConnectionString("TestConnection");
            if (!string.IsNullOrWhiteSpace(fromFile))
                return fromFile;

            throw new InvalidOperationException(
                "No test connection string found. " +
                "Set the HEALLINK_TEST_CONNECTION_STRING environment variable, " +
                "or create HealLink.Integration.Tests/appsettings.Testing.json " +
                "with a ConnectionStrings:TestConnection entry. " +
                "See appsettings.Testing.json.example for the required format.");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                // Remove the production DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<HealLinkDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Register with the test SQL Server connection string
                services.AddDbContext<HealLinkDbContext>(options =>
                    options.UseSqlServer(_connectionString));
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace real email service with no-op to prevent SMTP calls in tests
                var emailDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IEmailService));
                if (emailDescriptor != null)
                    services.Remove(emailDescriptor);
                services.AddScoped<IEmailService, FakeEmailService>();
            });

            return base.CreateHost(builder);
        }

        /// <summary>
        /// Called by xunit before the first test in each IClassFixture class.
        /// Drops and re-creates the database then applies all migrations.
        /// The static semaphore prevents parallel classes from racing on DB creation.
        /// </summary>
        public async Task InitializeAsync()
        {
            await _dbLock.WaitAsync();
            try
            {
                using var scope = Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<HealLinkDbContext>();
                await db.Database.EnsureDeletedAsync();
                await db.Database.MigrateAsync();
            }
            finally
            {
                _dbLock.Release();
            }
        }

        public new async Task DisposeAsync()
        {
            await base.DisposeAsync();
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
