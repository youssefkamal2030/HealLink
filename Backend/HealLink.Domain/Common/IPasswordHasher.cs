using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Common
{
    public interface IPasswordHasher
    {

        public ErrorOr<string> HashPassword(string password);
        public bool IsCorrectPassword(string password, string hash);
        

    }
}
