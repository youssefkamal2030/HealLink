using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace HealLink.Domain.Enums
{
    // TODO: [DDD] Role SmartEnum has incorrect member names — 'Adnin' is a typo for 'Admin', and its value maps to "Doctor" (wrong name/value pairing).
    // TODO: [DDD] 'manager' member is lowercase — naming convention should be PascalCase (Manager).
    // TODO: [DDD] This Role SmartEnum duplicates the UserRole enum in DomainEnums.cs — consolidate into a single role definition to avoid divergence.
    public class Role : SmartEnum<Role>
    {
        public static readonly Role Patient = new Role("Patient", 1);
        public static readonly Role Adnin = new Role("Doctor", 2);
        public static readonly Role manager = new Role("Admin", 3);
        private Role(string name, int value) : base(name, value)
        {
        }
    }
}
