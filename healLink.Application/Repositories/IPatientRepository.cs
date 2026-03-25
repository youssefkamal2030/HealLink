using System;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    //Todo : this should inherit from the generic repository interface to reduce code duplication 
    public interface IPatientRepository
    {
        Task<Patient> GetByPatientId(Guid patientId);
        Task<string> GetPatientNameById(Guid patientId);
        Task UpdateAsync(Patient patient);
    }
}
