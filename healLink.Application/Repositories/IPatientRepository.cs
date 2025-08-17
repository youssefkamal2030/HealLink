using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Aggregates;

namespace healLink.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<PatientAggregate> GetAggregateByPatientId(Guid PatientId);
        Task UpdateAggregate(PatientAggregate aggregate);
    }
}
