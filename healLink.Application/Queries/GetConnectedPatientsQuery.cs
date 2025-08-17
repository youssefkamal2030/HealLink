using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Doctor.Responses;
using MediatR;

namespace healLink.Application.Queries
{
    public record GetConnectedPatientsQuery(Guid DoctorId): IRequest<ConnectedPatientsResponse>;
    
}
