using System;

namespace HealLink.Domain.Base
{
   
    // TODO: [DDD] Entity should implement equality based on Id (override Equals/GetHashCode) to support proper entity identity comparison.
    // TODO: [REFACTOR-P1] Implement Equals(object obj): return obj is Entity other && Id == other.Id. Implement GetHashCode(): return Id.GetHashCode(). This enables correct behavior in LINQ Contains(), Distinct(), and dictionary lookups on domain entities.
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

        // for equality, we consider two entities equal if they are of the same type and have the same Id. This is a common practice in DDD to ensure that entity identity is based on the unique identifier rather than reference equality.
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Entity)obj;    
            return Id == other.Id;
        }
        // For GetHashCode, we return the hash code of the Id. This ensures that entities with the same Id will have the same hash code, which is important for collections like HashSet or Dictionary that rely on hash codes for equality checks.
        public override int GetHashCode()
        {
            if (Id == default)
                return base.GetHashCode();

            return Id.GetHashCode();
        }
    }
} 