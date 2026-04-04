using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace healLink.Application.Handlers.Connection
{
    public class ConnectionAcceptedHandler : INotificationHandler<ConnectionAcceptedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConnectionAcceptedHandler> _logger;

        public ConnectionAcceptedHandler(
            INotificationService notificationService,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            ILogger<ConnectionAcceptedHandler> logger)
        {
            _notificationService = notificationService;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(ConnectionAcceptedEvent notification, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByPatientId(notification.PatientId);
            if (patient == null)
            {
                _logger.LogWarning("ConnectionAcceptedHandler: Patient {PatientId} not found — skipping ConnectedDoctorIds update.", notification.PatientId);
            }
            else
            {
                patient.AddConnectedDoctor(notification.DoctorId);
                await _patientRepository.UpdateAsync(patient);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            await _notificationService.NotifyPatientOfAcceptance(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
