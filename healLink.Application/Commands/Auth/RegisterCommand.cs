using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Auth.Responses;
using HealLink.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace healLink.Application.Commands.Auth
{
    // TODO: [TOMORROW-6] Create RegisterCommandValidator : AbstractValidator<RegisterCommand> in this folder
    //   Rules: username NotEmpty + MinimumLength(3), email NotEmpty + EmailAddress(),
    //          password Matches("[a-z]") + Matches("[0-9]"), Role must be Patient or Doctor
    public record RegisterCommand(
        string username,
        string password,
        string email,
        UserRole Role,
        string? Specilization,
        string? PracticeLicenseNumber,
        IFormFile? SyndicateId
    ) : IRequest<RegisterResponse>;
}
