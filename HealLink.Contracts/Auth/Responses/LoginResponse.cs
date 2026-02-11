using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Auth.Responses
{
   public record LoginResponse(Guid userId, string username,string Email, string token);
    
    
}
