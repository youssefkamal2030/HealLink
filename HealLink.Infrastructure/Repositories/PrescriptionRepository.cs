using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HealLinkDbContext _context;

        public PrescriptionRepository(HealLinkDbContext context) => _context = context;

        public async Task<Prescription> AddAsync(Prescription prescription, CancellationToken cancellationToken = default)
        {
            await _context.Prescriptions.AddAsync(prescription, cancellationToken);
            return prescription;
        }

        public async Task<List<Prescription>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
            => await _context.Prescriptions
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);

        public async Task<List<Prescription>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
            => await _context.Prescriptions
                .Where(p => p.DoctorId == doctorId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);

        public async Task<Prescription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Prescriptions.FindAsync(new object[] { id }, cancellationToken);
    }
}
