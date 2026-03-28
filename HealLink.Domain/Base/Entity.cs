using System;

namespace HealLink.Domain.Base
{
   
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
        
        protected void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
} 