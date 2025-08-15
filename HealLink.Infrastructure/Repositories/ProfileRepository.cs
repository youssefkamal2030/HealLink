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

        public async Task<Patient?> GetPatientByUserIdAsync(Guid UserId, CancellationToken cancellationToken = default)
        {
            return await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == UserId, cancellationToken);
        }

        public async Task<Doctor?> GetDoctorByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);
        }

        public async Task<string?> GetGuardianNameByIdAsync(Guid guardianId, CancellationToken cancellationToken = default)
        {
            var guardian = await _context.Guardians
                .Include(g => g.User)
                .FirstOrDefaultAsync(g => g.Id == guardianId, cancellationToken);
            
            return guardian?.User != null 
                ? $"{guardian.User.FirstName} {guardian.User.LastName}"
                : null;
        }

        public async Task<List<Doctor>> GetAllDoctorsWithUsersAsync(int skip, int take, string? searchTerm, CancellationToken cancellationToken = default)
        {
            var query = _context.Doctors
                .Include(d => d.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d => 
                    d.User.FirstName.Contains(searchTerm) ||
                    d.User.LastName.Contains(searchTerm) ||
                    d.User.Email.Contains(searchTerm) ||
                    d.Specialization.Contains(searchTerm) ||
                    d.CurrentWorkplace.Contains(searchTerm) ||
                    d.LicenseNumber.Contains(searchTerm)
                );
            }

            return await query
                .OrderBy(d => d.User.FirstName)
                .ThenBy(d => d.User.LastName)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Patient>> GetAllPatientsWithUsersAsync(int skip, int take, string? searchTerm, CancellationToken cancellationToken = default)
        {
            var query = _context.Patients
                .Include(p => p.User)
                .Include(p => p.Guardian)
                    .ThenInclude(g => g.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => 
                    p.User.FirstName.Contains(searchTerm) ||
                    p.User.LastName.Contains(searchTerm) ||
                    p.User.Email.Contains(searchTerm) ||
                    p.User.Username.Contains(searchTerm)
                );
            }

            return await query
                .OrderBy(p => p.User.FirstName)
                .ThenBy(p => p.User.LastName)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetDoctorsCountAsync(string? searchTerm, CancellationToken cancellationToken = default)
        {
            var query = _context.Doctors
                .Include(d => d.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d => 
                    d.User.FirstName.Contains(searchTerm) ||
                    d.User.LastName.Contains(searchTerm) ||
                    d.User.Email.Contains(searchTerm) ||
                    d.Specialization.Contains(searchTerm) ||
                    d.CurrentWorkplace.Contains(searchTerm) ||
                    d.LicenseNumber.Contains(searchTerm)
                );
            }

            return await query.CountAsync(cancellationToken);
        }

        public async Task<int> GetPatientsCountAsync(string? searchTerm, CancellationToken cancellationToken = default)
        {
            var query = _context.Patients
                .Include(p => p.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => 
                    p.User.FirstName.Contains(searchTerm) ||
                    p.User.LastName.Contains(searchTerm) ||
                    p.User.Email.Contains(searchTerm) ||
                    p.User.Username.Contains(searchTerm)
                );
            }

            return await query.CountAsync(cancellationToken);
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

        public async Task<Doctor?> GetDoctorByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == doctorId, cancellationToken);
        }

        public async Task UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteDoctorAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            var doctor = await _context.Doctors.FindAsync(new object[] { doctorId }, cancellationToken);
            if (doctor != null)
            {
                var user = await _context.Users.FindAsync(new object[] { doctor.UserId }, cancellationToken);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
