using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Doctor.Requests
{
    public record GetAllConnectedPatientsRequest(Guid DoctorId);
  
}
