using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    //Todo : this should inherit from the generic repository interface to reduce code duplication 

    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
        Task AddOtpAsync(OTP otp, CancellationToken cancellationToken = default);
        Task UpdateOtpAsync(OTP otp, CancellationToken cancellationToken = default);
        Task<OTP?> GetActiveOtpAsync(Guid userId, string code, CancellationToken cancellationToken = default);
    }
} 