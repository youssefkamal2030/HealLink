using HealLink.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Entities;

// DONE: OTP now extends Entity (Guid Id, CreatedAt, UpdatedAt), has private setters, IsUsed flag, IsExpired(), Invalidate(), and constructor validation.
// DONE: Owned by User aggregate — created only via User.RequestOTP().
public class OTP : Entity
{
   
    public string Code { get; private set; } = string.Empty;
    public DateTime ExpiryTime { get; private set; }

    public Guid UserId { get;  private set; }

    public User? User { get; private set; }
    public bool IsUsed { get; private set; } = false;
    public OTP(){ }
    public OTP(string code, DateTime expiryTime, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("OTP code cannot be null or empty", nameof(code));
        if (expiryTime <= DateTime.UtcNow)
            throw new ArgumentException("Expiry time must be in the future", nameof(expiryTime));
        Code = code;
        ExpiryTime = expiryTime;
        UserId = userId;
    }

    public bool IsExpired() => DateTime.UtcNow >= ExpiryTime;
    public void Invalidate()
    {
        if (IsUsed)
            throw new InvalidOperationException("OTP is already used");
        if (IsExpired())
            throw new InvalidOperationException("OTP is expired");
        
        IsUsed = true;
        UpdateTimestamp();
    }
}
