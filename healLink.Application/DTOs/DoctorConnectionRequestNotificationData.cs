using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.DTOs
{

    public record DoctorConnectionRequestNotificationData(
        Guid RequestId,
        Guid PatientId,
        string PatientName); 
}
