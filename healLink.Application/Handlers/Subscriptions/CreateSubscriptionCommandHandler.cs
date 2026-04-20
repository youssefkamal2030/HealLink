using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Subscriptions;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Subscriptions.Responses;
using HealLink.Domain.Aggregates;
using HealLink.Domain.Entities;
using HealLink.Domain.ValueObjects;using MediatR;

namespace healLink.Application.Handlers.Subscriptions
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Result<SubscriptionResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IDoctorPatientConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubscriptionCommandHandler(
            ISubscriptionRepository subscriptionRepository,
            IDoctorPatientConnectionRepository connectionRepository,
            IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _connectionRepository = connectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SubscriptionResponse>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var connected = await _connectionRepository.AcceptedConnectionExistsAsync(request.DoctorId, request.PatientId);
            if (!connected)
                return Result<SubscriptionResponse>.Failure("Doctor and patient are not connected.");

            var aggregate = SubscriptionAggregate.Create(
                request.PatientId,
                request.DoctorId,
                new Money(request.Amount, request.Currency),
                request.StartDate,
                request.EndDate,
                request.IsMonthly);

            await _subscriptionRepository.AddAsync(aggregate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<SubscriptionResponse>.Success(MapToResponse(aggregate.Subscription));
        }

        private static SubscriptionResponse MapToResponse(Subscription s) => new(
            s.Id, s.PatientId, s.DoctorId,
            s.Amount.Amount, s.Amount.Currency.ToString(),
            s.StartDate, s.EndDate, s.IsActive, s.IsMonthly);
    }
}
