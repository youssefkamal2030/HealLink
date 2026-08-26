using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HealLinkDbContext _context;

        public UserRepository(HealLinkDbContext context)
        {
            _context = context;
        }
        // Refactor : this methods fetchs the user with OTPs included, consider if this is always necessary for performance reasons
        // consider specification pattern or query objects to fetch only what is needed only instead of creating multiple methods for different scenarios
        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
            => await _context.Users
                .Include(u => u.Otps)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
            => await _context.Users
                .Include(u => u.Otps)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => _context.Users
                .Include(u => u.Otps)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        public async Task AddAsync(User user, CancellationToken cancellationToken)
            => await _context.Users.AddAsync(user, cancellationToken);

        public Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public async Task AddOtpAsync(OTP otp, CancellationToken cancellationToken = default)
            => await _context.OTPs.AddAsync(otp, cancellationToken);

        public Task UpdateOtpAsync(OTP otp, CancellationToken cancellationToken = default)
        {
            _context.OTPs.Update(otp);
            return Task.CompletedTask;
        }

        public async Task<OTP?> GetActiveOtpAsync(Guid userId, string code, CancellationToken cancellationToken = default)
            => await _context.OTPs
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Code == code, cancellationToken);
    }
}
