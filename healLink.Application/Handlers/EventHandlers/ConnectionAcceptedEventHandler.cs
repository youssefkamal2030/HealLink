using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Adapters;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.EventHandlers
{
    /// <summary>
    /// Pure side-effect handler — sends the acceptance notification only.
    /// Patient.ConnectedDoctorIds is updated by AcceptConnectionCommandHandler
    /// in the same transaction as the Doctor mutation, before this event fires.
    /// </summary>
    public class ConnectionAcceptedEventHandler : INotificationHandler<DomainEventNotification<ConnectionAcceptedEvent>>
    {
        private readonly INotificationService _notificationService;

        public ConnectionAcceptedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(DomainEventNotification<ConnectionAcceptedEvent> domainEvent, CancellationToken cancellationToken)
        {
            var notification = domainEvent.DomainEvent;
            await _notificationService.NotifyPatientOfAcceptance(
                notification.PatientId,
                notification.DoctorId);
        }
    }
}
