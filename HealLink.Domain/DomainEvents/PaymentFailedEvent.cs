using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class PaymentFailedEvent : IDomainEvent
    {
        public Guid PaymentId { get; }
        public Guid SubscriptionId { get; }
        public string FailureReason { get; }
        public DateTime OccurredOn { get; }

        public PaymentFailedEvent(Guid paymentId, Guid subscriptionId, string failureReason)
        {
            PaymentId = paymentId;
            SubscriptionId = subscriptionId;
            FailureReason = failureReason;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
