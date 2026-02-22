using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using HealLink.Domain.Aggregates;

namespace healLink.Application.Repositories
{
    public interface IDoctorRepository : IRepository<DoctorAggregate>
    {
        Task<DoctorAggregate> GetAggregateByDoctorId(Guid doctorId);
        Task UpdateAggregate(DoctorAggregate aggregate);
    }
}
