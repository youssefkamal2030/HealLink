using healLink.Application.Common.Models;
using HealLink.Contracts.Connections.Responses;
using MediatR;

namespace healLink.Application.Queries
{
    public record GetDoctorConnectionsQuery(Guid DoctorId) : IRequest<Result<ConnectionsListResponse>>;
}
