using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Adapters;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace healLink.Application.Handlers.EventHandlers
{
    public class ConnectionRejectedEventHandler : INotificationHandler<DomainEventNotification<ConnectionRejectedEvent>>
    {
        private readonly INotificationService _notificationService;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConnectionRejectedEventHandler> _logger;

        public ConnectionRejectedEventHandler(
            INotificationService notificationService,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            ILogger<ConnectionRejectedEventHandler> logger)
        {
            _notificationService = notificationService;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<ConnectionRejectedEvent> domainEvent, CancellationToken cancellationToken)
        {
            // TODO: [REFACTOR-P2] Same nested SaveChangesAsync issue as ConnectionAcceptedHandler.
            // The correct fix: dispatch UpdatePatientConnectionCommand(notification.PatientId, notification.DoctorId, ConnectionOperation.Remove)
            // and let that command handler own the save. Remove _unitOfWork injection from this handler entirely.
            var notification = domainEvent.DomainEvent;
            var patient = await _patientRepository.GetByPatientId(notification.PatientId);
            if (patient == null)
            {
                _logger.LogWarning("ConnectionRejectedHandler: Patient {PatientId} not found — skipping ConnectedDoctorIds update.", notification.PatientId);
            }
            else
            {
                // Only remove if the doctor was previously connected — rejection of a pending
                // request means the doctor was never in ConnectedDoctorIds to begin with.
                if (patient.ConnectedDoctorIds.Contains(notification.DoctorId))
                {
                    patient.RemoveConnectedDoctor(notification.DoctorId);
                    await _patientRepository.UpdateAsync(patient);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            await _notificationService.NotifyPatientOfRejection(
                notification.PatientId,
                notification.DoctorId
            );
        }
    }
}
