
using Microsoft.AspNetCore.Http;

namespace HealLink.Contracts.Auth
{
   public record RegisterRequest(string username, string Password , string Email, string Role, IFormFile? Idfront, IFormFile? Idback);
}
