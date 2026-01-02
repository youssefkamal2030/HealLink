using System;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Aggregates;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HealLink.Domain.Entities;
using MediatR;

namespace HealLink.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealLinkDbContext _context;
        private readonly IMediator _mediator;

        public DoctorRepository(HealLinkDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
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

           
            // Pass the actual connections to the aggregate instead of null
            return new DoctorAggregate(doctor, doctor.Address, doctor.PatientConnections);
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

            // Update each connection that was modified in the aggregate
            // This ensures status changes (Accept/Reject) are persisted to the database
            foreach (var connection in aggregate.Doctor.PatientConnections)
            {
                _context.DoctorPatientConnections.Update(connection);
            }

            await _context.SaveChangesAsync();
            
            // Dispatch domain events from the aggregate after successful save
            var domainEvents = aggregate.DomainEvents.ToList();
            aggregate.ClearDomainEvents();
            
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
