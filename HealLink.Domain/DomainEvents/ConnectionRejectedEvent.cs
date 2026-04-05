using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    // TODO: [DDD] Domain events should not directly depend on MediatR (INotification) in the domain layer — MediatR is an application/infrastructure concern. IDomainEvent already extends INotification via Base; the explicit INotification here is redundant but also leaks the dependency.
    public record ConnectionRejectedEvent(
        Guid ConnectionId,
        Guid DoctorId,
        Guid PatientId
    ) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
