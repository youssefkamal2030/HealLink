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

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailService.SendOtpAsync(user);

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