using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IConnectionRequestsRepository
    {
        Task<IEnumerable<ConnectionRequest>> GetAllConnectionsAsync();
        Task<ConnectionRequest> GetConnectionByIdAsync(Guid id);
        Task<ConnectionRequest> AddConnectionAsync(ConnectionRequest connectionRequest);
        Task UpdateConnectionAsync(ConnectionRequest connectionRequest);
        Task DeleteConnectionAsync(Guid id);
        Task<bool> ExistAsync(Guid doctorId, Guid patientId);

    }
}
