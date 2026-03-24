using System;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HealLink.Domain.Entities;
using MediatR;

namespace HealLink.Infrastructure.Repositories
{
    // ToDo: the rest of the methods need to be implemented and remove the duplicate code with the generic CRUDE methods in the generic repository interface
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealLinkDbContext _context;
        private readonly IMediator _mediator;

        public DoctorRepository(HealLinkDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public Task<Doctor> AddAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> GetByDoctorId(Guid doctorId)
        {
            if (doctorId == Guid.Empty)
                throw new ArgumentException("Doctor ID cannot be empty.", nameof(doctorId));

            var doctor = await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Address)
                .Include(d => d.Connections)
                .ThenInclude(c => c.Patient)
                .FirstOrDefaultAsync(d => d.Id == doctorId);

            return doctor;
        }

        public Task<IEnumerable<Doctor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            _context.Doctors.Update(doctor);

            if (doctor.User != null)
                _context.Users.Update(doctor.User);

            foreach (var connection in doctor.PatientConnections)
                _context.DoctorPatientConnections.Update(connection);

            await _context.SaveChangesAsync();

            // Dispatch domain events after successful save
            var domainEvents = doctor.DomainEvents.ToList();
            doctor.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}
