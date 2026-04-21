using System;
using HealLink.Domain.Enums;

namespace HealLink.Domain.ValueObjects
{
   
    public class Money
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        private Money() { } // For EF

        public Money(decimal amount, Currency currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            Amount = amount;
            Currency = currency;
        }
        public override bool Equals(object? obj)
        {
           if(obj == null || !(obj is Money)) return false;
           var other = obj as Money;
            return other.Currency == Currency && other.Amount == Amount;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }
    }
} 