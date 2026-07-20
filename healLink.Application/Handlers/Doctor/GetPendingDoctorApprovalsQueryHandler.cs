using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries.Doctor;
using healLink.Application.Repositories;
using HealLink.Contracts.Doctor.Responses;
using MediatR;

namespace healLink.Application.Handlers.Doctor
{
    public class GetPendingDoctorApprovalsQueryHandler : IRequestHandler<GetPendingDoctorApprovalsQuery, Result<PaginatedDoctorsResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;
        public GetPendingDoctorApprovalsQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public Task<Result<PaginatedDoctorsResponse>> Handle(GetPendingDoctorApprovalsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
