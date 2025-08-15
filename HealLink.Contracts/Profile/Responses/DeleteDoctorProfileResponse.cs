using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Profile.Responses
{
    public record DeleteDoctorProfileResponse(
         string Message,
         bool Success = true
     );
}
