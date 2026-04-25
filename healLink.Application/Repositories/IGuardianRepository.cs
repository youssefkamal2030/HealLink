using System;
using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IGuardianRepository
    {
        Task<Guardian?> GetByIdAsync(Guid guardianId, CancellationToken cancellationToken = default);
        Task<Guardian?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Guardian> AddAsync(Guardian guardian, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guardian guardian, CancellationToken cancellationToken = default);
    }
}
