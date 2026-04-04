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
    public class GetPatientConnectionsQueryHandler : IRequestHandler<GetPatientConnectionsQuery, Result<ConnectionsListResponse>>
    {
        private readonly IDoctorPatientConnectionRepository _DoctorPatientConnectionRepository;

        public GetPatientConnectionsQueryHandler(IDoctorPatientConnectionRepository DoctorPatientConnectionRepository)
        {
            _DoctorPatientConnectionRepository = DoctorPatientConnectionRepository;
        }

        public async Task<Result<ConnectionsListResponse>> Handle(GetPatientConnectionsQuery request, CancellationToken cancellationToken)
        {
            var connections = await _DoctorPatientConnectionRepository.GetConnectionsForPatientAsync(request.PatientId);

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
                Message: "Patient connections retrieved successfully.",
                Connections: connectionResponses,
                TotalCount: connectionResponses.Count
            );

            return Result<ConnectionsListResponse>.Success(response);
        }
    }
}
