using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Aggregates
{
    public class SubscriptionAggregate : AggregateRoot
    {
        public Subscription Subscription { get; private set; }
        private readonly List<Payment> _payments = new();
        public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

        private SubscriptionAggregate() { }

        public SubscriptionAggregate(Subscription subscription, IEnumerable<Payment> payments = null)
        {
            Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
            if (payments != null) _payments.AddRange(payments);
        }

        public Payment AddPayment(PaymentDetails details)
        {
            if (!Subscription.IsActive)
                throw new InvalidOperationException("Cannot add payment to an inactive subscription.");

            var payment = new Payment(Subscription.PatientId, details, Subscription.Id);
            _payments.Add(payment);
            return payment;
        }

        public void CompletePayment(Guid paymentId, string transactionId)
        {
            var payment = FindPayment(paymentId);
            payment.MarkAsCompleted(transactionId);
            AddDomainEvent(new PaymentCompletedEvent(paymentId, Subscription.Id, transactionId));
        }

        public void FailPayment(Guid paymentId, string failureReason)
        {
            var payment = FindPayment(paymentId);
            payment.MarkAsFailed(failureReason);
            AddDomainEvent(new PaymentFailedEvent(paymentId, Subscription.Id, failureReason));
        }

        public void RefundPayment(Guid paymentId)
        {
            var payment = FindPayment(paymentId);
            payment.Refund(); // guard is on Payment.Refund()
        }

        public void Deactivate()
        {
            Subscription.Deactivate();
            AddDomainEvent(new SubscriptionDeactivatedEvent(Subscription.Id, Subscription.PatientId));
        }

        public void Renew(DateTime newEndDate)
        {
            Subscription.Renew(newEndDate);
            AddDomainEvent(new SubscriptionRenewedEvent(Subscription.Id, Subscription.PatientId, newEndDate));
        }

        private Payment FindPayment(Guid paymentId)
        {
            var payment = _payments.Find(p => p.Id == paymentId);
            if (payment == null) throw new InvalidOperationException($"Payment {paymentId} not found.");
            return payment;
        }
    }
}
