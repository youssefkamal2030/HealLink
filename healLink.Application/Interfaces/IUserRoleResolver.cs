using HealLink.Domain.Enums;

namespace healLink.Application.Interfaces;

public interface IUserRoleResolver
{
    Task<(UserRole role, Guid entityId)?> ResolveUserAsync(Guid userId);
}
