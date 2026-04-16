using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Prescriptions;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Prescriptions
{
    public class MarkReminderAsTakenCommandHandler : IRequestHandler<MarkReminderAsTakenCommand, Result<bool>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkReminderAsTakenCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(MarkReminderAsTakenCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByPatientIdWithRemindersAsync(request.PatientId, cancellationToken);
            if (patient == null)
                return Result<bool>.Failure("Patient not found.");

            try
            {
                patient.ConfirmMedicationReminder(request.ReminderId, request.ActingUserId);
            }
            catch (System.Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

            await _patientRepository.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
