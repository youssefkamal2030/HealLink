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
        Task<Doctor?> GetDoctorByIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
        Task<string?> GetGuardianNameByIdAsync(Guid guardianId, CancellationToken cancellationToken = default);

        Task<List<Doctor>> GetAllDoctorsWithUsersAsync(int skip, int take, string? searchTerm, CancellationToken cancellationToken = default);
        Task<List<Patient>> GetAllPatientsWithUsersAsync(int skip, int take, string? searchTerm, CancellationToken cancellationToken = default);
        Task<int> GetDoctorsCountAsync(string? searchTerm, CancellationToken cancellationToken = default);
        Task<int> GetPatientsCountAsync(string? searchTerm, CancellationToken cancellationToken = default);

        Task AddPatientAsync(Patient patient, CancellationToken cancellationToken = default);
        Task AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
        Task UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
        Task DeleteDoctorAsync(Guid doctorId, CancellationToken cancellationToken = default);
    }
}
