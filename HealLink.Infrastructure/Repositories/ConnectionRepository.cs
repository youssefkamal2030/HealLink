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
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly HealLinkDbContext _context;

        public ConnectionRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorPatientConnection> AddConnectionAsync(DoctorPatientConnection connection)
        {
            await _context.DoctorPatientConnections.AddAsync(connection);
            await _context.SaveChangesAsync();
            return connection;
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
    }
}
