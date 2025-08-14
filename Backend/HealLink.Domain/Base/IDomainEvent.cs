using System;

namespace HealLink.Domain.Base
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
} 