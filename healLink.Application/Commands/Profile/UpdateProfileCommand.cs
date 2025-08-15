using System;
using HealLink.Domain.ValueObjects;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    public record UpdateProfileCommand(
        Guid DoctorId,
        PersonalInfo PersonalInfo,
        Address Address,
        string Specialization,
        string CurrentWorkplace,
        string Phone,
        bool? IsAvailableForChat = null
    ) : IRequest<UpdateProfileResponse>;

    public record UpdateProfileResponse(
        string Message,
        bool Success = true
    );
}
