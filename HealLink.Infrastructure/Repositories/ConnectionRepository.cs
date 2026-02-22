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
    public class DoctorPatientConnectionRepository : IDoctorPatientDoctorPatientConnectionRepository
    {
        private readonly HealLinkDbContext _context;

        public DoctorPatientConnectionRepository(HealLinkDbContext context)
        {
            _context = context;
        }

      

        public async Task<bool> ConnectionExistsAsync(Guid doctorId, Guid patientId)
        {
            return await _context.DoctorPatientConnections
                .AnyAsync(c => c.DoctorId == doctorId && c.PatientId == patientId);
        }

        public async Task<List<DoctorPatientConnection>> GetPendingConnectionsForDoctorAsync(Guid doctorId)
        {
            return await _context.DoctorPatientConnections
                .Where(c => c.DoctorId == doctorId && c.Status == ConnectionStatus.Pending)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<DoctorPatientConnection>> GetConnectionsForDoctorAsync(Guid doctorId)
        {
            return await _context.DoctorPatientConnections
                .Where(c => c.DoctorId == doctorId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<DoctorPatientConnection>> GetConnectionsForPatientAsync(Guid patientId)
        {
            return await _context.DoctorPatientConnections
                .Where(c => c.PatientId == patientId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<DoctorPatientConnection> GetConnectionByIdAsync(Guid connectionId)
        {
            return await _context.DoctorPatientConnections.FindAsync(connectionId);
        }

        public Task<DoctorPatientConnection> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DoctorPatientConnection>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorPatientConnection> AddAsync(DoctorPatientConnection entity)
        {
			await _context.DoctorPatientConnections.AddAsync(entity);
			await _context.SaveChangesAsync();
            return entity;
		}

        public Task UpdateAsync(DoctorPatientConnection entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DoctorPatientConnection entity)
        {
            throw new NotImplementedException();
        }
    }
}
