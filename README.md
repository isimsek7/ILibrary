# ILibrary

## Summary
ILibrary is a web application designed to manage books and authors efficiently. It provides users with the ability to create, read, update, and delete (CRUD) book and author records, while also offering a user-friendly interface for browsing and managing a library of literature.

## Features
- User authentication for secure access.
- Author management: Create, edit, and delete author records.
- Book management: Create, edit, and delete book records.
- View detailed information for each author and book.
- Responsive design for an optimal user experience on various devices.

## Technologies Used
- **ASP.NET Core MVC**: For building the web application.
- **Entity Framework Core**: For data access and management.
- **Bootstrap**: For responsive and modern UI design.
- **HTML/CSS/JavaScript**: For front-end development.
- **SQL Server**: As the database to store authors and books information.

## How It Works
ILibrary allows users to register and log in to manage their library. Once logged in, users can navigate to the author and book sections. The application supports CRUD operations, allowing users to add new authors and books, edit existing records, and delete those that are no longer needed.

- **User Authentication**: Users can register and log in to their accounts, storing their information securely.
- **Author Management**: Users can create new authors, input their details, and view a list of all authors with options for editing and deleting.
- **Book Management**: Users can add new books, including details like title, genre, and publication date, as well as associate them with authors.

## Testing
Although extensive testing was planned, the application has undergone basic testing to ensure all features function as intended. Manual testing included:
- Verifying user authentication processes.
- Ensuring all CRUD operations for authors and books work without errors.
- Checking responsiveness and usability across various devices.

## Installation
1. Clone the repository: `git clone <repository-url>`
2. Navigate to the project directory: `cd ILibrary`
3. Restore dependencies: `dotnet restore`
4. Run the application: `dotnet run`

Visit `http://localhost:5000` in your web browser to access the application.
