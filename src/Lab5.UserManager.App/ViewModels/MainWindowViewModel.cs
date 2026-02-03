using System.Collections.ObjectModel;
using Lab5.UserManager.App.Models;
using Lab5.UserManager.App.Services;

namespace Lab5.UserManager.App.ViewModels;

/// <summary>
/// ViewModel for the main window.
/// </summary>
public sealed class MainWindowViewModel : ViewModelBase
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;
    private readonly NotificationService _notificationService;
    private string _notificationMessage = string.Empty;

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<User> Users { get; } = new();

    public void AddUser()
    {
        var user = new User(FirstName, LastName, Email);
        Users.Add(user);

        ClearInputs();
    }

    private void ClearInputs()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }
    
    public string NotificationMessage
    {
        get => _notificationMessage;
        private set
        {
            _notificationMessage = value;
            OnPropertyChanged();
        }
    }
}
