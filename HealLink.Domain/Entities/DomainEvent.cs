using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Entities
{
    public class DomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public string EventType { get; set; }
        public int Version { get; set; }
        public string EventData { get; set; }
        public string Metadata { get; set; }
        public DateTime OccurredOn { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
