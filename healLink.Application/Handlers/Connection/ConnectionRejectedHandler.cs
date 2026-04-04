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
    public class ConnectionRejectedHandler : INotificationHandler<ConnectionRejectedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConnectionRejectedHandler> _logger;

        public ConnectionRejectedHandler(
            INotificationService notificationService,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            ILogger<ConnectionRejectedHandler> logger)
        {
            _notificationService = notificationService;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(ConnectionRejectedEvent notification, CancellationToken cancellationToken)
        {
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
