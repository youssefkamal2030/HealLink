using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Auth.Responses;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    // TODO: [TOMORROW-7] Create LoginCommandValidator : AbstractValidator<LoginCommand> in this folder
    //   Rules: Email NotEmpty + EmailAddress(), Password NotEmpty
    public record LoginCommand(string Email , string Password): IRequest<LoginResponse>;
}
