using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries.Guardian;
using healLink.Application.Repositories;
using HealLink.Contracts.Guardian.Responses;
using MediatR;

namespace healLink.Application.Handlers.Guardian
{
    public class GetGuardianQueryHandler : IRequestHandler<GetGuardianQuery, Result<GuardianResponse>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IGuardianRepository _guardianRepository;

        public GetGuardianQueryHandler(IPatientRepository patientRepository, IGuardianRepository guardianRepository)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
        }

        public async Task<Result<GuardianResponse>> Handle(GetGuardianQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByPatientId(request.PatientId);
            if (patient == null)
                return Result<GuardianResponse>.Failure("Patient not found.");

            if (patient.GuardianId == null)
                return Result<GuardianResponse>.Failure("Patient has no assigned guardian.");

            var guardian = await _guardianRepository.GetByIdAsync(patient.GuardianId.Value, cancellationToken);
            if (guardian == null)
                return Result<GuardianResponse>.Failure("Guardian not found.");

            return Result<GuardianResponse>.Success(new GuardianResponse(
                guardian.Id,
                guardian.UserId,
                guardian.RelationshipToPatient,
                guardian.CreatedAt));
        }
    }
}
