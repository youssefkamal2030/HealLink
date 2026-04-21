using System;
using HealLink.Domain.Enums;

namespace HealLink.Domain.ValueObjects
{
    public class PersonalInfo
    {
        public string FullName { get; private set; }
        public string Gender { get; private set; }
        public string Nationality { get; private set; }

        private PersonalInfo() { } // For EF

        public PersonalInfo(string fullName, string gender, string nationality)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
            Nationality = nationality ?? throw new ArgumentNullException(nameof(nationality));
        }

        public override bool Equals(object obj)
        {
            if (obj is not PersonalInfo other) return false;
            return FullName == other.FullName && Gender == other.Gender && Nationality == other.Nationality;
        }

        public override int GetHashCode() => HashCode.Combine(FullName, Gender, Nationality);
    }
} 