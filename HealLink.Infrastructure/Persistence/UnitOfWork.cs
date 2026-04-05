using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Adapters;
using healLink.Application.Interfaces;
using HealLink.Domain.Base;
using HealLink.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealLinkDbContext _context;
        private readonly IMediator _mediator;

        public UnitOfWork(HealLinkDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker knows all entities currently tracked by the DbContext and their state
            // (Added, Modified, Deleted). We use it to find every AggregateRoot that has raised
            // domain events during this unit of work so we can dispatch them after the DB commit.
            var aggregates = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = aggregates
                .SelectMany(a => a.DomainEvents)
                .ToList();

            // IMPORTANT — events are cleared BEFORE SaveChanges and BEFORE publishing.
            // This is what prevents an infinite loop when a handler calls SaveChangesAsync again:
            // the second call finds no pending events on any aggregate and publishes nothing.
            // This works correctly TODAY because AddConnectedDoctor/RemoveConnectedDoctor do not
            // call AddDomainEvent. If they ever do, the second SaveChangesAsync will dispatch those
            // new events, creating a cascading chain. See TODO below.
            foreach (var aggregate in aggregates)
                aggregate.ClearDomainEvents();

            await _context.SaveChangesAsync(cancellationToken);

            // TODO: [ARCH] Events are dispatched in-process via MediatR after the DB commit.
            // This means if the process crashes between SaveChanges and the last Publish, the DB
            // write succeeded but one or more side effects (notifications, state projections) are
            // permanently lost. For production use with real patient data, consider an outbox
            // pattern: persist events to an OutboxMessage table inside the same transaction, then
            // have a background worker dispatch and delete them with at-least-once guarantees.

            // TODO: [ARCH] Events are dispatched sequentially (one await per event). This is
            // non-blocking (async I/O, no thread starvation) but it is serial. For the current
            // event volume (1–3 events per command) this is fine. If event fan-out grows, consider
            // Task.WhenAll for independent events — but only after handlers are made idempotent.

            // TODO: [ARCH-CRITICAL] Nested SaveChangesAsync — ConnectionAcceptedHandler and
            // ConnectionRejectedHandler call _unitOfWork.SaveChangesAsync() from inside a handler
            // that was itself triggered by a SaveChangesAsync call. This does NOT loop today only
            // because AddConnectedDoctor/RemoveConnectedDoctor raise no domain events, so the
            // second SaveChangesAsync finds an empty event list and exits cleanly.
            // The correct fix (REFACTOR-P2): event handlers must not perform state mutations
            // directly. State mutations belong in command handlers. Handlers that need to update
            // read-model state (ConnectedDoctorIds) should dispatch a new command via IMediator
            // (e.g., UpdatePatientConnectionsCommand) which has its own clean SaveChangesAsync
            // call, keeping the event dispatch pipeline single-depth.

            foreach (var domainEvent in domainEvents)
            {
                // Reflection is used here to construct DomainEventNotification<T> at runtime
                // without knowing the concrete event type at compile time.
                // notificationType  == DomainEventNotification<YourConcreteEventType>
                // notification      == new DomainEventNotification<YourConcreteEventType>(domainEvent)
                var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
                var notification = (INotification)Activator.CreateInstance(notificationType, domainEvent);
                await _mediator.Publish(notification, cancellationToken);
            }
        }
    }
}
