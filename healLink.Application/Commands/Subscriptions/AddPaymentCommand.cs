using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Subscriptions.Responses;
using HealLink.Domain.Enums;
using MediatR;

namespace healLink.Application.Commands.Subscriptions
{
    public record AddPaymentCommand(
        Guid SubscriptionId,
        decimal Amount,
        Currency Currency,
        PaymentMethod PaymentMethod
    ) : IRequest<Result<PaymentResponse>>;

    public record CompletePaymentCommand(
        Guid SubscriptionId,
        Guid PaymentId,
        string TransactionId
    ) : IRequest<Result<bool>>;

    public record FailPaymentCommand(
        Guid SubscriptionId,
        Guid PaymentId,
        string FailureReason
    ) : IRequest<Result<bool>>;

    public record RefundPaymentCommand(
        Guid SubscriptionId,
        Guid PaymentId
    ) : IRequest<Result<bool>>;
}
