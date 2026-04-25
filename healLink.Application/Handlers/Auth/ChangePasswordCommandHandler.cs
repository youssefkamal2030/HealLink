using healLink.Application.Commands.Auth;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Common;
using MediatR;

namespace healLink.Application.Handlers.Auth
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return Result<bool>.Failure("User not found.");

            // Verify current password
            if (!_passwordHasher.IsCorrectPassword(request.CurrentPassword, user.PasswordHash))
                return Result<bool>.Failure("Current password is incorrect.");

            // Hash new password
            var newPasswordHashResult = _passwordHasher.HashPassword(request.NewPassword);
            if (newPasswordHashResult.IsError)
                return Result<bool>.Failure("Failed to hash new password.");

            // Change password
            user.ChangePassword(newPasswordHashResult.Value);

            // Update user
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
