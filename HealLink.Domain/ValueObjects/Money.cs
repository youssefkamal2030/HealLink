using System;
using HealLink.Domain.Enums;

namespace HealLink.Domain.ValueObjects
{
    // TODO: [DDD] Money is missing Equals/GetHashCode overrides — value objects must implement structural equality.
    // TODO: [DDD] Money has no validation (e.g., Amount >= 0) — domain invariants should be enforced in the constructor.
    public class Money
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }
        
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
} 