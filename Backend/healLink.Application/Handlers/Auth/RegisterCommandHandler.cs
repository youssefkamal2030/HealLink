using MediatR;
using HealLink.Contracts.Auth;
using HealLink.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Common;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Enums;
using healLink.Application.Commands.Auth;
using healLink.Application.Commands.Profile;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IApplicationDbContext context, IMediator mediator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mediator = mediator;
        _context = context;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.email, cancellationToken);
        if (existingUser != null)
        {
            return new RegisterResponse("Email Already Taken"); 
        }

        
        var hashedPasswordResult = _passwordHasher.HashPassword(request.password);

        if (hashedPasswordResult.IsError)
        {
            return new RegisterResponse("Password hashing failed");
        }

        var hashedPassword = hashedPasswordResult.Value;


        var user = new User(
            request.username,
            hashedPassword,
            firstName: "",
            lastName: "",
            request.email,
            request.Role
        );

        await _userRepository.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var CreateProfileCommand = new CreateProfileCommand(user.Id, user.Role, user.FirstName, user.LastName, "phonenumber", "Address");
        var result = await _mediator.Send(CreateProfileCommand);

        return new RegisterResponse("User registered successfully");
    }
}