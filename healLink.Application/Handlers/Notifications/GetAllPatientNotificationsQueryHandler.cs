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
    public class GetAllPatientNotificationsQueryHandler : IRequestHandler<GetAllPatientNotificationsQuery, Result<NotificationsListResponse>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetAllPatientNotificationsQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<NotificationsListResponse>> Handle(GetAllPatientNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetPatientNotificationsAsync(request.PatientId);

            var notificationResponses = notifications.Select(n => new NotificationResponse(
                Id: n.Id,
                Title: n.Title,
                Message: n.Message,
                Type: n.Type.ToString(),
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
