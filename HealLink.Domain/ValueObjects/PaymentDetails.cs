using HealLink.Domain.Enums;

namespace HealLink.Domain.ValueObjects
{
    public class PaymentDetails
    {
        public decimal Amount { get; }
        public Currency Currency { get; }
        public PaymentMethod PaymentMethod { get; }

        public PaymentDetails(decimal amount, Currency currency, PaymentMethod paymentMethod)
        {
            Amount = amount;
            Currency = currency;
            PaymentMethod = paymentMethod;
        }

        public override bool Equals(object obj)
        {
            if (obj is not PaymentDetails other) return false;
            return Amount == other.Amount && Currency == other.Currency && PaymentMethod == other.PaymentMethod;
        }

        public override int GetHashCode() => HashCode.Combine(Amount, Currency, PaymentMethod);
    }
} 