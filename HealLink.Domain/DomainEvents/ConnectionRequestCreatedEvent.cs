using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Base;
using MediatR;

namespace HealLink.Domain.DomainEvents
{
    // TODO: [DDD] ConnectionRequestCreatedEvent does not implement IDomainEvent — it only implements INotification (MediatR), bypassing the domain event contract and losing OccurredOn timestamp.
    // TODO: [DDD] Domain events should not directly depend on MediatR (INotification) in the domain layer — MediatR is an application/infrastructure concern. Use IDomainEvent only; dispatch via MediatR in the application layer.
    public record ConnectionRequestCreatedEvent(Guid RequestId, Guid DoctorId, Guid PatientId) : IDomainEvent
    {
        public DateTime OccurredOn => throw new NotImplementedException();
    }
}
