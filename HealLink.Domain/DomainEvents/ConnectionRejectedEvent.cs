using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public record ConnectionRejectedEvent(
        Guid ConnectionId,
        Guid DoctorId,
        Guid PatientId
    ) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
