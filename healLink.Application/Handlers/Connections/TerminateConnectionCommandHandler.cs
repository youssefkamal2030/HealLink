using healLink.Application.Commands.Connections;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Enums;
using MediatR;

namespace healLink.Application.Handlers.Connections
{
    // TODO: [REFACTOR-AUTH] Remove inline authorization check after centralized-authorization-infrastructure is implemented
    // PROBLEM: Handler performs inline authorization check (lines 48-53: checks if user is doctor or patient in connection)
    // FIX: Remove authorization logic from handler
    // APPROACH: Authorization will be handled by AuthorizationBehavior with custom ConnectionParticipant policy
    // REASON: Separation of concerns - handler should only contain business logic
    // MIGRATION STEPS:
    //   1. Create ConnectionParticipantPolicy that checks if user is doctor or patient in the connection
    //   2. Add [Authorize(AuthorizationPolicies.ConnectionParticipant)] to TerminateConnectionCommand
    //   3. Remove lines 48-53 (authorization check)
    //   4. Remove RequestingUserId from command
    //   5. Remove doctor/patient entity lookups for authorization (lines 41-47)
    //   6. Handler will focus only on business logic (terminate connection)
    public class TerminateConnectionCommandHandler : IRequestHandler<TerminateConnectionCommand, Result<bool>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorPatientConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TerminateConnectionCommandHandler(
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IDoctorPatientConnectionRepository connectionRepository,
            IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _connectionRepository = connectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(TerminateConnectionCommand request, CancellationToken cancellationToken)
        {
            // Get the connection
            var connection = await _connectionRepository.GetConnectionByIdAsync(request.ConnectionId);
            if (connection == null)
                return Result<bool>.Failure("Connection not found.");

            // Verify connection is in Accepted status
            if (connection.Status != ConnectionStatus.Accepted)
                return Result<bool>.Failure("Only accepted connections can be terminated.");

            // Get doctor and patient entities
            var doctor = await _doctorRepository.GetByDoctorId(connection.DoctorId);
            var patient = await _patientRepository.GetByPatientId(connection.PatientId);

            if (doctor == null)
                return Result<bool>.Failure("Doctor not found.");
            if (patient == null)
                return Result<bool>.Failure("Patient not found.");

            // Authorization: Verify the requesting user is either the doctor or patient in this connection
            bool isAuthorized = doctor.UserId == request.RequestingUserId || patient.UserId == request.RequestingUserId;

            if (!isAuthorized)
                return Result<bool>.Failure("You are not authorized to terminate this connection.");

            try
            {
                // Terminate the connection using reflection to call internal method
                var terminateMethod = connection.GetType().GetMethod("Terminate", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                terminateMethod?.Invoke(connection, null);

                await _connectionRepository.UpdateAsync(connection);

                // Remove the connection from patient aggregate
                patient.RemoveConnectedDoctor(connection.DoctorId);
                await _patientRepository.UpdateAsync(patient);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success(true);
            }
            catch (InvalidOperationException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to terminate connection: {ex.Message}");
            }
        }
    }
}
