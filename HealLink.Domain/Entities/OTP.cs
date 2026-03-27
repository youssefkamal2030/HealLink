using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Entities;

// TODO: [DDD] OTP is an Entity but doesn't extend Entity base class — it has no identity, encapsulation, or domain behavior.
// TODO: [DDD] All properties have public setters, violating encapsulation. State should only change through domain methods.
// TODO: [DDD] Id is int instead of Guid, inconsistent with the rest of the domain model.
// TODO: [DDD] No domain logic (e.g., IsExpired(), Invalidate()) — behavior belongs here, not in application/infrastructure layers.
// TODO: [DDD] No validation in constructor — OTP should enforce invariants (non-empty code, future expiry time).
// TODO: [AGGREGATE-MISSING] OTP has no aggregate. It must be owned by UserAggregate — OTP is only valid in the context of a User. The invariants (BR-AUTH-06: expires after 10 minutes, single-use invalidation) cannot be enforced without a parent aggregate root managing the OTP lifecycle. Add IsExpired() and Invalidate() methods here, and enforce them through UserAggregate.
// TODO: [TOMORROW-2] Make OTP extend Entity base class — removes the need for int Id, gives it Guid Id, CreatedAt, UpdatedAt automatically.
// TODO: [TOMORROW-2] Remove the public int Id field and replace with the inherited Guid Id from Entity.
// TODO: [TOMORROW-2] Make all property setters private — Code, ExpiryTime, UserId should only be set via the constructor.
// TODO: [TOMORROW-2] Add a bool IsUsed { get; private set; } field to track single-use invalidation.
// TODO: [TOMORROW-2] Add IsExpired() method — returns true if DateTime.UtcNow >= ExpiryTime.
// TODO: [TOMORROW-2] Add Invalidate() method — sets IsUsed = true and calls UpdateTimestamp(). Throw InvalidOperationException if already used or expired.
// TODO: [TOMORROW-2] Add constructor validation — throw ArgumentException if code is null/empty, or if expiryTime <= DateTime.UtcNow.
public class OTP
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpiryTime { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
}
