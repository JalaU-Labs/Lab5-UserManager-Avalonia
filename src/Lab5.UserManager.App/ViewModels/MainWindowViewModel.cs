using System;
using System.Collections.Generic;
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
    private readonly Stack<string> _errorStack = new();
    private string _errorLog = string.Empty;

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
        try
        {
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Email))
            {
                throw new InvalidOperationException("All user fields are required.");
            }

            Users.Add(new User(FirstName, LastName, Email));

            _notificationService.Notify($"User added: {FirstName} {LastName}");
        }
        catch (Exception ex)
        {
            HandleError(ex);
        }
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
    
    public string ErrorLog
    {
        get => _errorLog;
        private set
        {
            _errorLog = value;
            OnPropertyChanged();
        }
    }
    
    private void HandleError(Exception ex)
    {
        var errorMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.Message}";
        _errorStack.Push(errorMessage);

        _notificationService.Notify("An error occurred. Check error log.");
    }
    
    public void ShowErrors()
    {
        ErrorLog = _errorStack.Count == 0
            ? "No errors recorded."
            : string.Join(Environment.NewLine, _errorStack);
    }
}
