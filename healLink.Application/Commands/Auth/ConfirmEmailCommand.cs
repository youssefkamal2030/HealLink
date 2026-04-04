using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    public record ConfirmEmailCommand(string Email, string OtpCode) : IRequest<Result<bool>>;
}
