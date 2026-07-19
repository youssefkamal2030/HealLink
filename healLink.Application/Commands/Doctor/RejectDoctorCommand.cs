using System;
using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Doctor
{
    public record RejectDoctorCommand(
        Guid DoctorId,
        string Reason,
        Guid AdminId
    ) : IRequest<Result<bool>>;
}
