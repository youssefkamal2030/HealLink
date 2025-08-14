using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;

namespace HealLink.Domain.Aggregates
{
    public class SubscriptionAggregate
    {
        public Subscription Subscription { get; private set; }
        private readonly List<Payment> _payments = new();

        public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

        public SubscriptionAggregate(Subscription subscription, IEnumerable<Payment> payments)
        {
            Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
            if (payments != null) _payments.AddRange(payments);
        }

        public void AddPayment(Payment payment)
        {
            if (!Subscription.IsActive)
                throw new InvalidOperationException("Cannot add payment to inactive subscription.");
            _payments.Add(payment);
        }
    }
} 