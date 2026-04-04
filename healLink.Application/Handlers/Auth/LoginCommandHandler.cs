using MediatR;
using HealLink.Domain.Entities;
using healLink.Application.Interfaces;
using HealLink.Domain.Common;
using healLink.Application.Repositories;
using healLink.Application.Commands.Auth;
using HealLink.Contracts.Auth.Responses;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null || !_passwordHasher.IsCorrectPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        if (!user.EmailConfirmed)
            throw new UnauthorizedAccessException("Email not confirmed. Please verify your email before logging in.");

        var token = await _jwtTokenGenerator.GenerateTokenAsync(user);
        return new LoginResponse(user.Id, user.Username, user.Email, token);
    }
}

