using System;
using System.Collections.Generic;

namespace HealLink.Contracts.Subscriptions.Responses
{
    public record SubscriptionResponse(
        Guid Id,
        Guid PatientId,
        Guid DoctorId,
        decimal Amount,
        string Currency,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive,
        bool IsMonthly
    );

    public record SubscriptionsListResponse(List<SubscriptionResponse> Subscriptions);

    public record PaymentResponse(
        Guid Id,
        Guid PatientId,
        Guid? SubscriptionId,
        decimal Amount,
        string Currency,
        string PaymentMethod,
        string Status,
        string TransactionId,
        DateTime? PaidAt
    );
}
