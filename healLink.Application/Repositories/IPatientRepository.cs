using System;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    //Todo : this should inherit from the generic repository interface to reduce code duplication 
    public interface IPatientRepository
    {
        Task<Patient> GetByPatientId(Guid patientId);
        Task<Patient> GetByPatientIdWithRemindersAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<List<Patient>> GetByPatientIdsAsync(IEnumerable<Guid> patientIds, CancellationToken cancellationToken = default);
        Task<string> GetPatientNameById(Guid patientId);
        Task UpdateAsync(Patient patient);
        Task<MedicalHistory?> GetMedicalHistoryAsync(Guid patientId, CancellationToken cancellationToken = default);
    }
}
