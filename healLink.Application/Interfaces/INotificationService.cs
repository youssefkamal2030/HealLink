using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyDoctorOfPendingRequest(Guid doctorId, Guid patientId, Guid connectionId);
        Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId);
        Task NotifyPatientOfRejection(Guid patientId, Guid doctorId);
    }
}
