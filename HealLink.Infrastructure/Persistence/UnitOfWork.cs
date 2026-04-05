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
            var aggregates = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = aggregates
                .SelectMany(a => a.DomainEvents)
                .ToList();

            foreach (var aggregate in aggregates)
                aggregate.ClearDomainEvents();

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                // using reflection to create a domain event notification for each domain event and publish it using MediatR during runtime without knowing the specific type of the domain event at compile time
                var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()); // this line will equal to DomainEventNotification<YourDomainEventType>
                var notification = (INotification)Activator.CreateInstance(notificationType, domainEvent); // this line will equal to new DomainEventNotification<YourDomainEventType>(domainEvent)
                await _mediator.Publish(notification, cancellationToken);
            }
             
        }
    }
}
