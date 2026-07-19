using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Doctor;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Doctor
{
    public class RejectDoctorCommandHandler : IRequestHandler<RejectDoctorCommand, Result<bool>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RejectDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByDoctorId(request.DoctorId);
            if (doctor == null)
                return Result<bool>.Failure("Doctor not found.");

            try
            {
                doctor.Reject(request.Reason, request.AdminId);
            }
            catch (InvalidOperationException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

            await _doctorRepository.UpdateAsync(doctor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
