using System;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    public record DeleteProfileCommand(
        Guid DoctorId
    ) : IRequest<DeleteProfileResponse>;

    public record DeleteProfileResponse(
        string Message,
        bool Success = true
    );
}