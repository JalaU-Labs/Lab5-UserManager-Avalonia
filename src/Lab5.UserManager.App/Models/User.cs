namespace Lab5.UserManager.App.Models;

/// <summary>
/// Represents a user in the system.
/// </summary>
public sealed class User
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    public User(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} - {Email}";
    }
}
