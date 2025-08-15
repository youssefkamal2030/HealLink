using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Auth
{
    public record ResetPasswordRequest(string Email,string code, string Token, string NewPassword);
    
}
