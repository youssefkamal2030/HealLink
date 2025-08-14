using System;

namespace HealLink.Domain.ValueObjects
{
    public class QRCode
    {
        public string Value { get; }
        public DateTime GeneratedAt { get; }

        public QRCode(string value, DateTime generatedAt)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            GeneratedAt = generatedAt;
        }

        public bool IsValid(int validMinutes = 5)
        {
            return DateTime.UtcNow.Subtract(GeneratedAt).TotalMinutes < validMinutes;
        }

        public override bool Equals(object obj)
        {
            if (obj is not QRCode other) return false;
            return Value == other.Value && GeneratedAt == other.GeneratedAt;
        }

        public override int GetHashCode() => HashCode.Combine(Value, GeneratedAt);
    }
} 