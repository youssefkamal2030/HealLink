using System;
using healLink.Application.Handlers.Profile;
using HealLink.Domain.ValueObjects;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    public record UpdateDoctorProfileCommand(
        Guid DoctorId,
        PersonalInfo? PersonalInfo,
        Address? Address,
        string? Specialization,
        string? CurrentWorkplace,
        bool? IsAvailableForChat = null
    ) : IRequest<UpdateProfileResponse>;
}