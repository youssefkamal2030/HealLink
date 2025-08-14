using HealLink.Domain.Common;
using ErrorOr;
using BCrypt.Net;

namespace HealLink.Infrastructure.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        public ErrorOr<string> HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Error.Unexpected("Password cannot be empty");
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            return hash;
        }

        public bool IsCorrectPassword(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
                return false;
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
} 