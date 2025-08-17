using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;

namespace HealLink.Infrastructure.Services
{
    public class ConnectionService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepo;

        public ConnectionService()
        {
            
        }
        public async Task SendConnectionRequest(Guid patientId, Guid doctorId)
        {
            var patientAggregate = await _patientRepo.GetAggregateByPatientId(patientId);
            var doctorAggregate = await _doctorRepo.GetAggregateByDoctorId(doctorId);
            if (!doctorAggregate.Doctor.IsApproved) throw new InvalidOperationException("Doctor not approved.");

            var connection = new DoctorPatientConnection(doctorId, patientId);
            doctorAggregate.AddConnection(connection);

            await _doctorRepo.UpdateAggregate(doctorAggregate);

            //await _notificationService.NotifyDoctorOfPendingRequest(doctorId, patientId, connection.Id);
        }
    }
}
