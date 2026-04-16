using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries.Prescriptions;
using healLink.Application.Repositories;
using HealLink.Contracts.Reminders.Responses;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Prescriptions
{
    public class GetMedicationRemindersQueryHandler : IRequestHandler<GetMedicationRemindersQuery, Result<MedicationRemindersListResponse>>
    {
        private readonly IMedicationReminderRepository _reminderRepository;

        public GetMedicationRemindersQueryHandler(IMedicationReminderRepository reminderRepository)
            => _reminderRepository = reminderRepository;

        public async Task<Result<MedicationRemindersListResponse>> Handle(GetMedicationRemindersQuery request, CancellationToken cancellationToken)
        {
            var reminders = await _reminderRepository.GetByPatientIdAsync(request.PatientId, request.Date, request.Status, cancellationToken);
            return Result<MedicationRemindersListResponse>.Success(
                new MedicationRemindersListResponse(reminders.Select(MapToResponse).ToList()));
        }

        private static MedicationReminderResponse MapToResponse(MedicationReminder r) => new(
            r.Id, r.PatientId, r.PrescriptionId, r.MedicationName, r.ScheduledTime, r.Status.ToString(), r.TakenAt
        );
    }
}
