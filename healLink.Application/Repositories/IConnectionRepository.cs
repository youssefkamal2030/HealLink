using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    // Todo: there should be only one repisitory for aggregate to algin with DDD principles, but for now we will keep it simple and have separate repositories for doctor and patient connections
    public interface IDoctorPatientDoctorPatientConnectionRepository : IRepository<DoctorPatientConnection>
    {
        Task<bool> ConnectionExistsAsync(Guid doctorId, Guid patientId);
        Task<List<DoctorPatientConnection>> GetPendingConnectionsForDoctorAsync(Guid doctorId);
        Task<List<DoctorPatientConnection>> GetConnectionsForDoctorAsync(Guid doctorId);
        Task<List<DoctorPatientConnection>> GetConnectionsForPatientAsync(Guid patientId);
        Task<DoctorPatientConnection> GetConnectionByIdAsync(Guid connectionId);
    }
}
