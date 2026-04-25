using healLink.Application.Common.Models;
using HealLink.Contracts.Search.Responses;
using MediatR;

namespace healLink.Application.Queries
{
    public record SearchPatientsQuery(
        string? SearchTerm = null,
        string? City = null,
        string? Country = null,
        bool? HasGuardian = null,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<Result<PatientSearchResponse>>;
}
