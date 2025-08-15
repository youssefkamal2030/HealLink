using System;
using HealLink.Contracts.Profile.Responses;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    public record DeleteDoctorProfileCommand(
        Guid DoctorId
    ) : IRequest<DeleteDoctorProfileResponse>;

   
}