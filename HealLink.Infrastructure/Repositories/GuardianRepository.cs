using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly HealLinkDbContext _context;

        public GuardianRepository(HealLinkDbContext context) => _context = context;

        public async Task<Guardian?> GetByIdAsync(Guid guardianId, CancellationToken cancellationToken = default)
            => await _context.Guardians
                .Include(g => g.User)
                .FirstOrDefaultAsync(g => g.Id == guardianId, cancellationToken);

        public async Task<Guardian?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
            => await _context.Guardians
                .Include(g => g.User)
                .FirstOrDefaultAsync(g => g.UserId == userId, cancellationToken);

        public async Task<Guardian> AddAsync(Guardian guardian, CancellationToken cancellationToken = default)
        {
            await _context.Guardians.AddAsync(guardian, cancellationToken);
            return guardian;
        }

        public Task UpdateAsync(Guardian guardian, CancellationToken cancellationToken = default)
        {
            _context.Guardians.Update(guardian);
            return Task.CompletedTask;
        }
    }
}
