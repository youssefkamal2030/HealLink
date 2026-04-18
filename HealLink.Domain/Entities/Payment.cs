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
        public PaymentDetails Details { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string TransactionId { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public string FailureReason { get; private set; }

        private Payment() { } // For EF

        public Payment(Guid patientId, PaymentDetails details, Guid? subscriptionId = null)
        {
            PatientId = patientId;
            Details = details ?? throw new ArgumentNullException(nameof(details));
            Status = PaymentStatus.Pending;
            SubscriptionId = subscriptionId;
        }

        internal void MarkAsCompleted(string transactionId)
        {
            Status = PaymentStatus.Completed;
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            PaidAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        internal void MarkAsFailed(string failureReason)
        {
            Status = PaymentStatus.Failed;
            FailureReason = failureReason ?? throw new ArgumentNullException(nameof(failureReason));
            UpdateTimestamp();
        }

        internal void Refund()
        {
            if (Status == PaymentStatus.Failed)
                throw new InvalidOperationException("A failed payment cannot be refunded.");
            if (Status != PaymentStatus.Completed)
                throw new InvalidOperationException("Only completed payments can be refunded.");
            Status = PaymentStatus.Refunded;
            UpdateTimestamp();
        }
    }
}
