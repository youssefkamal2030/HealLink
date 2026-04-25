using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Guardian;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Guardian.Responses;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Guardian
{
    public class AssignGuardianCommandHandler : IRequestHandler<AssignGuardianCommand, Result<GuardianResponse>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IGuardianRepository _guardianRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignGuardianCommandHandler(
            IPatientRepository patientRepository,
            IGuardianRepository guardianRepository,
            IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GuardianResponse>> Handle(AssignGuardianCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByPatientId(request.PatientId);
            if (patient == null)
                return Result<GuardianResponse>.Failure("Patient not found.");

            // Find existing guardian for this user or create a new one
            var guardian = await _guardianRepository.GetByUserIdAsync(request.GuardianUserId, cancellationToken);
            if (guardian == null)
            {
                guardian = new HealLink.Domain.Entities.Guardian(request.GuardianUserId, request.RelationshipToPatient);
                await _guardianRepository.AddAsync(guardian, cancellationToken);
            }
            else
            {
                guardian.UpdateRelationship(request.RelationshipToPatient);
                await _guardianRepository.UpdateAsync(guardian, cancellationToken);
            }

            guardian.AddPatient(patient.Id);
            patient.AssignGuardian(guardian.Id);

            await _patientRepository.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GuardianResponse>.Success(new GuardianResponse(
                guardian.Id,
                guardian.UserId,
                guardian.RelationshipToPatient,
                guardian.CreatedAt));
        }
    }
}
