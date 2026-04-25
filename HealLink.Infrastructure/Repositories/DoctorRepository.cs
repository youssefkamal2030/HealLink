using System;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    // ToDo: the rest of the generic IRepository<T> methods (GetAllAsync, DeleteAsync) still need implementing.
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealLinkDbContext _context;

        public DoctorRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public Task<Doctor> AddAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> GetByDoctorId(Guid doctorId)
        {
            if (doctorId == Guid.Empty)
                throw new ArgumentException("Doctor ID cannot be empty.", nameof(doctorId));

            return await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Address)
                .Include(d => d.PatientConnections)
                    .ThenInclude(c => c.Patient)
                .FirstOrDefaultAsync(d => d.Id == doctorId);
        }

        public Task UpdateAsync(Doctor doctor)
        {
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));

            _context.Doctors.Update(doctor);

            if (doctor.User != null)
                _context.Users.Update(doctor.User);

            foreach (var connection in doctor.PatientConnections)
                _context.DoctorPatientConnections.Update(connection);

            return Task.CompletedTask;
        }

        public async Task<(List<Doctor> Doctors, int TotalCount)> SearchDoctorsAsync(
            string? searchTerm,
            string? specialization,
            string? city,
            string? country,
            bool? isAvailableForChat,
            bool? isApprovedOnly,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Address)
                .Include(d => d.PersonalInfo)
                .AsQueryable();

            // Apply approval filter (default to approved only)
            if (isApprovedOnly.GetValueOrDefault(true))
            {
                query = query.Where(d => d.IsApproved);
            }

            // Apply search term filter (searches in name, email, specialization, workplace)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(d =>
                    (d.PersonalInfo != null && d.PersonalInfo.FullName.ToLower().Contains(lowerSearchTerm)) ||
                    d.User.Email.ToLower().Contains(lowerSearchTerm) ||
                    (d.Specialization != null && d.Specialization.ToLower().Contains(lowerSearchTerm)) ||
                    (d.CurrentWorkplace != null && d.CurrentWorkplace.ToLower().Contains(lowerSearchTerm)));
            }

            // Apply specialization filter
            if (!string.IsNullOrWhiteSpace(specialization))
            {
                var lowerSpecialization = specialization.ToLower();
                query = query.Where(d => d.Specialization != null && d.Specialization.ToLower().Contains(lowerSpecialization));
            }

            // Apply city filter
            if (!string.IsNullOrWhiteSpace(city))
            {
                var lowerCity = city.ToLower();
                query = query.Where(d => d.Address != null && d.Address.City.ToLower().Contains(lowerCity));
            }

            // Apply country filter
            if (!string.IsNullOrWhiteSpace(country))
            {
                var lowerCountry = country.ToLower();
                query = query.Where(d => d.Address != null && d.Address.Country.ToLower().Contains(lowerCountry));
            }

            // Apply availability filter
            if (isAvailableForChat.HasValue)
            {
                query = query.Where(d => d.IsAvailableForChat == isAvailableForChat.Value);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination and ordering
            var doctors = await query
                .OrderBy(d => d.PersonalInfo != null ? d.PersonalInfo.FullName : d.User.Email)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (doctors, totalCount);
        }
    }
}
