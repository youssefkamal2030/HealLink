using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Doctors;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Doctor
{
    public class ApproveDoctorCommandHandler : IRequestHandler<ApproveDoctorCommand, Result<bool>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ApproveDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByDoctorId(request.DoctorId);
            if (doctor == null)
                return Result<bool>.Failure("Doctor not found.");

            if (doctor.IsApproved)
                return Result<bool>.Failure("Doctor is already approved.");

            doctor.Approve(doctor.Id);
            await _doctorRepository.UpdateAsync(doctor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
