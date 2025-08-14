using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Auth
{
   public record RegisterRequest(string username, string Password , string Email, string Role, string? Idfront, string? Idback);
}
