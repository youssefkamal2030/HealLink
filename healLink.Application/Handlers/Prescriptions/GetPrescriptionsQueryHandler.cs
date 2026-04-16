using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries.Prescriptions;
using healLink.Application.Repositories;
using HealLink.Contracts.Prescriptions.Responses;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Prescriptions
{
    public class GetPrescriptionsByPatientQueryHandler : IRequestHandler<GetPrescriptionsByPatientQuery, Result<PrescriptionsListResponse>>
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public GetPrescriptionsByPatientQueryHandler(IPrescriptionRepository prescriptionRepository)
            => _prescriptionRepository = prescriptionRepository;

        public async Task<Result<PrescriptionsListResponse>> Handle(GetPrescriptionsByPatientQuery request, CancellationToken cancellationToken)
        {
            var prescriptions = await _prescriptionRepository.GetByPatientIdAsync(request.PatientId, cancellationToken);
            return Result<PrescriptionsListResponse>.Success(new PrescriptionsListResponse(prescriptions.Select(MapToResponse).ToList()));
        }

        private static PrescriptionResponse MapToResponse(Prescription p) => new(
            p.Id, p.PatientId, p.DoctorId, p.Notes, p.Status.ToString(), p.ExpiresAt, p.CreatedAt,
            p.Medications.Select(m => new MedicationDosageResponse(m.MedicationName, m.Dosage, m.Instructions, m.ScheduledTimes)).ToList()
        );
    }

    public class GetPrescriptionsByDoctorQueryHandler : IRequestHandler<GetPrescriptionsByDoctorQuery, Result<PrescriptionsListResponse>>
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public GetPrescriptionsByDoctorQueryHandler(IPrescriptionRepository prescriptionRepository)
            => _prescriptionRepository = prescriptionRepository;

        public async Task<Result<PrescriptionsListResponse>> Handle(GetPrescriptionsByDoctorQuery request, CancellationToken cancellationToken)
        {
            var prescriptions = await _prescriptionRepository.GetByDoctorIdAsync(request.DoctorId, cancellationToken);
            return Result<PrescriptionsListResponse>.Success(new PrescriptionsListResponse(prescriptions.Select(MapToResponse).ToList()));
        }

        private static PrescriptionResponse MapToResponse(Prescription p) => new(
            p.Id, p.PatientId, p.DoctorId, p.Notes, p.Status.ToString(), p.ExpiresAt, p.CreatedAt,
            p.Medications.Select(m => new MedicationDosageResponse(m.MedicationName, m.Dosage, m.Instructions, m.ScheduledTimes)).ToList()
        );
    }
}
