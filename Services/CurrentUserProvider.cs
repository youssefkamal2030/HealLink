using ErrorOr;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Application.Common.Models;
using System.Security.Claims;

namespace HealLink.Api.Services
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ErrorOr<CurrentUser> GetCurrentUser()
        {

            var claims = _httpContextAccessor.HttpContext?.User?.Claims;


            var idClaim = TryGetUserId();
            if (idClaim.IsError)
                return Error.Forbidden();
            var id = idClaim.Value;


            var roleClaim = TryGetUserRole();
            if (roleClaim.IsError)
                return Error.Forbidden();
            var role = roleClaim.Value;



            if (id == Guid.Empty || string.IsNullOrEmpty(role))
            {
                return Error.Unauthorized(description: "User is not authenticated");
            }

            return new CurrentUser(id, role);


        }
        private ErrorOr<string> TryGetUserRole()
        {
            var role = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == "myRole")?.Value;

            if (string.IsNullOrEmpty(role))
                return Error.Forbidden(description: "Missing or invalid role");

            return role;
        }
        private ErrorOr<Guid> TryGetUserId()
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == "id");

            if (idClaim == null || !Guid.TryParse(idClaim.Value, out var userId))
                return Error.Forbidden(description: "Invalid or missing user ID");

            return userId;
        }


    }
}
