using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Lab5.UserManager.App.Models;
using Lab5.UserManager.App.Services;

namespace Lab5.UserManager.App.ViewModels;

/// <summary>
/// ViewModel for the main window.
/// </summary>
public sealed class MainWindowViewModel : ViewModelBase, IObserver
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;
    private string _searchText = string.Empty;
    private readonly NotificationService _notificationService;
    private string _notificationMessage = string.Empty;
    private readonly Stack<string> _errorStack = new();
    private string _errorLog = string.Empty;
    private string _userCountSummary = string.Empty;
    
    // We keep the original list to support filtering
    private readonly List<User> _allUsers = new();

    public MainWindowViewModel()
    {
        _notificationService = new NotificationService();
        _notificationService.Subscribe(this);
        
        AddUserCommand = new RelayCommand(_ => AddUser());
        SearchUserCommand = new RelayCommand(_ => SearchUser());
        SortAscendingCommand = new RelayCommand(_ => SortUsers(true));
        SortDescendingCommand = new RelayCommand(_ => SortUsers(false));
        ShowErrorsCommand = new RelayCommand(_ => ShowErrors());
        
        UpdateUserCount();
    }

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
    
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<User> Users { get; } = new();

    public ICommand AddUserCommand { get; }
    public ICommand SearchUserCommand { get; }
    public ICommand SortAscendingCommand { get; }
    public ICommand SortDescendingCommand { get; }
    public ICommand ShowErrorsCommand { get; }

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
            
            if (!Email.Contains("@"))
            {
                throw new InvalidOperationException("Invalid email format.");
            }

            var newUser = new User(FirstName, LastName, Email);
            _allUsers.Add(newUser);
            
            // Refresh the list
            RefreshUsersList(_allUsers);

            _notificationService.Notify($"User added: {FirstName} {LastName}");
            
            ClearInputs();
            UpdateUserCount();
        }
        catch (Exception ex)
        {
            HandleError(ex);
        }
    }
    
    public void SearchUser()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                RefreshUsersList(_allUsers);
                _notificationService.Notify("Search cleared.");
                return;
            }

            var filtered = _allUsers.Where(u => u.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            RefreshUsersList(filtered);
            
            _notificationService.Notify($"Search executed for: {SearchText}");
        }
        catch (Exception ex)
        {
            HandleError(ex);
        }
    }
    
    public void SortUsers(bool ascending)
    {
        try
        {
            var sorted = ascending 
                ? Users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToList()
                : Users.OrderByDescending(u => u.LastName).ThenByDescending(u => u.FirstName).ToList();
            
            RefreshUsersList(sorted);
            
            _notificationService.Notify(ascending ? "Sorted Ascending" : "Sorted Descending");
        }
        catch (Exception ex)
        {
            HandleError(ex);
        }
    }

    private void RefreshUsersList(IEnumerable<User> users)
    {
        Users.Clear();
        foreach (var user in users)
        {
            Users.Add(user);
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
    
    public string UserCountSummary
    {
        get => _userCountSummary;
        private set
        {
            _userCountSummary = value;
            OnPropertyChanged();
        }
    }
    
    private void UpdateUserCount()
    {
        UserCountSummary = $"Total Users: {_allUsers.Count}";
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

    public void Update(string message)
    {
        NotificationMessage = message;
    }
}
