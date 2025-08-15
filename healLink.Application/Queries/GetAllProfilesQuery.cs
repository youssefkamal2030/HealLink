using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Contracts.Profile;
using MediatR;

namespace healLink.Application.Queries
{
    public record GetAllProfilesQuery(
        int Page = 1,
        int PageSize = 20,
        string? SearchTerm = null,
        string? RoleFilter = null
    ) : IRequest<AllProfilesResponse>;
}
