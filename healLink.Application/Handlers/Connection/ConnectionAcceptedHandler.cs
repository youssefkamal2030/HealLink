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
            // Send notification to patient about acceptance
            await _notificationService.NotifyPatientOfAcceptance(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
