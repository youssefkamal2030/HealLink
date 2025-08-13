using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly HealLinkDbContext _context;
        public ProfileRepository(HealLinkDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Patient?> GetPatientByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
        }

        public async Task<Doctor?> GetDoctorByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);
        }

        public async Task AddPatientAsync(Patient patient, CancellationToken cancellationToken = default)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
