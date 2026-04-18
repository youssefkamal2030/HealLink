using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Subscriptions.Responses;
using MediatR;

namespace healLink.Application.Queries.Subscriptions
{
    public record GetSubscriptionsByPatientQuery(Guid PatientId) : IRequest<Result<SubscriptionsListResponse>>;
    public record GetSubscriptionsByDoctorQuery(Guid DoctorId) : IRequest<Result<SubscriptionsListResponse>>;
}
