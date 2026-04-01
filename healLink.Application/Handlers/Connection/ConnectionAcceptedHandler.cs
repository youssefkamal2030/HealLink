using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class ConnectionAcceptedHandler : INotificationHandler<ConnectionAcceptedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConnectionAcceptedHandler(INotificationService notificationService, IPatientRepository  patientRepository, IUnitOfWork unitOfWork)
        {
            _notificationService = notificationService;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ConnectionAcceptedEvent notification, CancellationToken cancellationToken)
        {
            // Send notification to patient about acceptance
            var patient = await _patientRepository.GetByPatientId(notification.PatientId);
            patient.AddConnectedDoctor(notification.DoctorId);
            _unitOfWork.SaveChangesAsync();

            await _notificationService.NotifyPatientOfAcceptance(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
