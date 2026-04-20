using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries.MedicalHistory;
using healLink.Application.Repositories;
using HealLink.Contracts.MedicalHistory.Responses;
using MediatR;

namespace healLink.Application.Handlers.MedicalHistory
{
    public class GetMedicalHistoryQueryHandler : IRequestHandler<GetMedicalHistoryQuery, Result<MedicalHistoryResponse>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetMedicalHistoryQueryHandler(IPatientRepository patientRepository)
            => _patientRepository = patientRepository;

        public async Task<Result<MedicalHistoryResponse>> Handle(GetMedicalHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await _patientRepository.GetMedicalHistoryAsync(request.PatientId, cancellationToken);

            if (history == null)
                return Result<MedicalHistoryResponse>.Failure("Medical history not found.");

            return Result<MedicalHistoryResponse>.Success(new MedicalHistoryResponse(
                history.PatientId,
                history.Details.ChronicConditions,
                history.Details.Allergies,
                history.Details.CurrentMedications,
                history.Details.PreviousSurgeries,
                history.Details.FamilyHistory,
                history.Details.Notes,
                history.FileLink,
                history.UpdatedAt));
        }
    }
}
