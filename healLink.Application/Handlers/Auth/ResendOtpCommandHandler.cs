using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Auth;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Auth
{
    public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public ResendOtpCommandHandler(
            IUserRepository userRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
                // Return success even for unknown emails to prevent user enumeration
                return Result<bool>.Success(true);

            if (user.EmailConfirmed)
                return Result<bool>.Failure("Email is already confirmed.");

            var otp = user.RequestOTP();

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _emailService.SendOtpEmailAsync(user.Email, user.Username, otp.Code, 10);

            return Result<bool>.Success(true);
        }
    }
}
