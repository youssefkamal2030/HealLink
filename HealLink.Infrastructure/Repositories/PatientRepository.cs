using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HealLinkDbContext _context;

        public PatientRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> GetByPatientId(Guid patientId)
            => await _context.Patients
                .Include(p => p.User)
                .Include(p => p.Guardian)
                .FirstOrDefaultAsync(p => p.Id == patientId);

        public async Task<Patient> GetByPatientIdWithRemindersAsync(Guid patientId, CancellationToken cancellationToken = default)
            => await _context.Patients
                .Include(p => p.MedicationReminders)
                .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);

        public async Task<Patient> GetByPatientIdWithTestResultsAsync(Guid patientId, CancellationToken cancellationToken = default)
            => await _context.Patients
                .Include(p => p.TestResults)
                .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);

        public async Task<List<Patient>> GetByPatientIdsAsync(IEnumerable<Guid> patientIds, CancellationToken cancellationToken = default)
        {
            var ids = patientIds.ToList();
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(cancellationToken);
        }

        public Task<string> GetPatientNameById(Guid patientId)
            => _context.Patients
                .Where(p => p.Id == patientId)
                .Select(p => p.User.Username)
                .FirstOrDefaultAsync();

        public Task UpdateAsync(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));
            _context.Patients.Update(patient);
            return Task.CompletedTask;
        }

        public async Task<MedicalHistory?> GetMedicalHistoryAsync(Guid patientId, CancellationToken cancellationToken = default)
            => await _context.MedicalHistories
                .FirstOrDefaultAsync(m => m.PatientId == patientId, cancellationToken);

        public async Task<(List<Patient> Patients, int TotalCount)> SearchPatientsAsync(
            string? searchTerm,
            string? city,
            string? country,
            bool? hasGuardian,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Patients
                .Include(p => p.User)
                .Include(p => p.Guardian)
                    .ThenInclude(g => g.User)
                .AsQueryable();

            // Apply search term filter (searches in user email/username)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(p =>
                    p.User.Email.ToLower().Contains(lowerSearchTerm) ||
                    p.User.Username.ToLower().Contains(lowerSearchTerm));
            }

            // [KNOWN-LIMITATION] TODO: City and country filters not implemented - Patient entity lacks address fields
            // TRACKED-IN: .kiro/steering/feature-status.md (Search Patients - city/country filters)
            // DECISION-NEEDED: Either add Address value object to Patient entity OR remove city/country parameters
            // CURRENT-BEHAVIOR: Parameters are accepted but ignored; results are not filtered by location

            // Apply guardian filter
            if (hasGuardian.HasValue)
            {
                query = hasGuardian.Value
                    ? query.Where(p => p.GuardianId != null)
                    : query.Where(p => p.GuardianId == null);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            var patients = await query
                .OrderBy(p => p.User.Email)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (patients, totalCount);
        }
    }
}
