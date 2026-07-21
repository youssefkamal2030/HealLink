using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    // why is there a reason here as a string while we have a value object of rejection contains the details of the rejection ? should we use that instead of a string here ?
    // this should be a value object of rejection instead of a string, but for now we will keep it as a string to avoid breaking changes. We can refactor this later.
    public record DoctorRejectedEvent(
        Guid DoctorId,
        string Reason
    ) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
    }

