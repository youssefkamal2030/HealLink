using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class DoctorPatientConnectionRepository : IDoctorPatientConnectionRepository
    {
        private readonly HealLinkDbContext _context;

        public DoctorPatientConnectionRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ConnectionExistsAsync(Guid doctorId, Guid patientId)
            => await _context.DoctorPatientConnections
                .AnyAsync(c => c.DoctorId == doctorId && c.PatientId == patientId);

        public async Task<bool> AcceptedConnectionExistsAsync(Guid doctorId, Guid patientId)
            => await _context.DoctorPatientConnections
                .AnyAsync(c => c.DoctorId == doctorId
                            && c.PatientId == patientId
                            && c.Status == ConnectionStatus.Accepted);

        public async Task<List<DoctorPatientConnection>> GetPendingConnectionsForDoctorAsync(Guid doctorId)
            => await _context.DoctorPatientConnections
                .Where(c => c.DoctorId == doctorId && c.Status == ConnectionStatus.Pending)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public async Task<List<DoctorPatientConnection>> GetConnectionsForDoctorAsync(Guid doctorId)
            => await _context.DoctorPatientConnections
                .Where(c => c.DoctorId == doctorId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public async Task<List<DoctorPatientConnection>> GetConnectionsForPatientAsync(Guid patientId)
            => await _context.DoctorPatientConnections
                .Where(c => c.PatientId == patientId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public async Task<DoctorPatientConnection> GetConnectionByIdAsync(Guid connectionId)
            => await _context.DoctorPatientConnections.FindAsync(connectionId);

        public Task<DoctorPatientConnection> GetByIdAsync(Guid id)
            => _context.DoctorPatientConnections.FindAsync(id).AsTask();

        public Task<IEnumerable<DoctorPatientConnection>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorPatientConnection> AddAsync(DoctorPatientConnection entity)
        {
            await _context.DoctorPatientConnections.AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(DoctorPatientConnection entity)
        {
            _context.DoctorPatientConnections.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(DoctorPatientConnection entity)
        {
            _context.DoctorPatientConnections.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
