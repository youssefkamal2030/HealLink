using System;
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
    }
}
