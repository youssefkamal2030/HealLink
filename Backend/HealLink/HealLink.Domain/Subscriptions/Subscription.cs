using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Subscriptions
{
    public class Subscription
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

    }
}
