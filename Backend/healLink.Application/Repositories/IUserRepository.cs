using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> CheckOtpAsync(Guid userId, string otpCode, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
    }
} 