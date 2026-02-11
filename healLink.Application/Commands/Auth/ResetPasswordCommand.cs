using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Auth.Responses;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    public record ResetPasswordCommand(
        string Email,
        string Token,
        string NewPassword
    ) : IRequest<ResetPasswordResponse>;


}
