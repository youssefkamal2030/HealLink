using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] This class is an infrastructure/persistence concern (event sourcing outbox), not a domain concept.
    // TODO: [DDD] It should live in HealLink.Infrastructure, not in the Domain layer.
    // TODO: [DDD] Naming conflicts with the domain concept of IDomainEvent — rename to OutboxMessage or DomainEventOutbox.
    // TODO: [DDD] All properties have public setters — this is a data bag, not a domain object.
    // TODO: [REFACTOR-P1] Move this entire file to HealLink.Infrastructure/Persistence/OutboxMessage.cs and rename the class to OutboxMessage. Update any references. If this class is not currently used anywhere (check with a codebase search), simply delete it.
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
