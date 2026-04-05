using HealLink.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Common.Adapters
{
    // Adapter to wrap domain events in a MediatR notification for dispatching via IMediator.Publish. This decouples the domain event contract from the MediatR framework, allowing the domain layer to remain framework-agnostic.
    // Links the domain event to the MediatR notification system without coupling the domain layer to MediatR. The UnitOfWork can wrap each IDomainEvent in a DomainEventNotification before publishing, and application handlers can handle DomainEventNotification<TDomainEvent> instead of TDomainEvent directly.
    public class DomainEventNotification<TDomainEvent> : INotification
        where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; }
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
