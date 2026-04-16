using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;

namespace healLink.Application.Repositories
{
    public interface IMedicationReminderRepository
    {
        Task<List<MedicationReminder>> GetByPatientIdAsync(Guid patientId, DateTime? date = null, MedicationReminderStatus? status = null, CancellationToken cancellationToken = default);
        Task<MedicationReminder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
