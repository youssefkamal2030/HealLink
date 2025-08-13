using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    public record ForgotPasswordCommand(
        string Email
    ): IRequest;

}
