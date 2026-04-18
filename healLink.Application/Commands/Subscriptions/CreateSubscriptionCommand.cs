using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Subscriptions.Responses;
using HealLink.Domain.Enums;
using MediatR;

namespace healLink.Application.Commands.Subscriptions
{
    public record CreateSubscriptionCommand(
        Guid DoctorId,
        Guid PatientId,
        decimal Amount,
        Currency Currency,
        DateTime StartDate,
        DateTime EndDate,
        bool IsMonthly
    ) : IRequest<Result<SubscriptionResponse>>;
}
