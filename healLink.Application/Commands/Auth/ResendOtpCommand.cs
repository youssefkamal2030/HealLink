using MediatR;
using healLink.Application.Common.Models;

namespace healLink.Application.Commands.Auth
{
    public record ResendOtpCommand(string Email) : IRequest<Result<bool>>;
}
