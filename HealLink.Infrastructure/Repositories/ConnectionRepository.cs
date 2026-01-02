using System;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
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
    }
}
