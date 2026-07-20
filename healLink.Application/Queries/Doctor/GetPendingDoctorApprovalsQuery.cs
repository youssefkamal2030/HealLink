using healLink.Application.Common.Models;
using HealLink.Contracts.Doctor.Responses;
using MediatR;

namespace healLink.Application.Queries.Doctor
{
    public record GetPendingDoctorApprovalsQuery(
        int Page = 1,
        int PageSize = 10
    ) : IRequest<Result<PaginatedDoctorsResponse>>;
}
