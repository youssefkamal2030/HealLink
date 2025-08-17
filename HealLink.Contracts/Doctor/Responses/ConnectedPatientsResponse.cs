using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Profile;

namespace HealLink.Contracts.Doctor.Responses
{
    public record ConnectedPatientsResponse(
          bool Success,
          string Message,
          List<PatientProfileResponse> ConnectedPatients,
          int TotalCount
      );
}
