using System.Threading;
using System.Threading.Tasks;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class ConnectionAcceptedHandler : INotificationHandler<ConnectionAcceptedEvent>
    {
        private readonly INotificationService _notificationService;

        public ConnectionAcceptedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(ConnectionAcceptedEvent notification, CancellationToken cancellationToken)
        {
            // TODO: [TOMORROW-3] Load the Patient aggregate via IPatientRepository and call patient.AddConnectedDoctor(notification.DoctorId), then persist. This keeps Patient's connected doctor list in sync via domain event rather than direct collection mutation.
            // Send notification to patient about acceptance
            await _notificationService.NotifyPatientOfAcceptance(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
