using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] Payment.Amount is typed as int — should use the Money value object or at minimum decimal to represent monetary values accurately.
    // TODO: [DDD] No domain event raised on MarkAsCompleted() or MarkAsFailed() — payment state changes are significant domain events.
    // TODO: [DDD] PaymentDetails value object already exists (HealLink.Domain/ValueObjects/PaymentDetails.cs) but is not used here — Amount, PaymentMethod should be encapsulated in it.
    // TODO: [DOMAIN-NEXT] Replace `int Amount` and `PaymentMethod PaymentMethod` with a single `PaymentDetails Details` property using the existing PaymentDetails value object. Update the constructor from (int amount, PaymentMethod paymentMethod) to (PaymentDetails details). Update SubscriptionAggregate.AddPayment() and any callers.
    // TODO: [DOMAIN-NEXT] Raise PaymentCompletedEvent in MarkAsCompleted() and PaymentFailedEvent in MarkAsFailed(). Create both event classes in HealLink.Domain/DomainEvents/. Note: Payment extends Entity, not AggregateRoot — it cannot raise events itself. The owning SubscriptionAggregate must expose CompletePayment(Guid paymentId, string transactionId) and FailPayment(Guid paymentId, string reason) methods that call the entity methods and raise the events.
    // TODO: [DOMAIN-NEXT] Add a guard to Refund(): throw InvalidOperationException if Status == PaymentStatus.Failed — per BR-SUB-06, a failed payment cannot be refunded. This invariant must be enforced here.
    public class Payment : Entity
    {
        public Guid PatientId { get; private set; }
        public Guid? SubscriptionId { get; private set; }
        public int Amount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string TransactionId { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public string FailureReason { get; private set; }

        private Payment() { } // For EF

        public Payment(Guid patientId, int amount, PaymentMethod paymentMethod, Guid? subscriptionId = null)
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
