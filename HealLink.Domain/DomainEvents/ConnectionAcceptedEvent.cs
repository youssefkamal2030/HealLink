using System;
using HealLink.Domain.Base;
using MediatR;

namespace HealLink.Domain.DomainEvents
{
   
    public record ConnectionAcceptedEvent(
        Guid ConnectionId,
        Guid DoctorId,
        Guid PatientId,
        DateTime AcceptedAt
    ) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
