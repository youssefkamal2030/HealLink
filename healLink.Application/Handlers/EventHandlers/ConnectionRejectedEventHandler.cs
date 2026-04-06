using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Adapters;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.EventHandlers
{
    /// <summary>
    /// Pure side-effect handler — sends the rejection notification only.
    /// Patient.ConnectedDoctorIds is updated by RejectConnectionCommandHandler
    /// in the same transaction as the Doctor mutation, before this event fires.
    /// </summary>
    public class ConnectionRejectedEventHandler : INotificationHandler<DomainEventNotification<ConnectionRejectedEvent>>
    {
        private readonly INotificationService _notificationService;

        public ConnectionRejectedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(DomainEventNotification<ConnectionRejectedEvent> domainEvent, CancellationToken cancellationToken)
        {
            var notification = domainEvent.DomainEvent;
            await _notificationService.NotifyPatientOfRejection(
                notification.PatientId,
                notification.DoctorId);
        }
    }
}
