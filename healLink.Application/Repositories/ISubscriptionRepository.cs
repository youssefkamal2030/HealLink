using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Aggregates;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<SubscriptionAggregate> AddAsync(SubscriptionAggregate aggregate, CancellationToken cancellationToken = default);
        Task<SubscriptionAggregate?> GetByIdAsync(Guid subscriptionId, CancellationToken cancellationToken = default);
        Task<List<Subscription>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<List<Subscription>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
        Task UpdateAsync(SubscriptionAggregate aggregate, CancellationToken cancellationToken = default);
    }
}
