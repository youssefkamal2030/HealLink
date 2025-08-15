using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Entities;
using healLink.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using HealLink.Infrastructure.Data;

namespace HealLink.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HealLinkDbContext _context;
        public UserRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
   

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
} 