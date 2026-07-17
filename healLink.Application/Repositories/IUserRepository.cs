using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    // [WONTFIX] TODO: Generic repository inheritance not needed - IUserRepository has specialized methods
    // REASON: User repository has unique methods (GetByEmailAsync, GetActiveOtpAsync, UpdateOtpAsync)
    //         that don't fit generic IRepository<T> pattern. Keeping as-is for clarity.

    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
        Task UpdateOtpAsync(OTP otp, CancellationToken cancellationToken = default);
        Task<OTP?> GetActiveOtpAsync(Guid userId, string code, CancellationToken cancellationToken = default);
    }
} 