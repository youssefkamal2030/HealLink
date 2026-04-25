using healLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using HealLink.Infrastructure.Data;
using HealLink.Integration.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HealLink.Integration.Tests.UnitOfWork
{
    /// <summary>
    /// Verifies that UnitOfWork correctly persists data and dispatches domain events.
    /// Uses the in-memory database via HealLinkWebFactory.
    /// </summary>
    public class UnitOfWorkTests : IClassFixture<HealLinkWebFactory>
    {
        private readonly HealLinkWebFactory _factory;

        public UnitOfWorkTests(HealLinkWebFactory factory)
        {
            _factory = factory;
        }

        private IServiceScope CreateScope() => _factory.Services.CreateScope();

        // ── Persistence ──────────────────────────────────────────────────────

        [Fact]
        public async Task SaveChangesAsync_PersistsAddedEntity()
        {
            using var scope = CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<HealLinkDbContext>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var user = User.Register("uowuser", "hash", $"uow_{Guid.NewGuid()}@test.com", UserRole.Patient);
            await db.Users.AddAsync(user);
            await uow.SaveChangesAsync();

            var saved = await db.Users.FindAsync(user.Id);
            Assert.NotNull(saved);
        }

        [Fact]
        public async Task SaveChangesAsync_PersistsUpdatedEntity()
        {
            using var scope = CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<HealLinkDbContext>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var user = User.Register("updateme", "hash", $"update_{Guid.NewGuid()}@test.com", UserRole.Patient);
            await db.Users.AddAsync(user);
            await uow.SaveChangesAsync();

            user.Activate();
            db.Users.Update(user);
            await uow.SaveChangesAsync();

            var updated = await db.Users.FindAsync(user.Id);
            Assert.Equal(AccountStatus.Active, updated!.Status);
        }

        // ── Domain event dispatch ────────────────────────────────────────────

        [Fact]
        public async Task SaveChangesAsync_ClearsDomainEventsAfterDispatch()
        {
            using var scope = CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<HealLinkDbContext>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var user = User.Register("evtuser", "hash", $"evt_{Guid.NewGuid()}@test.com", UserRole.Patient);
            // User constructor raises UserRegisteredEvent
            Assert.Single(user.DomainEvents);

            await db.Users.AddAsync(user);
            await uow.SaveChangesAsync();

            // Events should be cleared after dispatch
            Assert.Empty(user.DomainEvents);
        }

        [Fact]
        public async Task SaveChangesAsync_WithMultipleAggregates_DispatchesAllEvents()
        {
            using var scope = CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<HealLinkDbContext>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var user1 = User.Register("multi1", "hash", $"m1_{Guid.NewGuid()}@test.com", UserRole.Patient);
            var user2 = User.Register("multi2", "hash", $"m2_{Guid.NewGuid()}@test.com", UserRole.Doctor);

            await db.Users.AddRangeAsync(user1, user2);
            await uow.SaveChangesAsync();

            // Both aggregates should have their events cleared
            Assert.Empty(user1.DomainEvents);
            Assert.Empty(user2.DomainEvents);
        }

        // ── Atomicity ────────────────────────────────────────────────────────

        [Fact]
        public async Task SaveChangesAsync_WithoutCallingIt_DoesNotPersist()
        {
            using var scope = CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<HealLinkDbContext>();

            var email = $"nopersist_{Guid.NewGuid()}@test.com";
            var user = User.Register("nopersist", "hash", email, UserRole.Patient);
            await db.Users.AddAsync(user);
            // Intentionally NOT calling SaveChangesAsync

            // New scope = new DbContext = no uncommitted changes visible
            using var verifyScope = CreateScope();
            var verifyDb = verifyScope.ServiceProvider.GetRequiredService<HealLinkDbContext>();
            var found = await verifyDb.Users.FirstOrDefaultAsync(u => u.Email == email);

            Assert.Null(found);
        }
    }
}
