using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public record DoctorRejectedEvent(
        Guid DoctorId,
        string Reason
    ) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
