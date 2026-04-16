using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<Prescription> AddAsync(Prescription prescription, CancellationToken cancellationToken = default);
        Task<List<Prescription>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<List<Prescription>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
        Task<Prescription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
