using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class ConnectionRequestsRepository(HealLinkDbContext dbContext) : IConnectionRequestsRepository
    {
        private readonly HealLinkDbContext DbContext = dbContext;
        public async Task<ConnectionRequest> AddConnectionAsync(ConnectionRequest connectionRequest)
        {

            await DbContext.connectionRequests.AddAsync(connectionRequest);
            await DbContext.SaveChangesAsync();
            return connectionRequest;
        }

        public Task DeleteConnectionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistAsync(Guid doctorId, Guid patientId)
        {
            return await DbContext.connectionRequests
                .AnyAsync(cr => cr.DoctorId == doctorId && cr.PatientId == patientId);
        }

        public Task<IEnumerable<ConnectionRequest>> GetAllConnectionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ConnectionRequest> GetConnectionByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateConnectionAsync(ConnectionRequest connectionRequest)
        {
            throw new NotImplementedException();
        }
    }
}
