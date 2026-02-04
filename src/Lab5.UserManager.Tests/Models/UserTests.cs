using Lab5.UserManager.App.Models;

namespace Lab5.UserManager.Tests.Models;

public class UserTests
{
    [Fact]
    public void Constructor_ValidInputs_SetsPropertiesCorrectly()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        string email = "john.doe@example.com";

        // Act
        var user = new User(firstName, lastName, email);

        // Assert
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(email, user.Email);
    }

    [Fact]
    public void ToString_ReturnsFormattedString()
    {
        // Arrange
        var user = new User("Jane", "Smith", "jane.smith@test.com");
        string expected = "Jane Smith - jane.smith@test.com";

        // Act
        string result = user.ToString();

        // Assert
        Assert.Equal(expected, result);
    }
}
