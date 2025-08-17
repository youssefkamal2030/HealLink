using System;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Aggregates;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HealLink.Domain.Entities;
using healLink.Application.Repositories;

namespace HealLink.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealLinkDbContext _context;

        public DoctorRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorAggregate> GetAggregateByDoctorId(Guid doctorId)
        {
            if (doctorId == Guid.Empty)
            {
                throw new ArgumentException("Doctor ID cannot be empty.", nameof(doctorId));
            }

        
            var doctor = await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Address)
                .Include(d => d.PatientConnections) 
                .ThenInclude(c => c.Patient)
                .FirstOrDefaultAsync(d => d.Id == doctorId);

            if (doctor == null)
            {
                return null;
            }

           
            return new DoctorAggregate(doctor, doctor.Address, null);
        }

        public async Task UpdateAggregate(DoctorAggregate aggregate)
        {
            if (aggregate?.Doctor == null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }

            _context.Doctors.Update(aggregate.Doctor);

            if (aggregate.Doctor.User != null)
            {
                _context.Users.Update(aggregate.Doctor.User);
            }

            await _context.SaveChangesAsync();
        }
    }
}