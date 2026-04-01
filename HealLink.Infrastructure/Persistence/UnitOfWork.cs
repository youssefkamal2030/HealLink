using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            // Collect domain events from all tracked aggregate roots before saving
            var aggregates = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = aggregates
                .SelectMany(a => a.DomainEvents)
                .ToList();

            // Clear events before save so re-entrancy doesn't double-dispatch
            foreach (var aggregate in aggregates)
                aggregate.ClearDomainEvents();

            await _context.SaveChangesAsync(cancellationToken);

            // Dispatch events after the transaction commits
            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
