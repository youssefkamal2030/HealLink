using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Auth;
using HealLink.Domain.Enums;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    public record RegisterCommand(string username , string password , string email, UserRole Role, string Idfront, string Idback) : IRequest<RegisterResponse>;
}
