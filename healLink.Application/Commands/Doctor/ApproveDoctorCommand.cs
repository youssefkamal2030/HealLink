using System;
using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Doctors
{
    public record ApproveDoctorCommand(Guid DoctorId) : IRequest<Result<bool>>;
}
