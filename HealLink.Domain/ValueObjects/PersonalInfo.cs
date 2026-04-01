using System;
using HealLink.Domain.Enums;

namespace HealLink.Domain.ValueObjects
{
    // TODO: [DDD] PersonalInfo is missing Equals/GetHashCode overrides — value objects must implement structural equality, not reference equality.
    // TODO: [DOMAIN-NEXT] Implement Equals/GetHashCode: override bool Equals(object obj) comparing FullName, Gender, and Nationality, override int GetHashCode() using HashCode.Combine(FullName, Gender, Nationality). Follow the same pattern as Address.cs.
    public class PersonalInfo
    {
        public string FullName { get; private set; }
        public string Gender { get; private set; }
        public string Nationality { get; private set; }

        public PersonalInfo(string fullName, string gender, string nationality)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
            Nationality = nationality ?? throw new ArgumentNullException(nameof(nationality));
        }

    }
} 