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
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectConnectionCommandHandler(
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ConnectionActionResponse>> Handle(RejectConnectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _doctorRepository.GetByDoctorId(request.DoctorId);
                if (doctor == null)
                    return Result<ConnectionActionResponse>.Failure("Doctor not found");

                // Resolve PatientId from the connection before the doctor aggregate removes it
                var connection = doctor.PatientConnections.FirstOrDefault(c => c.Id == request.ConnectionId);
                if (connection == null)
                    return Result<ConnectionActionResponse>.Failure("Connection not found");

                var patientId = connection.PatientId;

                // Mutate Doctor aggregate — raises ConnectionRejectedEvent
                doctor.RejectPatientRequest(request.ConnectionId);
                await _doctorRepository.UpdateAsync(doctor);

                // Mutate Patient aggregate in the same transaction.
                // Only remove if the doctor was previously connected — a pending rejection
                // means the doctor was never added to ConnectedDoctorIds.
                var patient = await _patientRepository.GetByPatientId(patientId);
                if (patient != null && patient.ConnectedDoctorIds.Contains(request.DoctorId))
                {
                    patient.RemoveConnectedDoctor(request.DoctorId);
                    await _patientRepository.UpdateAsync(patient);
                }

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

