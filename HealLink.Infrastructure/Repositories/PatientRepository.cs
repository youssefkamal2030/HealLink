using System;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class PatientRepository(HealLinkDbContext _context, IMediator _mediator) : IPatientRepository
    {
        public async Task<Patient> GetByPatientId(Guid patientId)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Include(p => p.Guardian)
                .Include(p => p.ConnectedDoctorIds)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public Task<string> GetPatientNameById(Guid patientId)
        {
            return _context.Patients
                .Where(p => p.Id == patientId)
                .Select(p => p.User.Username)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            var domainEvents = patient.DomainEvents.ToList();
            patient.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}
