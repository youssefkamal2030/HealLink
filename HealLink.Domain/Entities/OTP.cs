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
public class OTP
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpiryTime { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
}
