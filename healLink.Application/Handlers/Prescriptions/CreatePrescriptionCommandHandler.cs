using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Prescriptions;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Prescriptions.Responses;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Prescriptions
{
    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, Result<PrescriptionResponse>>
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IDoctorPatientConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePrescriptionCommandHandler(
            IPrescriptionRepository prescriptionRepository,
            IDoctorPatientConnectionRepository connectionRepository,
            IUnitOfWork unitOfWork)
        {
            _prescriptionRepository = prescriptionRepository;
            _connectionRepository = connectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PrescriptionResponse>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var connected = await _connectionRepository.AcceptedConnectionExistsAsync(request.DoctorId, request.PatientId);
            if (!connected)
                return Result<PrescriptionResponse>.Failure("Doctor and patient are not connected.");

            var prescription = Prescription.Issue(
                request.PatientId,
                request.DoctorId,
                request.Notes,
                request.Medications,
                request.ExpiresAt);

            await _prescriptionRepository.AddAsync(prescription, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<PrescriptionResponse>.Success(MapToResponse(prescription));
        }

        private static PrescriptionResponse MapToResponse(Prescription p) => new(
            p.Id,
            p.PatientId,
            p.DoctorId,
            p.Notes,
            p.Status.ToString(),
            p.ExpiresAt,
            p.CreatedAt,
            p.Medications.Select(m => new MedicationDosageResponse(
                m.MedicationName, m.Dosage, m.Instructions, m.ScheduledTimes)).ToList()
        );
    }
}
