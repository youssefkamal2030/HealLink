using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HealLink.Domain.DomainEvents
{
    public record ConnectionRequestCreatedEvent(Guid RequestId , Guid DoctorId, Guid PatientId):INotification;


}
