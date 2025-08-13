using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace HealLink.Domain.Enums
{
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
