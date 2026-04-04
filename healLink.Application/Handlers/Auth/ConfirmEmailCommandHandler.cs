using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Auth;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Auth
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
                return Result<bool>.Failure("User not found.");

            if (user.EmailConfirmed)
                return Result<bool>.Failure("Email is already confirmed.");

            // Load the OTP directly from the repository — EF doesn't track the private _otps collection
            var otp = await _userRepository.GetActiveOtpAsync(user.Id, request.OtpCode, cancellationToken);

            if (otp == null)
                return Result<bool>.Failure("Invalid OTP code.");

            if (otp.IsExpired())
                return Result<bool>.Failure("OTP has expired. Please request a new one.");

            if (otp.IsUsed)
                return Result<bool>.Failure("OTP has already been used.");

            // Invalidate the OTP and confirm the email
            otp.Invalidate();
            user.ConfirmEmail();
            user.Activate();

            await _userRepository.UpdateOtpAsync(otp, cancellationToken);
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
