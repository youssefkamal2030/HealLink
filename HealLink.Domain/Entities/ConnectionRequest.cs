using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Base;

namespace HealLink.Domain.Entities
{
    public class ConnectionRequest : Entity
    {
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public string Status { get; private set; } = "Pending";
        public ConnectionRequest(Guid doctorId, Guid patientId)
        {
            DoctorId = doctorId;
            PatientId = patientId;
        }

    }
}
