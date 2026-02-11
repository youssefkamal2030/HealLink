using healLink.Application.Commands.Connections;
using healLink.Application.Common.Models;
using healLink.Application.Repositories;
using HealLink.Contracts.Connections.Responses;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class CreateConnectionRequestCommandHandler
        : IRequestHandler<CreateConnectionRequestCommand, Result<CreateConnectionRequestResponse>>
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IMediator _mediator;

        public CreateConnectionRequestCommandHandler(
            IConnectionRepository connectionRepository,
            IMediator mediator)
        {
            _connectionRepository = connectionRepository;
            _mediator = mediator;
        }

        public async Task<Result<CreateConnectionRequestResponse>> Handle(
         CreateConnectionRequestCommand request,
         CancellationToken cancellationToken)
        {
            try
            {
                // Check if connection already exists
                if (await _connectionRepository.ConnectionExistsAsync(request.DoctorId, request.PatientId))
                    return Result<CreateConnectionRequestResponse>.Failure("Connection request already exists.");

                // Create DoctorPatientConnection
                var connection = new DoctorPatientConnection(request.DoctorId, request.PatientId);
                var result = await _connectionRepository.AddConnectionAsync(connection);

                // Publish domain event
                await _mediator.Publish(
                    new ConnectionRequestCreatedEvent(result.Id, result.DoctorId, result.PatientId), 
                    cancellationToken);

                var response = new CreateConnectionRequestResponse(result.Id, result.Status.ToString());
                return Result<CreateConnectionRequestResponse>.Success(response);
            }
            catch
            {
                return Result<CreateConnectionRequestResponse>.Failure("Failed to create connection request.");
            }
        }
    }
}
