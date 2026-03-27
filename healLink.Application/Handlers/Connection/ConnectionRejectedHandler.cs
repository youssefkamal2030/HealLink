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
            // TODO: [TOMORROW-3] Load the Patient aggregate via IPatientRepository and call patient.RemoveConnectedDoctor(notification.DoctorId) if present, then persist. Keeps Patient's connected doctor list consistent on rejection.
            // Send notification to patient about rejection
            await _notificationService.NotifyPatientOfRejection(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
