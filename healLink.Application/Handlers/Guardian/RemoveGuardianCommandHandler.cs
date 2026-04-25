using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Guardian;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Guardian
{
    public class RemoveGuardianCommandHandler : IRequestHandler<RemoveGuardianCommand, Result<bool>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IGuardianRepository _guardianRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveGuardianCommandHandler(
            IPatientRepository patientRepository,
            IGuardianRepository guardianRepository,
            IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveGuardianCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByPatientId(request.PatientId);
            if (patient == null)
                return Result<bool>.Failure("Patient not found.");

            if (patient.GuardianId == null)
                return Result<bool>.Failure("Patient has no assigned guardian.");

            var guardian = await _guardianRepository.GetByIdAsync(patient.GuardianId.Value, cancellationToken);
            if (guardian != null)
            {
                guardian.RemovePatient(patient.Id);
                await _guardianRepository.UpdateAsync(guardian, cancellationToken);
            }

            patient.RemoveGuardian();
            await _patientRepository.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
