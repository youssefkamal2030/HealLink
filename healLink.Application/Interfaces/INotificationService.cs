using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.DTOs;

namespace HealLink.Application.Interfaces
{
    public interface INotificationService
    {

        Task NotifyDoctorOfPendingRequest(Guid doctorId, DoctorConnectionRequestNotificationData data);
        Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId);
        Task NotifyPatientOfRejection(Guid patientId, Guid doctorId);
        Task NotifyDoctorOfApproval(Guid doctorId);
        Task NotifyDoctorOfRejection(Guid doctorId, string reason);
    }
}
