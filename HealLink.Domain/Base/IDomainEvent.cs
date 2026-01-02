using System;
using MediatR;

namespace HealLink.Domain.Base
{
    public interface IDomainEvent:INotification
    {
        DateTime OccurredOn { get; }
    }
} 