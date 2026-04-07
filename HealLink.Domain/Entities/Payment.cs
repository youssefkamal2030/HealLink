using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    // DONE: Payment.Amount is now Money value object.
    // TODO: [DDD] No domain event raised on MarkAsCompleted() or MarkAsFailed() — payment state changes are significant domain events.
    // TODO: [DDD] PaymentDetails value object already exists (HealLink.Domain/ValueObjects/PaymentDetails.cs) but is not used here — Amount, PaymentMethod should be encapsulated in it.
    // TODO: [DOMAIN-NEXT] Replace `Money Amount` and `PaymentMethod PaymentMethod` with a single `PaymentDetails Details` property using the existing PaymentDetails value object. Update the constructor. Update SubscriptionAggregate.AddPayment() and any callers. Update DbContext OwnsOne accordingly.
    // TODO: [DOMAIN-NEXT] Raise PaymentCompletedEvent in MarkAsCompleted() and PaymentFailedEvent in MarkAsFailed(). Payment extends Entity not AggregateRoot — events must be raised by the owning SubscriptionAggregate via CompletePayment()/FailPayment() methods.
    // TODO: [DOMAIN-NEXT] Add a guard to Refund(): throw InvalidOperationException if Status == PaymentStatus.Failed — per BR-SUB-06, a failed payment cannot be refunded.
    public class Payment : Entity
    {
        public Guid PatientId { get; private set; }
        public Guid? SubscriptionId { get; private set; }
        public Money Amount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string TransactionId { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public string FailureReason { get; private set; }

        private Payment() { } // For EF

        public Payment(Guid patientId, Money amount, PaymentMethod paymentMethod, Guid? subscriptionId = null)
        {
            PatientId = patientId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = PaymentStatus.Pending;
            SubscriptionId = subscriptionId;
        }

        public void MarkAsCompleted(string transactionId)
        {
            Status = PaymentStatus.Completed;
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            PaidAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public void MarkAsFailed(string failureReason)
        {
            Status = PaymentStatus.Failed;
            FailureReason = failureReason ?? throw new ArgumentNullException(nameof(failureReason));
            UpdateTimestamp();
        }

        public void Refund()
        {
            Status = PaymentStatus.Refunded;
            UpdateTimestamp();
        }
    }
}
