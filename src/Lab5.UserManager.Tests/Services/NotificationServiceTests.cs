using Lab5.UserManager.App.Services;

namespace Lab5.UserManager.Tests.Services;

public class NotificationServiceTests
{
    // Mock observer for testing
    private class TestObserver : IObserver
    {
        public List<string> Messages { get; } = new();

        public void Update(string message)
        {
            Messages.Add(message);
        }
    }

    [Fact]
    public void Subscribe_AddsObserver_ObserverReceivesNotification()
    {
        // Arrange
        var service = new NotificationService();
        var observer = new TestObserver();
        string message = "Test Notification";

        // Act
        service.Subscribe(observer);
        service.Notify(message);

        // Assert
        Assert.Single(observer.Messages);
        Assert.Equal(message, observer.Messages[0]);
    }

    [Fact]
    public void Unsubscribe_RemovesObserver_ObserverDoesNotReceiveNotification()
    {
        // Arrange
        var service = new NotificationService();
        var observer = new TestObserver();
        service.Subscribe(observer);

        // Act
        service.Unsubscribe(observer);
        service.Notify("Message");

        // Assert
        Assert.Empty(observer.Messages);
    }

    [Fact]
    public void Notify_MultipleObservers_AllReceiveNotification()
    {
        // Arrange
        var service = new NotificationService();
        var observer1 = new TestObserver();
        var observer2 = new TestObserver();
        service.Subscribe(observer1);
        service.Subscribe(observer2);
        string message = "Broadcast";

        // Act
        service.Notify(message);

        // Assert
        Assert.Single(observer1.Messages);
        Assert.Equal(message, observer1.Messages[0]);
        Assert.Single(observer2.Messages);
        Assert.Equal(message, observer2.Messages[0]);
    }
}
