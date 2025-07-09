using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Users
{
    public class Role:SmartEnum<Role>
    {
        public static readonly Role Patient = new Role("Patient", 1);
        public static readonly Role Adnin = new Role("Doctor", 2);
        public static readonly Role manager = new Role("Admin", 3);
        private Role(string name, int value) : base(name, value)
        {
        }
    }
}
