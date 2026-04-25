using healLink.Application.Common.Models;
using HealLink.Contracts.Search.Responses;
using MediatR;

namespace healLink.Application.Queries
{
    public record SearchDoctorsQuery(
        string? SearchTerm = null,
        string? Specialization = null,
        string? City = null,
        string? Country = null,
        bool? IsAvailableForChat = null,
        bool? IsApprovedOnly = true,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<Result<DoctorSearchResponse>>;
}
