using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Notifications;
using MediatR;

namespace healLink.Application.Handlers.Notifications
{
    public class GetAllDoctorNotificationsQueryHandler : IRequestHandler<GetAllDoctorNotificatonsQuery, Result<NotificationsListResponse>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetAllDoctorNotificationsQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<NotificationsListResponse>> Handle(GetAllDoctorNotificatonsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetDoctorNotificationsAsync(request.DoctorId);

            var notificationResponses = notifications.Select(n => new NotificationResponse(
                Id: n.Id,
                Title: n.Title,
                Message: n.Message,
                Type: n.Type,
                IsRead: n.IsRead,
                ReadAt: n.ReadAt,
                CreatedAt: n.CreatedAt
            )).ToList();

            var response = new NotificationsListResponse(
                Success: true,
                Message: "Notifications retrieved successfully.",
                Notifications: notificationResponses,
                TotalCount: notificationResponses.Count
            );

            return Result<NotificationsListResponse>.Success(response);
        }
    }
}
