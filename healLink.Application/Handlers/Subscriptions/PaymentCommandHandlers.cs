using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Subscriptions;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Subscriptions.Responses;
using HealLink.Domain.ValueObjects;
using MediatR;

namespace healLink.Application.Handlers.Subscriptions
{
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Result<PaymentResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPaymentCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaymentResponse>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);
            if (aggregate == null)
                return Result<PaymentResponse>.Failure("Subscription not found.");

            try
            {
                var details = new PaymentDetails(request.Amount, request.Currency, request.PaymentMethod);
                var payment = aggregate.AddPayment(details);
                await _subscriptionRepository.UpdateAsync(aggregate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<PaymentResponse>.Success(new PaymentResponse(
                    payment.Id, payment.PatientId, payment.SubscriptionId,
                    payment.Details.Amount, payment.Details.Currency.ToString(),
                    payment.Details.PaymentMethod.ToString(), payment.Status.ToString(),
                    payment.TransactionId, payment.PaidAt));
            }
            catch (Exception ex)
            {
                return Result<PaymentResponse>.Failure(ex.Message);
            }
        }
    }

    public class CompletePaymentCommandHandler : IRequestHandler<CompletePaymentCommand, Result<bool>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompletePaymentCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(CompletePaymentCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);
            if (aggregate == null) return Result<bool>.Failure("Subscription not found.");

            try
            {
                aggregate.CompletePayment(request.PaymentId, request.TransactionId);
                await _subscriptionRepository.UpdateAsync(aggregate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<bool>.Success(true);
            }
            catch (Exception ex) { return Result<bool>.Failure(ex.Message); }
        }
    }

    public class FailPaymentCommandHandler : IRequestHandler<FailPaymentCommand, Result<bool>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FailPaymentCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(FailPaymentCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);
            if (aggregate == null) return Result<bool>.Failure("Subscription not found.");

            try
            {
                aggregate.FailPayment(request.PaymentId, request.FailureReason);
                await _subscriptionRepository.UpdateAsync(aggregate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<bool>.Success(true);
            }
            catch (Exception ex) { return Result<bool>.Failure(ex.Message); }
        }
    }

    public class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand, Result<bool>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RefundPaymentCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);
            if (aggregate == null) return Result<bool>.Failure("Subscription not found.");

            try
            {
                aggregate.RefundPayment(request.PaymentId);
                await _subscriptionRepository.UpdateAsync(aggregate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<bool>.Success(true);
            }
            catch (Exception ex) { return Result<bool>.Failure(ex.Message); }
        }
    }
}
