using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Auth;
using HealLink.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace healLink.Application.Commands.Auth
{
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
