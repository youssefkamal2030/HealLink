using System;

namespace HealLink.Domain.Base
{
    // TODO: [DDD] Entity.UpdateTimestamp() is public — infrastructure/persistence concerns should not be exposed as public domain API.
    // TODO: [DDD] CreatedAt and UpdatedAt are protected set, but UpdateTimestamp() bypasses encapsulation by being callable from outside the aggregate boundary.
    // TODO: [DDD] Entity should implement equality based on Id (override Equals/GetHashCode) to support proper entity identity comparison.
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