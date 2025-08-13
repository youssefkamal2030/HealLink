using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Auth;
using MediatR;

namespace healLink.Application.Commands.Auth
{
  public record LoginCommand(string Email , string Password): IRequest<LoginResponse>;
}
