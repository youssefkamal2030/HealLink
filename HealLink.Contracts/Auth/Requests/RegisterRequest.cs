using Microsoft.AspNetCore.Http;

namespace HealLink.Contracts.Auth.Requests
{
   public record RegisterRequest(
       string username, 
       string Password , 
       string Email, 
       string Role,
       string? PracticeLisenceNumber ,
       IFormFile? SyndicateId
       ,string? Specilization);
}
