using System;
using HealLink.Domain.Enums;

namespace HealLink.Contracts.Subscriptions.Requests
{
    public record CreateSubscriptionRequest(
        Guid DoctorId,
        Guid PatientId,
        decimal Amount,
        Currency Currency,
        DateTime StartDate,
        DateTime EndDate,
        bool IsMonthly
    );

    public record AddPaymentRequest(
        decimal Amount,
        Currency Currency,
        PaymentMethod PaymentMethod
    );

    public record CompletePaymentRequest(string TransactionId);
    public record FailPaymentRequest(string FailureReason);
}
