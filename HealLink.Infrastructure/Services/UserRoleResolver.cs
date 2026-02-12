using healLink.Application.Interfaces;
using HealLink.Domain.Enums;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Services;

public class UserRoleResolver : IUserRoleResolver
{
    private readonly HealLinkDbContext _context;

    public UserRoleResolver(HealLinkDbContext context)
    {
        _context = context;
    }

    public async Task<(UserRole role, Guid entityId)?> ResolveUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return null;

        if (user.Role == UserRole.Doctor)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
            if (doctor == null) return null;
            return (UserRole.Doctor, doctor.Id);
        }

        if (user.Role == UserRole.Patient)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
            if (patient == null) return null;
            return (UserRole.Patient, patient.Id);
        }

        return null;
    }
}
