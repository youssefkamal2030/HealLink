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
            // [ARCHITECTURAL] This works correctly TODAY because AddConnectedDoctor/RemoveConnectedDoctor do not
            // call AddDomainEvent. If they ever do, the second SaveChangesAsync will dispatch those
            // new events, creating a cascading chain (documented below in architectural concerns).
            foreach (var aggregate in aggregates)
                aggregate.ClearDomainEvents();

            await _context.SaveChangesAsync(cancellationToken);

            // [ARCHITECTURAL-CONCERN-1] Events are dispatched in-process via MediatR after the DB commit.
            // ISSUE: This means if the process crashes between SaveChanges and the last Publish, the DB
            // write succeeded but one or more side effects (notifications, state projections) are
            // permanently lost.
            // CURRENT-STATUS: Acceptable for MVP - low event volume, low failure probability
            // FUTURE-ENHANCEMENT: For production use with real patient data, consider an outbox
            // pattern: persist events to an OutboxMessage table inside the same transaction, then
            // have a background worker dispatch and delete them with at-least-once guarantees.

            // [ARCHITECTURAL-CONCERN-2] Events are dispatched sequentially (one await per event). This is
            // non-blocking (async I/O, no thread starvation) but it is serial. For the current
            // event volume (1–3 events per command) this is fine.
            // FUTURE-ENHANCEMENT: If event fan-out grows, consider
            // Task.WhenAll for independent events — but only after handlers are made idempotent.

            // RESOLVED: The nested SaveChangesAsync issue that previously existed in ConnectionAcceptedEventHandler
            // and ConnectionRejectedEventHandler has been fixed. Both handlers are now pure notification
            // dispatchers with no DB writes. Patient.ConnectedDoctorIds is mutated in the command handlers
            // (AcceptConnectionCommandHandler / RejectConnectionCommandHandler) in the same transaction as
            // the Doctor aggregate, before events are dispatched. Event handlers never call SaveChangesAsync.

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
