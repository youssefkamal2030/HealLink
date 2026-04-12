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

            var user = new User(request.username, hashedPasswordResult.Value, request.email, request.Role);

            // ── REMAINING ISSUE ──────────────────────────────────────────────────────────────────
            //
            // Problem A (RESOLVED) — OTP generation is now owned by the domain.
            //   user.RequestOTP() calls OTP.Generate() internally. The handler has no knowledge
            //   of OTP length, character set, or expiry duration.
            //
            // Problem B — EF tracking of OTPs via backing field needs verification.
            //   User._otps is a private List<OTP>. EF Core must be configured with
            //   .HasMany with UsePropertyAccessMode(PropertyAccessMode.Field) or equivalent
            //   in HealLinkDbContext so the OTP is tracked when user is staged via AddAsync.
            //   If the config is wrong, the OTP is silently dropped on SaveChanges.
            //   TODO: Verify HealLinkDbContext maps User._otps via backing field, or add
            //   explicit userRepository.AddOtpAsync(otp) after staging the user.
            //
            // Problem C — Profile creation is a separate SaveChangesAsync (NOT ATOMIC).
            //   mediator.Send(createProfileCommand) triggers CreateProfileCommandHandler which
            //   calls SaveChangesAsync internally. Registration has two commits:
            //     - Commit 1: user + OTP  (here)
            //     - Commit 2: patient/doctor profile  (inside CreateProfileCommandHandler)
            //   If Commit 2 fails, the user exists without a profile — broken state.
            //   TODO: Make CreateProfileCommandHandler not call SaveChangesAsync, stage the
            //   profile here, and do a single final SaveChangesAsync covering everything.
            //
            // ─────────────────────────────────────────────────────────────────────────────────────

            var otp = user.RequestOTP();

            // Stage user — EF tracks the OTP via the _otps backing field (HasMany via backing field)
            await userRepository.AddAsync(user, cancellationToken);

            // Single atomic commit: user + OTP together
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Side effect AFTER commit — if email fails, user+OTP are already persisted
            // and the user can request a new OTP. No broken state.
            await emailService.SendOtpEmailAsync(user.Email, user.Username, otp.Code, 10);

            var syndicateIdPath = request.SyndicateId != null
                ? await photoService.SavePhotoAsync(request.SyndicateId, "uploads")
                : null;

            var createProfileCommand = new CreateProfileCommand(
                user.Id, user.Role, request.Specilization, request.PracticeLicenseNumber, syndicateIdPath);
            var result = await mediator.Send(createProfileCommand, cancellationToken);

            if (!result.Success)
                return new RegisterResponse("Profile creation failed: " + result.Message);

            return new RegisterResponse("User registered successfully");
        }
    }
}
