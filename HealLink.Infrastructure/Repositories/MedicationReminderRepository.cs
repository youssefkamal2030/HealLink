using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class MedicationReminderRepository : IMedicationReminderRepository
    {
        private readonly HealLinkDbContext _context;

        public MedicationReminderRepository(HealLinkDbContext context) => _context = context;

        public async Task<List<MedicationReminder>> GetByPatientIdAsync(
            Guid patientId,
            DateTime? date = null,
            MedicationReminderStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.MedicationReminders.Where(r => r.PatientId == patientId);

            if (date.HasValue)
                query = query.Where(r => r.ScheduledTime.Date == date.Value.Date);

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            return await query.OrderBy(r => r.ScheduledTime).ToListAsync(cancellationToken);
        }

        public async Task<MedicationReminder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.MedicationReminders.FindAsync(new object[] { id }, cancellationToken);
    }
}
