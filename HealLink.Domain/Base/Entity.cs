using System;

namespace HealLink.Domain.Base
{
    // TODO: [DDD] Entity.UpdateTimestamp() is public — infrastructure/persistence concerns should not be exposed as public domain API.
    // TODO: [DDD] CreatedAt and UpdatedAt are protected set, but UpdateTimestamp() bypasses encapsulation by being callable from outside the aggregate boundary.
    // TODO: [DDD] Entity should implement equality based on Id (override Equals/GetHashCode) to support proper entity identity comparison.
    // TODO: [TOMORROW-4] Change UpdateTimestamp() visibility from public to protected internal — this prevents application and infrastructure layers from calling it directly on entities, enforcing that timestamp updates only happen through domain methods inside the aggregate boundary.
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
} 