using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Commands.Connections
{
    public record CreateConnectionRequestCommand(
        Guid DoctorId,
        Guid PatientId
    );


}
