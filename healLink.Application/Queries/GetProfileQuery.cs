using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Profile;
using MediatR;

namespace healLink.Application.Queries
{
    public record GetProfileQuery(Guid UserId) : IRequest<ProfileResponse>;
}
