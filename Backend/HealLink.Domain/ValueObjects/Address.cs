using System;

namespace HealLink.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string PostalCode { get; }

        protected Address() { }

        public Address(string street, string city, string state, string country, string postalCode)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        }

        public override bool Equals(object obj)
        {
            if (obj is not Address other) return false;
            return Street == other.Street && City == other.City && State == other.State && Country == other.Country && PostalCode == other.PostalCode;
        }

        public override int GetHashCode() => HashCode.Combine(Street, City, State, Country, PostalCode);
    }
} 