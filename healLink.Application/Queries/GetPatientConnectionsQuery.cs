using healLink.Application.Common.Models;
using HealLink.Contracts.Connections.Responses;
using MediatR;

namespace healLink.Application.Queries
{
    public record GetPatientConnectionsQuery(Guid PatientId) : IRequest<Result<ConnectionsListResponse>>;
}
