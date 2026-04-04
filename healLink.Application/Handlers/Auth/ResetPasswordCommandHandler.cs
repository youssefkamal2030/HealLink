using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Auth;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Auth.Responses;
using HealLink.Domain.Common;
using MediatR;

namespace healLink.Application.Handlers.Auth
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordCommandHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
                return new ResetPasswordResponse("User not found");

            var validationId = user.Id;
            if (_jwtTokenGenerator.VerifyPasswordResetHmacCode(request.Token, out validationId))
            {
                var newPassword = _passwordHasher.HashPassword(request.NewPassword);
                user.ChangePassword(newPassword.Value);
                await _userRepository.UpdateAsync(user, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return new ResetPasswordResponse("Password reset Successfully");
            }

            return new ResetPasswordResponse("Invalid Token or user ID mismatch");
        }
    }
}
