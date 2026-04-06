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
    public class AcceptConnectionCommandHandler : IRequestHandler<AcceptConnectionCommand, Result<ConnectionActionResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AcceptConnectionCommandHandler(
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ConnectionActionResponse>> Handle(AcceptConnectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _doctorRepository.GetByDoctorId(request.DoctorId);
                if (doctor == null)
                    return Result<ConnectionActionResponse>.Failure("Doctor not found");

                // Resolve PatientId from the connection before mutating the doctor aggregate
                var connection = doctor.PatientConnections.FirstOrDefault(c => c.Id == request.ConnectionId);
                if (connection == null)
                    return Result<ConnectionActionResponse>.Failure("Connection not found");

                // Mutate Doctor aggregate — raises ConnectionAcceptedEvent
                doctor.AcceptPatientRequest(request.ConnectionId);
                await _doctorRepository.UpdateAsync(doctor);

                // Mutate Patient aggregate in the same transaction so both are committed atomically
                // before ConnectionAcceptedEvent is dispatched. The event handler then only sends
                // the notification — no DB writes, no nested SaveChangesAsync.
                var patient = await _patientRepository.GetByPatientId(connection.PatientId);
                if (patient != null)
                {
                    patient.AddConnectedDoctor(request.DoctorId);
                    await _patientRepository.UpdateAsync(patient);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<ConnectionActionResponse>.Success(new ConnectionActionResponse("Connection accepted successfully"));
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

