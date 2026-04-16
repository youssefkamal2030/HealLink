using System;
using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Prescriptions
{
    public record MarkReminderAsTakenCommand(
        Guid ReminderId,
        Guid PatientId,
        Guid ActingUserId
    ) : IRequest<Result<bool>>;
}
