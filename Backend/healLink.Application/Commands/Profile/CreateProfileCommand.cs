using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Enums;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    public record CreateProfileCommand(
        Guid UserId,
        UserRole Role,
        string? FirstName,
        string? LastName,
        string? PhoneNumber,
        string? Address
    ) : IRequest<CreateProfileResponse>;

}
