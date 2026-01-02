using System;
using HealLink.Domain.Base;
using MediatR;

namespace HealLink.Domain.DomainEvents
{
    public record ConnectionRejectedEvent(
        Guid ConnectionId,
        Guid DoctorId,
        Guid PatientId
    ) : IDomainEvent, INotification
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
