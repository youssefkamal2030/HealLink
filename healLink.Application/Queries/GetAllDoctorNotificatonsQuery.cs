using healLink.Application.Common.Models;
using HealLink.Contracts.Notifications;
using MediatR;

namespace healLink.Application.Queries
{
    public record GetAllDoctorNotificatonsQuery(Guid DoctorId) : IRequest<Result<NotificationsListResponse>>;
}
