using healLink.Application.Commands.Connections;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Connections.Responses;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class CreateConnectionRequestCommandHandler
        : IRequestHandler<CreateConnectionRequestCommand, Result<CreateConnectionRequestResponse>>
    {
        private readonly IDoctorPatientDoctorPatientConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateConnectionRequestCommandHandler(
            IDoctorPatientDoctorPatientConnectionRepository connectionRepository,
            IUnitOfWork unitOfWork)
        {
            _connectionRepository = connectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateConnectionRequestResponse>> Handle(
            CreateConnectionRequestCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                if (await _connectionRepository.ConnectionExistsAsync(request.DoctorId, request.PatientId))
                    return Result<CreateConnectionRequestResponse>.Failure("Connection request already exists.");

                var connection = new DoctorPatientConnection(request.DoctorId, request.PatientId);
                var result = await _connectionRepository.AddAsync(connection);

                // UnitOfWork saves and dispatches ConnectionRequestCreatedEvent raised by the aggregate
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<CreateConnectionRequestResponse>.Success(
                    new CreateConnectionRequestResponse(result.Id, result.Status.ToString()));
            }
            catch
            {
                return Result<CreateConnectionRequestResponse>.Failure("Failed to create connection request.");
            }
        }
    }
}
