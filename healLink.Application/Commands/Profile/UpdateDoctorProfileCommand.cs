using System;
using healLink.Application.Handlers.Profile;
using HealLink.Domain.ValueObjects;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    // this one giant updating command is not ideal, but it works for now. In the future, we might want to break this down into smaller commands for better separation of concerns and maintainability.
    // we should in the feature identifiy the prorties that change togther depending on business rules and make seperate commands for them, for example, if we have a command to update the personal info, we should not include the address in that command, and vice versa. but for now, we will keep it simple and have one command to update the whole profile.
    public record UpdateDoctorProfileCommand(
        Guid DoctorId,
        PersonalInfo? PersonalInfo,
        Address? Address,
        string? Specialization,
        string? CurrentWorkplace,
        bool? IsAvailableForChat = null
    ) : IRequest<UpdateProfileResponse>;
}