using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using HealLink.Contracts.Connections.Responses;
using MediatR;

namespace healLink.Application.Commands.Connections
{
    public record CreateConnectionRequestCommand(
        Guid DoctorId,
        Guid PatientId
    ) :IRequest<Result<CreateConnectionRequestResponse>>;


}
