using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using healLink.Application.Commands.Auth;
using healLink.Application.Repositories;
using HealLink.Contracts.Auth;
using HealLink.Domain.Common;
using MediatR;

namespace healLink.Application.Handlers.Auth
{
   public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;

        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                return new ResetPasswordResponse("User not found");
            }

            if (!await _userRepository.CheckOtpAsync(user.Id, request.Code, cancellationToken))
            {
                return new ResetPasswordResponse("Invalid OTP");
            }

            var validationId = user.Id;
            if (_jwtTokenGenerator.VerifyPasswordResetHmacCode(request.Token, out validationId))
            {
                var newPassword = _passwordHasher.HashPassword(request.NewPassword);
                user.ChangePassword(newPassword.Value);
                await _userRepository.UpdateAsync(user, cancellationToken);

                return new ResetPasswordResponse("Password reset successfully");
            }
            else
            {
                return new ResetPasswordResponse("Invalid token or user ID mismatch");
            }
        }
    }
}
