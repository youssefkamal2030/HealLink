using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Connections;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Connections.Responses;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class RejectConnectionCommandHandler : IRequestHandler<RejectConnectionCommand, Result<ConnectionActionResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectConnectionCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ConnectionActionResponse>> Handle(RejectConnectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _doctorRepository.GetByDoctorId(request.DoctorId);
                if (doctor == null)
                    return Result<ConnectionActionResponse>.Failure("Doctor not found");

                doctor.RejectPatientRequest(request.ConnectionId);
                await _doctorRepository.UpdateAsync(doctor);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<ConnectionActionResponse>.Success(new ConnectionActionResponse("Connection rejected successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return Result<ConnectionActionResponse>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<ConnectionActionResponse>.Failure($"Failed to reject connection: {ex.Message}");
            }
        }
    }
}

