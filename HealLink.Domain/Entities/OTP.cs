using HealLink.Domain.Base;
using System;

namespace HealLink.Domain.Entities;


public class OTP : AggregateRoot
{
   
    public string Code { get; private set; } = string.Empty;
    public DateTime ExpiryTime { get; private set; }

    public Guid UserId { get; private set; }

    public bool IsUsed { get; private set; } = false;
    public OTP(){ }
    private OTP(string code, DateTime expiryTime, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("OTP code cannot be null or empty", nameof(code));
        if (expiryTime <= DateTime.UtcNow)
            throw new ArgumentException("Expiry time must be in the future", nameof(expiryTime));
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty", nameof(userId));
            
        Code = code;
        ExpiryTime = expiryTime;
        UserId = userId;
    }
    
    internal static OTP Generate(Guid userId, int length = 6, int expiryMinutes = 5)
    {
        var code = new Random().Next(0, 1000000).ToString("D" + length);
        var expiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes);
        return new OTP(code, expiryTime, userId);
    }
    public bool IsExpired() => DateTime.UtcNow >= ExpiryTime;
    
    /// <summary>
    /// Marks this OTP as used after successful verification.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if OTP is already used or expired.</exception>
    public void MarkAsUsed()
    {
        if (IsUsed)
            throw new InvalidOperationException("OTP is already used");
        if (IsExpired())
            throw new InvalidOperationException("OTP is expired");
        
        IsUsed = true;
        UpdateTimestamp();
    }

    /// <summary>
    /// Revokes this OTP without marking it as verified (e.g., when requesting a new one).
    /// Does not throw if OTP is expired, allowing cleanup of stale codes.
    /// </summary>
    public void Revoke()
    {
        IsUsed = true;
        UpdateTimestamp();
    }
}
