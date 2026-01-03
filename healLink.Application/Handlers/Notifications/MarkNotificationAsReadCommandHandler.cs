using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Notifications;
using healLink.Application.Common.Models;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Notifications
{
    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, Result<bool>>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<bool>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
            
            if (notification == null)
            {
                return Result<bool>.Failure("Notification not found.");
            }

            notification.MarkAsRead();
            await _notificationRepository.UpdateNotificationAsync(notification);

            return Result<bool>.Success(true);
        }
    }
}
