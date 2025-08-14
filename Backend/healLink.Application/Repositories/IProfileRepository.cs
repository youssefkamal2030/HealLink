using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IProfileRepository
    {
        Task<Patient?> GetPatientByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Doctor?> GetDoctorByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<string?> GetGuardianNameByIdAsync(Guid guardianId, CancellationToken cancellationToken = default);

        Task AddPatientAsync(Patient patient, CancellationToken cancellationToken = default);
        Task AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
    }
}
