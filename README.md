# Lab5 - User Manager Application (Avalonia UI)

## Project Overview

This project is a desktop application developed as part of the Programming 3 laboratory curriculum. It serves as a practical exercise in implementing the Model-View-ViewModel (MVVM) architectural pattern, LINQ queries, and custom design patterns within the .NET ecosystem using Avalonia UI.

The primary objective of this application is to manage a collection of users while demonstrating proficiency in C# programming, event-driven architecture, and robust error handling mechanisms.

## Features

The application includes the following core functionalities:

- **User Registration**: Allows the entry of First Name, Last Name, and Email address.
- **User Visualization**: Displays a dynamic list of registered users.
- **Search Functionality**: Filters users by email using LINQ queries.
- **Sorting**: Provides options to sort the user list in Ascending or Descending order based on Last Name and First Name.
- **Notification System**: Implements a custom Observer pattern to provide real-time feedback on application actions (e.g., "User added", "Sorted Ascending").
- **Error Handling**: Captures exceptions and stores them in an internal error stack.
- **Error Log**: Displays the history of errors in a Last-In-First-Out (LIFO) order when requested.
- **User Summary**: Shows the total count of registered users in real-time.

## Technologies Used

- **.NET 6**: The underlying framework for the application.
- **Avalonia UI**: The cross-platform XAML-based UI framework.
- **C#**: The primary programming language.
- **MVVM (Model-View-ViewModel)**: The architectural pattern used to separate UI logic from business logic.
- **LINQ (Language Integrated Query)**: Used for efficient data querying, filtering, and sorting.

## Project Structure

The solution is organized into the following components:

- **src/Lab5.UserManager.App**: The main application project.
    - **Models**: Contains data entities (e.g., `User.cs`).
    - **ViewModels**: Contains the presentation logic and state management (e.g., `MainWindowViewModel.cs`).
    - **Views**: Contains the XAML UI definitions (e.g., `MainWindow.axaml`).
    - **Services**: Contains business logic and pattern interfaces (e.g., `NotificationService.cs`, `IObserver.cs`, `ISubject.cs`).
- **src/Lab5.UserManager.Tests**: The unit testing project containing xUnit tests for Models, ViewModels, and Services.

## Cloning the Repository

To set up the project locally, clone the repository using one of the following URLs:

**GitHub:**
```bash
git clone git@github.com:JalaU-Labs/Lab5-UserManager-Avalonia.git
```

**GitLab:**
```bash
git clone git@gitlab.com:jala-university1/cohort-5/ES.CSPR-231.GA.T1.26.M1/SD/labs/lab5/botina.alejandro.git
```

## Running the Application

### Prerequisites
- .NET SDK 6.0 must be installed on your machine.

### Execution Steps

1. Navigate to the solution root directory.
2. Restore the project dependencies:
   ```bash
   dotnet restore
   ```
3. Build the solution:
   ```bash
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run --project src/Lab5.UserManager.App
   ```

## Running Unit Tests

The project includes a comprehensive suite of unit tests to validate business logic, data integrity, and pattern implementation.

To execute the tests, run the following command from the solution root:

```bash
dotnet test
```

The tests cover:
- **Models**: Validation of property assignments and string formatting.
- **Services**: Verification of the Observer pattern (subscription, unsubscription, and notification broadcasting).
- **ViewModels**: Validation of command logic, state updates, LINQ filtering, and error handling.

## Error Handling and Notifications

### Custom Observer Pattern
Unlike standard .NET events, this project implements a custom Observer pattern defined by the `ISubject` and `IObserver` interfaces.
- **Subject**: The `NotificationService` acts as the subject, managing a list of subscribers.
- **Observer**: The `MainWindowViewModel` implements `IObserver` to receive and display updates.

### Error Stack
Errors are managed using a `Stack<string>` data structure. This ensures that the most recent errors are retrieved first (LIFO). Errors are not displayed immediately to avoid interrupting the user flow; instead, they are logged and can be viewed by clicking the "Show Errors" button.

## Academic Notes

This project satisfies the specific requirements of Lab 5 by:
1. **Avoiding Framework Magic**: Implementing the Observer pattern manually instead of relying solely on `INotifyPropertyChanged` or external libraries for messaging.
2. **Data Manipulation**: utilizing LINQ for all search and sort operations instead of simple loops.
3. **Architecture**: Strictly adhering to MVVM principles, ensuring no business logic exists in the code-behind files.

## License

This project is licensed under the MIT License.

**Author**: CodeWithBotinaOficial
