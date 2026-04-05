using System;

namespace HealLink.Domain.Base
{
    // TODO: [DDD] IDomainEvent directly extends INotification (MediatR) — this couples the domain layer to an application/infrastructure framework. The domain should define its own IDomainEvent contract; the MediatR adapter should live in the application layer.
    // TODO: [REFACTOR-P1] Remove `using MediatR;` and remove `: INotification` from IDomainEvent. Create a MediatR adapter in the Application layer: a marker interface `IDomainEventNotification : INotification` and a wrapper class `DomainEventNotification<T>(T DomainEvent) : IDomainEventNotification where T : IDomainEvent`. Update UnitOfWork to wrap each IDomainEvent in DomainEventNotification<T> before publishing via MediatR. Update all INotificationHandler<T> registrations in the Application layer to use the wrapper type.
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
} 