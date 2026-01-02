using System.Threading;
using System.Threading.Tasks;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class ConnectionRejectedHandler : INotificationHandler<ConnectionRejectedEvent>
    {
        private readonly INotificationService _notificationService;

        public ConnectionRejectedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(ConnectionRejectedEvent notification, CancellationToken cancellationToken)
        {
            // Send notification to patient about rejection
            await _notificationService.NotifyPatientOfRejection(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
