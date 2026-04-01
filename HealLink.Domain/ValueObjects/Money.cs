using System;
using HealLink.Domain.Enums;

namespace HealLink.Domain.ValueObjects
{
    // TODO: [DDD] Money is missing Equals/GetHashCode overrides — value objects must implement structural equality.
    // TODO: [DDD] Money has no validation (e.g., Amount >= 0) — domain invariants should be enforced in the constructor.
    // TODO: [DOMAIN-NEXT] Implement Equals/GetHashCode: override bool Equals(object obj) comparing Amount and Currency, override int GetHashCode() using HashCode.Combine(Amount, Currency). Follow the same pattern as Address.cs and MedicationDosage.cs.
    // TODO: [DOMAIN-NEXT] Add constructor validation: if (amount < 0) throw new ArgumentException("Amount cannot be negative", nameof(amount)).
    // TODO: [DOMAIN-NEXT] After fixing Money, replace Payment.Amount (int) and Subscription.Amount (int) with Money — update their constructors and the SubscriptionAggregate accordingly.
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