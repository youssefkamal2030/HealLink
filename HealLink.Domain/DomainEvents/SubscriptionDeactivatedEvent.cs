using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class SubscriptionDeactivatedEvent : IDomainEvent
    {
        public Guid SubscriptionId { get; }
        public Guid PatientId { get; }
        public DateTime OccurredOn { get; }

        public SubscriptionDeactivatedEvent(Guid subscriptionId, Guid patientId)
        {
            SubscriptionId = subscriptionId;
            PatientId = patientId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
