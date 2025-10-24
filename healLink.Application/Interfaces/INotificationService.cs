using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Application.Interfaces
{
    public interface INotificationService
    {

        Task NotifyDoctorOfPendingRequest(Guid doctorId, Guid patientId, Guid connectionRequestId);
        Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId);
    }
}
