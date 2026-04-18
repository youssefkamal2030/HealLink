using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries.Subscriptions;
using healLink.Application.Repositories;
using HealLink.Contracts.Subscriptions.Responses;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Subscriptions
{
    public class GetSubscriptionsByPatientQueryHandler : IRequestHandler<GetSubscriptionsByPatientQuery, Result<SubscriptionsListResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetSubscriptionsByPatientQueryHandler(ISubscriptionRepository subscriptionRepository)
            => _subscriptionRepository = subscriptionRepository;

        public async Task<Result<SubscriptionsListResponse>> Handle(GetSubscriptionsByPatientQuery request, CancellationToken cancellationToken)
        {
            var subs = await _subscriptionRepository.GetByPatientIdAsync(request.PatientId, cancellationToken);
            return Result<SubscriptionsListResponse>.Success(new SubscriptionsListResponse(subs.Select(MapToResponse).ToList()));
        }

        private static SubscriptionResponse MapToResponse(Subscription s) => new(
            s.Id, s.PatientId, s.DoctorId,
            s.Amount.Amount, s.Amount.Currency.ToString(),
            s.StartDate, s.EndDate, s.IsActive, s.IsMonthly);
    }

    public class GetSubscriptionsByDoctorQueryHandler : IRequestHandler<GetSubscriptionsByDoctorQuery, Result<SubscriptionsListResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetSubscriptionsByDoctorQueryHandler(ISubscriptionRepository subscriptionRepository)
            => _subscriptionRepository = subscriptionRepository;

        public async Task<Result<SubscriptionsListResponse>> Handle(GetSubscriptionsByDoctorQuery request, CancellationToken cancellationToken)
        {
            var subs = await _subscriptionRepository.GetByDoctorIdAsync(request.DoctorId, cancellationToken);
            return Result<SubscriptionsListResponse>.Success(new SubscriptionsListResponse(subs.Select(MapToResponse).ToList()));
        }

        private static SubscriptionResponse MapToResponse(Subscription s) => new(
            s.Id, s.PatientId, s.DoctorId,
            s.Amount.Amount, s.Amount.Currency.ToString(),
            s.StartDate, s.EndDate, s.IsActive, s.IsMonthly);
    }
}
