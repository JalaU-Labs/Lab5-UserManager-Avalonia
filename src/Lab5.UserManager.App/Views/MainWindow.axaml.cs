using Avalonia.Controls;
using Lab5.UserManager.App.ViewModels;

namespace Lab5.UserManager.App.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void OnAddUserClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.AddUser();
        }
    }
}