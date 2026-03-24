using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Aggregates;

namespace healLink.Application.Repositories
{
    //Todo : this should inherit from the generic repository interface to reduce code duplication 

    public interface IPatientRepository
    {
        Task<PatientAggregate> GetAggregateByPatientId(Guid PatientId);
        Task<string> GetPatientNameById(Guid patientId);
        Task UpdateAggregate(PatientAggregate aggregate);
    }
}
