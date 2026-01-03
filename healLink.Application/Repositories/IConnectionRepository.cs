using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IConnectionRepository
    {
        Task<DoctorPatientConnection> AddConnectionAsync(DoctorPatientConnection connection);
        Task<bool> ConnectionExistsAsync(Guid doctorId, Guid patientId);
        Task<List<DoctorPatientConnection>> GetPendingConnectionsForDoctorAsync(Guid doctorId);
        Task<List<DoctorPatientConnection>> GetConnectionsForDoctorAsync(Guid doctorId);
        Task<List<DoctorPatientConnection>> GetConnectionsForPatientAsync(Guid patientId);
        Task<DoctorPatientConnection> GetConnectionByIdAsync(Guid connectionId);
    }
}
