using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public record ConnectionRequestCreatedEvent(Guid RequestId, Guid DoctorId, Guid PatientId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
