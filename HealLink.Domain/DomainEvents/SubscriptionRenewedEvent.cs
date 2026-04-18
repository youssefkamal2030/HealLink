using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class SubscriptionRenewedEvent : IDomainEvent
    {
        public Guid SubscriptionId { get; }
        public Guid PatientId { get; }
        public DateTime NewEndDate { get; }
        public DateTime OccurredOn { get; }

        public SubscriptionRenewedEvent(Guid subscriptionId, Guid patientId, DateTime newEndDate)
        {
            SubscriptionId = subscriptionId;
            PatientId = patientId;
            NewEndDate = newEndDate;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
