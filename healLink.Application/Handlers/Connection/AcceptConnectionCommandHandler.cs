using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Connections;
using healLink.Application.Common.Models;
using healLink.Application.Repositories;
using HealLink.Contracts.Connections.Responses;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class AcceptConnectionCommandHandler : IRequestHandler<AcceptConnectionCommand, Result<ConnectionActionResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public AcceptConnectionCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<ConnectionActionResponse>> Handle(AcceptConnectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Load doctor aggregate
                var doctorAggregate = await _doctorRepository.GetAggregateByDoctorId(request.DoctorId);

                if (doctorAggregate == null)
                    return Result<ConnectionActionResponse>.Failure("Doctor not found");

                // Accept the connection (raises domain event)
                doctorAggregate.AcceptPatientRequest(request.ConnectionId);

                // Save aggregate (this will dispatch domain events)
                await _doctorRepository.UpdateAggregate(doctorAggregate);

                return Result<ConnectionActionResponse>.Success(
                    new ConnectionActionResponse("Connection accepted successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return Result<ConnectionActionResponse>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<ConnectionActionResponse>.Failure($"Failed to accept connection: {ex.Message}");
            }
        }
    }
}

