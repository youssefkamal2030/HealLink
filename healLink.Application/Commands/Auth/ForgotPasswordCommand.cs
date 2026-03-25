using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    // TODO: [TOMORROW-8] Create ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand> in this folder
    //   Rules: Email NotEmpty + EmailAddress()
    public record ForgotPasswordCommand(
        string Email
    ): IRequest;

}
