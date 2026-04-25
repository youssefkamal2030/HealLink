using MediatR;
using HealLink.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Common;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using healLink.Application.Commands.Auth;
using healLink.Application.Commands.Profile;
using HealLink.Contracts.Auth.Responses;

namespace healLink.Application.Handlers.Auth
{
    public class RegisterCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMediator mediator,
        IEmailService emailService,
        IPhotoService photoService,
        IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.email, cancellationToken);
            if (existingUser != null)
                return new RegisterResponse("Email Already Taken");

            var hashedPasswordResult = passwordHasher.HashPassword(request.password);
            if (hashedPasswordResult.IsError)
                return new RegisterResponse("Password hashing failed");

            var user = User.Register(request.username, hashedPasswordResult.Value, request.email, request.Role);

            // TODO: [Problem B — RESOLVED] EF now tracks User._otps via backing field.
            // HasMany<OTP>("_otps") in HealLinkDbContext means SaveChangesAsync picks up
            // any OTP added via user.RequestOTP() automatically. No explicit AddOtpAsync needed.

            var otp = user.RequestOTP();

            // Stage user + OTP — no commit yet
            await userRepository.AddAsync(user, cancellationToken);

            var syndicateIdPath = request.SyndicateId != null
                ? await photoService.SavePhotoAsync(request.SyndicateId, "uploads")
                : null;

            // Stage profile — CreateProfileCommandHandler does NOT call SaveChangesAsync
            var createProfileCommand = new CreateProfileCommand(
                user.Id, user.Role, request.Specilization, request.PracticeLicenseNumber, syndicateIdPath);
            var result = await mediator.Send(createProfileCommand, cancellationToken);

            if (!result.Success)
                return new RegisterResponse("Profile creation failed: " + result.Message);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Email is a side effect AFTER the commit — if it fails, the user is already
            // persisted and can request a new OTP. No broken state.
            await emailService.SendOtpEmailAsync(user.Email, user.Username, otp.Code, 10);

            return new RegisterResponse("User registered successfully");
        }
    }
}
