namespace Lab5.UserManager.App.Services;

public interface ISubject
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void Notify(string message);
}