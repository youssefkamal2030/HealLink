using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Chat.Requests
{
    public record ChatHistoryRequest(
        Guid UserId1,
        Guid UserId2
    );
  
}
