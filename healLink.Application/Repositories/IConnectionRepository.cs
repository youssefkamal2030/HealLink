using System;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IConnectionRepository
    {
        Task<DoctorPatientConnection> AddConnectionAsync(DoctorPatientConnection connection);
        Task<bool> ConnectionExistsAsync(Guid doctorId, Guid patientId);
    }
}
