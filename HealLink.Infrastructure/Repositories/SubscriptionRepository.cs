using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Aggregates;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly HealLinkDbContext _context;

        public SubscriptionRepository(HealLinkDbContext context) => _context = context;

        public async Task<SubscriptionAggregate> AddAsync(SubscriptionAggregate aggregate, CancellationToken cancellationToken = default)
        {
            await _context.Subscriptions.AddAsync(aggregate.Subscription, cancellationToken);
            foreach (var payment in aggregate.Payments)
                await _context.Payments.AddAsync(payment, cancellationToken);
            return aggregate;
        }

        public async Task<SubscriptionAggregate?> GetByIdAsync(Guid subscriptionId, CancellationToken cancellationToken = default)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.Id == subscriptionId, cancellationToken);

            if (subscription == null) return null;

            var payments = await _context.Payments
                .Where(p => p.SubscriptionId == subscriptionId)
                .ToListAsync(cancellationToken);

            return new SubscriptionAggregate(subscription, payments);
        }

        public async Task<List<Subscription>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
            => await _context.Subscriptions
                .Where(s => s.PatientId == patientId)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync(cancellationToken);

        public async Task<List<Subscription>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
            => await _context.Subscriptions
                .Where(s => s.DoctorId == doctorId)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync(cancellationToken);

        public Task UpdateAsync(SubscriptionAggregate aggregate, CancellationToken cancellationToken = default)
        {
            // Only mark the Subscription entity as modified.
            // Payments that were mutated via aggregate methods (MarkAsCompleted, MarkAsFailed, Refund)
            // are already tracked by EF change tracker — no explicit Update() needed.
            // Calling Update() on every payment would mark unmodified ones as dirty unnecessarily.
            _context.Subscriptions.Update(aggregate.Subscription);
            return Task.CompletedTask;
        }
    }
}
