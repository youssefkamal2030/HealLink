using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Email
{
    public record ConfirmEmailRequest(
     string Email,
     string Code
 );
}
