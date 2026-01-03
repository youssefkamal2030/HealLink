using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Connections.Responses;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class GetDoctorConnectionsQueryHandler : IRequestHandler<GetDoctorConnectionsQuery, Result<ConnectionsListResponse>>
    {
        private readonly IConnectionRepository _connectionRepository;

        public GetDoctorConnectionsQueryHandler(IConnectionRepository connectionRepository)
        {
            _connectionRepository = connectionRepository;
        }

        public async Task<Result<ConnectionsListResponse>> Handle(GetDoctorConnectionsQuery request, CancellationToken cancellationToken)
        {
            var connections = await _connectionRepository.GetConnectionsForDoctorAsync(request.DoctorId);

            var connectionResponses = connections.Select(c => new ConnectionResponse(
                Id: c.Id,
                DoctorId: c.DoctorId,
                PatientId: c.PatientId,
                Status: c.Status.ToString(),
                CreatedAt: c.CreatedAt,
                AcceptedAt: c.AcceptedAt
            )).ToList();

            var response = new ConnectionsListResponse(
                Success: true,
                Message: "Doctor connections retrieved successfully.",
                Connections: connectionResponses,
                TotalCount: connectionResponses.Count
            );

            return Result<ConnectionsListResponse>.Success(response);
        }
    }
}
