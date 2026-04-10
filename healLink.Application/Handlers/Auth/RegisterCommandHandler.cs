using MediatR;
using HealLink.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Common;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Enums;
using healLink.Application.Commands.Auth;
using healLink.Application.Commands.Profile;
using HealLink.Contracts.Auth.Responses;

public class RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IMediator mediator, IEmailService emailService, IPhotoService photoService, IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IPhotoService _photoService = photoService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IMediator _mediator = mediator;
    private readonly IEmailService _emailService = emailService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.email, cancellationToken);
        if (existingUser != null)
            return new RegisterResponse("Email Already Taken");

        var hashedPasswordResult = _passwordHasher.HashPassword(request.password);
        if (hashedPasswordResult.IsError)
            return new RegisterResponse("Password hashing failed");

        var user = new User(request.username, hashedPasswordResult.Value, request.email, request.Role);

        // TODO: [REFACTOR] Two SaveChangesAsync calls break atomicity — if the second save fails,
        // the user row exists in the DB but has no OTP, and the email was already sent.
        // The user can't verify, can't log in, and re-registering fails with "Email Already Taken".
        //
        // Root cause: EmailService.SendOtpAsync() mutates the aggregate AND sends the email in one call,
        // forcing us to flush the user first so OTP.UserId FK has a valid PK to reference.
        //
        // Fix:
        //   1. Generate the OTP code here in the handler (e.g. Random or a dedicated service)
        //   2. Call user.RequestOTP(code, expiry) directly — pure domain mutation, no infra
        //   3. Stage user (EF tracks OTP via navigation collection — no AddOtpAsync needed)
        //   4. Single SaveChangesAsync — user + OTP committed atomically
        //   5. THEN call _emailService.SendEmailAsync(user.Email, otpCode) — side effect after commit
        //
        // See also: IEmailService.SendOtpAsync (remove it), EmailService.SendOtpAsync (remove it)

        // Stage the user first so it exists before the OTP references it
        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // SendOtpAsync calls user.RequestOTP() and returns the code.
        // We then stage the new OTP and save — one commit for user + OTP together.
        var otpCode = await _emailService.SendOtpAsync(user);
        var newOtp = user.OTPs.First(o => o.Code == otpCode);
        await _userRepository.AddOtpAsync(newOtp, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var syndicateIdPath = request.SyndicateId != null
            ? await _photoService.SavePhotoAsync(request.SyndicateId, "uploads")
            : null;

        var createProfileCommand = new CreateProfileCommand(user.Id, user.Role, request.Specilization, request.PracticeLicenseNumber, syndicateIdPath);
        var result = await _mediator.Send(createProfileCommand, cancellationToken);

        if (!result.Success)
            return new RegisterResponse("Profile creation failed: " + result.Message);

        return new RegisterResponse("User registered successfully");
    }
}