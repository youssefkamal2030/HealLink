using healLink.Application.Common.Models;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> GetByDoctorId(Guid doctorId);
        Task UpdateAsync(Doctor doctor);
        Task<(List<Doctor> Doctors, int TotalCount)> SearchDoctorsAsync(
            string? searchTerm,
            string? specialization,
            string? city,
            string? country,
            bool? isAvailableForChat,
            bool? isApprovedOnly,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);
        Task<(List<Doctor> Doctors, int TotalCount)> GetPendingDoctorsAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
