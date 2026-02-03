using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab5.UserManager.App.ViewModels;

/// <summary>
/// Base class for ViewModels implementing INotifyPropertyChanged.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
