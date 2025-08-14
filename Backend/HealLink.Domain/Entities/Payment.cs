using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
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
