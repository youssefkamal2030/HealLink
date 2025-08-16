using System;
using HealLink.Domain.Enums;

namespace HealLink.Domain.ValueObjects
{
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