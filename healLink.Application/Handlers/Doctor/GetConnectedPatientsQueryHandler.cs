using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Doctor.Responses;
using HealLink.Contracts.Profile.Responses;
using HealLink.Domain.Enums;
using MediatR;

namespace HealLink.Application.Handlers.Doctor
{
    public class GetConnectedPatientsQueryHandler : IRequestHandler<GetConnectedPatientsQuery, Result<ConnectedPatientsResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public GetConnectedPatientsQueryHandler(IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<Result<ConnectedPatientsResponse>> Handle(GetConnectedPatientsQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByDoctorId(request.DoctorId);
            if (doctor == null)
                return Result<ConnectedPatientsResponse>.Failure("Doctor not found.");

            var connectedPatientIds = doctor.PatientConnections
                .Where(c => c.Status == ConnectionStatus.Accepted)
                .Select(c => c.PatientId)
                .ToList();

            // Single query instead of one per patient
            var patients = await _patientRepository.GetByPatientIdsAsync(connectedPatientIds, cancellationToken);

            var connectedPatients = patients
                .Where(p => p.User != null)
                .Select(p => new PatientProfileResponse(
                    Id: p.Id,
                    UserId: p.UserId,
                    FullName: p.User.Username,
                    Email: p.User.Email,
                    GuardianId: p.GuardianId))
                .ToList();

            return Result<ConnectedPatientsResponse>.Success(new ConnectedPatientsResponse(
                Success: true,
                Message: "Connected patients retrieved successfully.",
                ConnectedPatients: connectedPatients,
                TotalCount: connectedPatients.Count));
        }
    }
}
