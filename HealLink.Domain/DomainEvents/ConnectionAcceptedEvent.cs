using System;
using HealLink.Domain.Base;
using MediatR;

namespace HealLink.Domain.DomainEvents
{
    // TODO: [DDD] Same MediatR coupling issue — INotification is redundant since IDomainEvent already extends it; remove the explicit INotification dependency from the domain layer.
    public record ConnectionAcceptedEvent(
        Guid ConnectionId,
        Guid DoctorId,
        Guid PatientId,
        DateTime AcceptedAt
    ) : IDomainEvent, INotification
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
