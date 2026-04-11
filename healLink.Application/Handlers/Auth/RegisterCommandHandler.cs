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

            // ── OTP DESIGN ISSUE ─────────────────────────────────────────────────────────────────
            //
            // CURRENT APPROACH (what this code does today):
            //   1. Generate a random OTP code inline here in the handler
            //   2. Call user.RequestOTP(code, expiry) — domain method that adds OTP to the aggregate
            //   3. Stage user via userRepository.AddAsync — EF tracks the OTP via the _otps backing field
            //   4. Single SaveChangesAsync — user + OTP committed atomically
            //   5. Send the OTP email AFTER the commit as a side effect
            //
            // WHY THIS IS STILL PROBLEMATIC:
            //
            //   Problem A — OTP code generation belongs in the domain, not the handler.
            //   The handler is generating a random 6-digit code with `new Random()`. This is
            //   application/infrastructure logic leaking into the handler. The domain should own
            //   the OTP generation policy (length, character set, expiry duration). Options:
            //     - Move generation into user.RequestOTP() itself (domain generates its own code)
            //     - Inject an IOtpGenerator interface and call it here (application layer owns policy)
            //   The IOtpGenerator interface already exists at healLink.Application/Interfaces/IOtpGenerator.cs
            //   — it just isn't being used here yet.
            //
            //   Problem B — EF tracking of OTPs via backing field is fragile.
            //   User._otps is a private List<OTP>. EF Core can track it via HasMany + backing field
            //   configuration, but only if the DbContext is configured correctly with
            //   .HasMany(u => u.OTPs, "_otps") or similar. If that config is missing or wrong,
            //   the OTP is silently dropped on SaveChanges. Verify the EF config in HealLinkDbContext
            //   actually maps the backing field, or use userRepository.AddOtpAsync(otp) explicitly
            //   after staging the user.
            //
            //   Problem C — Profile creation is a separate SaveChangesAsync inside CreateProfileCommand.
            //   The mediator.Send(createProfileCommand) call below triggers another handler that calls
            //   SaveChangesAsync internally. This means registration is NOT atomic:
            //     - Commit 1: user + OTP
            //     - Commit 2: patient/doctor profile (inside CreateProfileCommandHandler)
            //   If Commit 2 fails, the user exists in the DB without a profile. The user can't log in
            //   (no profile) and can't re-register (email already taken). This is a broken state.
            //
            // RECOMMENDED FIX FOR TOMORROW:
            //   1. Inject IOtpGenerator, call otpGenerator.Generate() to get the code
            //   2. Stage the profile creation in the same unit of work as the user+OTP
            //      (move profile creation logic here instead of dispatching a separate command,
            //       OR make CreateProfileCommand not call SaveChangesAsync and let this handler
            //       do the single final commit)
            //   3. Single SaveChangesAsync at the end covering user + OTP + profile
            //   4. Send email after the single commit
            //
            // ─────────────────────────────────────────────────────────────────────────────────────

            // Generate OTP code here — pure domain mutation, no infra dependency
            var otpCode = new Random().Next(100000, 999999).ToString();
            user.RequestOTP(otpCode, DateTime.UtcNow.AddMinutes(10));

            // Stage user — EF tracks the OTP via the _otps backing field (HasMany via backing field)
            await userRepository.AddAsync(user, cancellationToken);

            // Single atomic commit: user + OTP together
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Side effect AFTER commit — if email fails, user+OTP are already persisted
            // and the user can request a new OTP. No broken state.
            await emailService.SendOtpEmailAsync(user.Email, user.Username, otpCode, 10);

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
