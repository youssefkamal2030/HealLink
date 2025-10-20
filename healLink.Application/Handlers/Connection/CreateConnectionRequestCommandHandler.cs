using healLink.Application.Commands.Connections;
using healLink.Application.Common.Models;
using healLink.Application.Repositories;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class CreateConnectionRequestCommandHandler
        : IRequestHandler<CreateConnectionRequestCommand, Result<CreateConnectionRequestResponse>>
    {
        private readonly IConnectionRequestsRepository _connectionRequestsRepository;
        private readonly IMediator _mediator;
        public CreateConnectionRequestCommandHandler(IConnectionRequestsRepository connectionRequestsRepository
            ,IMediator mediator)
        {
            _connectionRequestsRepository = connectionRequestsRepository;
            _mediator = mediator;
        }

        public async Task<Result<CreateConnectionRequestResponse>> Handle(
         CreateConnectionRequestCommand request,
         CancellationToken cancellationToken)
        {
            try
            {
                if (await _connectionRequestsRepository.ExistAsync(request.DoctorId, request.PatientId))
                    return Result<CreateConnectionRequestResponse>.Failure("Connection request already exists.");

                var connectionRequest = new ConnectionRequest(request.DoctorId, request.PatientId);
                var result = await _connectionRequestsRepository.AddConnectionAsync(connectionRequest);
                await _mediator.Publish(new ConnectionRequestCreatedEvent(result.Id, result.DoctorId, result.PatientId), cancellationToken);

                var response = new CreateConnectionRequestResponse(result.Id, result.Status);
                return Result<CreateConnectionRequestResponse>.Success(response);
            }
            catch
            {
                return Result<CreateConnectionRequestResponse>.Failure("Failed to create connection request.");
            }
        }
    }
}
