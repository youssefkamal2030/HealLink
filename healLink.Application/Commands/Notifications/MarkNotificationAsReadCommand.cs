using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Notifications
{
    public record MarkNotificationAsReadCommand(Guid NotificationId) : IRequest<Result<bool>>;
}
