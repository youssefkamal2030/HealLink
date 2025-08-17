using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Aggregates;

namespace HealLink.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public Task<PatientAggregate> GetAggregateByPatientId(Guid PatientId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAggregate(PatientAggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
