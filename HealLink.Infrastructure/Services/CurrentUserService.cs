using System.Security.Claims;
using healLink.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HealLink.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
                return Guid.Empty;

            // Try "sub" claim first (standard JWT claim)
            var subClaim = user.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(subClaim) && Guid.TryParse(subClaim, out var subGuid))
                return subGuid;

            // Fall back to NameIdentifier claim
            var nameIdentifierClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(nameIdentifierClaim) && Guid.TryParse(nameIdentifierClaim, out var nameIdGuid))
                return nameIdGuid;

            // No valid claim found
            return Guid.Empty;
        }
    }
}
