using System;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    // ToDo: the rest of the generic IRepository<T> methods (GetAllAsync, DeleteAsync) still need implementing.
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealLinkDbContext _context;

        public DoctorRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public Task<Doctor> AddAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> GetByDoctorId(Guid doctorId)
        {
            if (doctorId == Guid.Empty)
                throw new ArgumentException("Doctor ID cannot be empty.", nameof(doctorId));

            return await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Address)
                .Include(d => d.PatientConnections)
                    .ThenInclude(c => c.Patient)
                .FirstOrDefaultAsync(d => d.Id == doctorId);
        }

        public Task UpdateAsync(Doctor doctor)
        {
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));

            _context.Doctors.Update(doctor);

            if (doctor.User != null)
                _context.Users.Update(doctor.User);

            foreach (var connection in doctor.PatientConnections)
                _context.DoctorPatientConnections.Update(connection);

            return Task.CompletedTask;
        }
    }
}
