using System;

namespace HealLink.Domain.ValueObjects
{
    public class Address
    {
        
        public string City { get; }
      
        public string Country { get; }
      

        protected Address() { }

        public Address( string city,  string country)
        {
        
            City = city ?? throw new ArgumentNullException(nameof(city));
         
            Country = country ?? throw new ArgumentNullException(nameof(country));
      
        }

        public override bool Equals(object obj)
        {
            if (obj is not Address other) return false;
            return    City == other.City  && Country == other.Country ;
        }

        public override int GetHashCode() => HashCode.Combine(City, Country);
    }
} 