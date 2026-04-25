using healLink.Application.Common.Models;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    //Todo : this should inherit from the generic repository interface to reduce code duplication 
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
    }
}
