using HealLink.Domain.Base;
using System;

namespace HealLink.Domain.Entities;


public class OTP : Entity
{
   
    public string Code { get; private set; } = string.Empty;
    public DateTime ExpiryTime { get; private set; }

    public Guid UserId { get;  private set; }

    public User? User { get; private set; }
    public bool IsUsed { get; private set; } = false;
    public OTP(){ }
    private OTP(string code, DateTime expiryTime)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("OTP code cannot be null or empty", nameof(code));
        if (expiryTime <= DateTime.UtcNow)
            throw new ArgumentException("Expiry time must be in the future", nameof(expiryTime));
        Code = code;
        ExpiryTime = expiryTime;
       
    }
    internal static OTP Generate(int length = 6, int expiryMinutes = 5)
    {
        var code = new Random().Next(0, 1000000).ToString("D" + length);
        var expiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes);
        return new OTP(code, expiryTime);
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
