using HealLink.Domain.Common;
using System.Data;

namespace HealLink.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = null!;
    public string NameToShow { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public bool IsVerified { get; private set; } = false;

    private string _passwordHash = null!;
    public Role Role { get; private set; } = Role.Patient;

    private User() { }

    public User(string fullName,string nameToShow, string email, string passwordHash, Role? role = null, Guid? id = null, bool isVerified = false)
    {
        Id = id ?? Guid.NewGuid();
        FullName = fullName;
        NameToShow = nameToShow;
        Email = email;
        _passwordHash = passwordHash;
        Role = role ?? Role.Patient;
        IsVerified = isVerified;
    }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public void ChangeRole(Role newRole)
    {
        Role = newRole;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        _passwordHash = newPasswordHash;
    }
}
