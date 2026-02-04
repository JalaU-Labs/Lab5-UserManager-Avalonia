using Lab5.UserManager.App.ViewModels;

namespace Lab5.UserManager.Tests.ViewModels;

public class MainWindowViewModelTests
{
    [Fact]
    public void AddUser_ValidInput_AddsUserToList()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        viewModel.FirstName = "Alice";
        viewModel.LastName = "Wonderland";
        viewModel.Email = "alice@example.com";

        // Act
        viewModel.AddUser();

        // Assert
        Assert.Single(viewModel.Users);
        var user = viewModel.Users.First();
        Assert.Equal("Alice", user.FirstName);
        Assert.Equal("Wonderland", user.LastName);
        Assert.Equal("alice@example.com", user.Email);
    }

    [Fact]
    public void AddUser_ValidInput_ClearsInputs()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        viewModel.FirstName = "Bob";
        viewModel.LastName = "Builder";
        viewModel.Email = "bob@example.com";

        // Act
        viewModel.AddUser();

        // Assert
        Assert.Empty(viewModel.FirstName);
        Assert.Empty(viewModel.LastName);
        Assert.Empty(viewModel.Email);
    }

    [Fact]
    public void AddUser_MissingFields_DoesNotAddUserAndLogsError()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        viewModel.FirstName = "Charlie";
        // LastName is missing
        viewModel.Email = "charlie@example.com";

        // Act
        viewModel.AddUser();

        // Assert
        Assert.Empty(viewModel.Users);
        
        // Verify error handling
        viewModel.ShowErrors();
        Assert.Contains("All user fields are required", viewModel.ErrorLog);
    }

    [Fact]
    public void AddUser_InvalidEmail_DoesNotAddUserAndLogsError()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        viewModel.FirstName = "David";
        viewModel.LastName = "Bowie";
        viewModel.Email = "invalid-email"; // No @ symbol

        // Act
        viewModel.AddUser();

        // Assert
        Assert.Empty(viewModel.Users);
        
        // Verify error handling
        viewModel.ShowErrors();
        Assert.Contains("Invalid email format", viewModel.ErrorLog);
    }

    [Fact]
    public void SearchUser_ByEmail_FiltersList()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        
        // Add users directly via public properties and method
        viewModel.FirstName = "User1"; viewModel.LastName = "Test"; viewModel.Email = "one@test.com";
        viewModel.AddUser();
        
        viewModel.FirstName = "User2"; viewModel.LastName = "Test"; viewModel.Email = "two@test.com";
        viewModel.AddUser();
        
        viewModel.FirstName = "User3"; viewModel.LastName = "Test"; viewModel.Email = "three@other.com";
        viewModel.AddUser();

        // Act
        viewModel.SearchText = "test.com";
        viewModel.SearchUser();

        // Assert
        Assert.Equal(2, viewModel.Users.Count);
        Assert.Contains(viewModel.Users, u => u.Email == "one@test.com");
        Assert.Contains(viewModel.Users, u => u.Email == "two@test.com");
        Assert.DoesNotContain(viewModel.Users, u => u.Email == "three@other.com");
    }

    [Fact]
    public void SearchUser_EmptySearch_ReturnsAllUsers()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        
        viewModel.FirstName = "User1"; viewModel.LastName = "Test"; viewModel.Email = "one@test.com";
        viewModel.AddUser();
        
        viewModel.FirstName = "User2"; viewModel.LastName = "Test"; viewModel.Email = "two@test.com";
        viewModel.AddUser();

        // Filter first
        viewModel.SearchText = "one";
        viewModel.SearchUser();
        Assert.Single(viewModel.Users);

        // Act - Clear search
        viewModel.SearchText = "";
        viewModel.SearchUser();

        // Assert
        Assert.Equal(2, viewModel.Users.Count);
    }

    [Fact]
    public void SortUsers_Ascending_SortsByLastNameThenFirstName()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        
        viewModel.FirstName = "B"; viewModel.LastName = "Z"; viewModel.Email = "1@t.com";
        viewModel.AddUser();
        
        viewModel.FirstName = "A"; viewModel.LastName = "Z"; viewModel.Email = "2@t.com";
        viewModel.AddUser();
        
        viewModel.FirstName = "C"; viewModel.LastName = "A"; viewModel.Email = "3@t.com";
        viewModel.AddUser();

        // Act
        viewModel.SortUsers(true); // Ascending

        // Assert
        Assert.Equal("A", viewModel.Users[0].LastName); // C A
        Assert.Equal("Z", viewModel.Users[1].LastName); // A Z (First name A comes before B)
        Assert.Equal("A", viewModel.Users[1].FirstName);
        Assert.Equal("Z", viewModel.Users[2].LastName); // B Z
        Assert.Equal("B", viewModel.Users[2].FirstName);
    }

    [Fact]
    public void SortUsers_Descending_SortsByLastNameThenFirstName()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        
        viewModel.FirstName = "B"; viewModel.LastName = "Z"; viewModel.Email = "1@t.com";
        viewModel.AddUser();
        
        viewModel.FirstName = "A"; viewModel.LastName = "Z"; viewModel.Email = "2@t.com";
        viewModel.AddUser();
        
        viewModel.FirstName = "C"; viewModel.LastName = "A"; viewModel.Email = "3@t.com";
        viewModel.AddUser();

        // Act
        viewModel.SortUsers(false); // Descending

        // Assert
        Assert.Equal("Z", viewModel.Users[0].LastName); // B Z (First name B comes after A)
        Assert.Equal("B", viewModel.Users[0].FirstName);
        Assert.Equal("Z", viewModel.Users[1].LastName); // A Z
        Assert.Equal("A", viewModel.Users[1].FirstName);
        Assert.Equal("A", viewModel.Users[2].LastName); // C A
    }

    [Fact]
    public void Notification_TriggeredOnAddUser()
    {
        // Arrange
        var viewModel = new MainWindowViewModel();
        viewModel.FirstName = "Notify";
        viewModel.LastName = "Me";
        viewModel.Email = "notify@me.com";

        // Act
        viewModel.AddUser();

        // Assert
        Assert.Contains("User added", viewModel.NotificationMessage);
    }
}
