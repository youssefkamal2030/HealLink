using System.Threading;
using System.Threading.Tasks;
using MediatR;
using healLink.Application.Repositories;
using healLink.Application.Interfaces;
using healLink.Application.Commands.Auth;

namespace healLink.Application.Handlers.Auth
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailService _emailService;
        public ForgotPasswordCommandHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                return;
            }
            var token =  _jwtTokenGenerator.CreatePasswordResetHmacCode(user.Id); 
            await _emailService.SendPasswordResetEmailAsync(user.Email, token);
        }
    }
}
