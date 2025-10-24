using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Aggregates;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class PatientRepository(HealLinkDbContext _context) : IPatientRepository
    {
        private readonly HealLinkDbContext _context = _context;
        public Task<PatientAggregate> GetAggregateByPatientId(Guid PatientId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPatientNameById(Guid patientId)
        {
            return _context.Patients
               .Where(p => p.Id == patientId)
               .Select(p => p.User.Username)
               .FirstOrDefaultAsync() ;
        }

        public Task UpdateAggregate(PatientAggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
