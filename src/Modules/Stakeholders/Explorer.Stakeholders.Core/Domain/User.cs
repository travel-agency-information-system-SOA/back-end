using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Stakeholders.Core.Domain;

public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }
    public string VerificationToken { get; private set; }

    public User(string username, string password, UserRole role, bool isActive, string verificationToken)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        VerificationToken = verificationToken;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }
}

