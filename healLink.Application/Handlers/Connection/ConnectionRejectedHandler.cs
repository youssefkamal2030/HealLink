using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class ConnectionRejectedHandler : INotificationHandler<ConnectionRejectedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IPatientRepository _patientRepository; 

        public ConnectionRejectedHandler(INotificationService notificationService , IPatientRepository patientRepository)
        {
            _notificationService = notificationService;
            _patientRepository = patientRepository;
        }

        public async Task Handle(ConnectionRejectedEvent notification, CancellationToken cancellationToken)
        {
            // TODO: [TASK-A] Load the Patient aggregate via IPatientRepository and call patient.RemoveConnectedDoctor(notification.DoctorId) if present, then persist.
            // Send notification to patient about rejection 
            
            
            await _notificationService.NotifyPatientOfRejection(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
