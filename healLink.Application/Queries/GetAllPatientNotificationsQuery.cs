using healLink.Application.Common.Models;
using HealLink.Contracts.Notifications;
using MediatR;

namespace healLink.Application.Queries
{
    public record GetAllPatientNotificationsQuery(Guid PatientId) : IRequest<Result<NotificationsListResponse>>;
}
