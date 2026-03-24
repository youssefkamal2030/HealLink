using System;
using MediatR;

namespace HealLink.Domain.Base
{
    // TODO: [DDD] IDomainEvent directly extends INotification (MediatR) — this couples the domain layer to an application/infrastructure framework. The domain should define its own IDomainEvent contract; the MediatR adapter should live in the application layer.
    public interface IDomainEvent:INotification
    {
        DateTime OccurredOn { get; }
    }
} 