using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    
    // TODO: [TASK-B] Create ForgotPasswordCommandValidator in this folder — rules: RuleFor(x => x.Email).NotEmpty().EmailAddress().
    public record ForgotPasswordCommand(
        string Email
    ): IRequest;

}
