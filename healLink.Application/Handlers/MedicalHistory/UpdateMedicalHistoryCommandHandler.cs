using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.MedicalHistory;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.MedicalHistory.Responses;
using HealLink.Domain.ValueObjects;
using MediatR;

namespace healLink.Application.Handlers.MedicalHistory
{
    public class UpdateMedicalHistoryCommandHandler : IRequestHandler<UpdateMedicalHistoryCommand, Result<MedicalHistoryResponse>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMedicalHistoryCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MedicalHistoryResponse>> Handle(UpdateMedicalHistoryCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByPatientId(request.PatientId);
            if (patient == null)
                return Result<MedicalHistoryResponse>.Failure("Patient not found.");

            var details = new MedicalHistoryDetails(
                request.ChronicConditions,
                request.Allergies,
                request.CurrentMedications,
                request.PreviousSurgeries,
                request.FamilyHistory,
                request.Notes);

            patient.UpdateMedicalHistoryDetails(details, request.FileLink);

            await _patientRepository.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MedicalHistoryResponse>.Success(new MedicalHistoryResponse(
                patient.Id,
                details.ChronicConditions,
                details.Allergies,
                details.CurrentMedications,
                details.PreviousSurgeries,
                details.FamilyHistory,
                details.Notes,
                request.FileLink,
                patient.MedicalHistory.UpdatedAt));
        }
    }
}
