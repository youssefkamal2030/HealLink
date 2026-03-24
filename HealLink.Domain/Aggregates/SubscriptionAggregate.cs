using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;

namespace HealLink.Domain.Aggregates
{
    // TODO: [AGGREGATE] SubscriptionAggregate uses the wrapper pattern — Subscription entity should be merged into this class and extend AggregateRoot directly.
    // TODO: [AGGREGATE] Subscription is exposed as a public property — callers can bypass the aggregate and call Deactivate()/Renew() directly on the entity without going through the aggregate boundary.
    // TODO: [AGGREGATE] Payment state transitions (Complete, Fail, Refund) are managed directly on the Payment entity from outside the aggregate — the aggregate must expose these as methods (e.g., CompletePayment(), FailPayment(), RefundPayment()) to enforce invariants and raise domain events.
    // TODO: [AGGREGATE] Payment.Refund() has no guard — per BR-SUB-06, a failed payment cannot be refunded. That invariant must be enforced here in the aggregate's RefundPayment() method, not left to callers.
    // TODO: [AGGREGATE-MISSING] Domain events are entirely absent — SubscriptionRenewed, SubscriptionDeactivated, PaymentCompleted, PaymentFailed are all significant state transitions per the business rules and must be raised from this aggregate.
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