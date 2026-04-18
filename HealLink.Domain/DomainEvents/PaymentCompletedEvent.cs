using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class PaymentCompletedEvent : IDomainEvent
    {
        public Guid PaymentId { get; }
        public Guid SubscriptionId { get; }
        public string TransactionId { get; }
        public DateTime OccurredOn { get; }

        public PaymentCompletedEvent(Guid paymentId, Guid subscriptionId, string transactionId)
        {
            PaymentId = paymentId;
            SubscriptionId = subscriptionId;
            TransactionId = transactionId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
