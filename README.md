# Project Readme

This repository contains a web application project developed for a company. The project implements user authentication and profile management functionalities.

## Features

- User Registration: Users can create a new account by providing their username, email, and password.
- User Login: Registered users can log in to the application using their email and password.
- Profile Management: Users can view, add, edit, and delete their profile information, including name, email, address, state, city, pin code, and telephone.

## Technologies Used

- ASP.NET Core: The web application is developed using the ASP.NET Core framework.
- C#: The application logic is implemented using the C# programming language.
- HTML: The views are created using HTML for the user interface.
- CSS: The styling of the views is done using CSS for a better user experience.
- Entity Framework Core: The project uses Entity Framework Core as the ORM (Object-Relational Mapping) tool for database operations.
- Authentication and Authorization: The application utilizes ASP.NET Core's authentication and authorization features to secure user access and protect sensitive information.
- AutoMapper: AutoMapper is used for object-to-object mapping between the DTOs (Data Transfer Objects) and models.

## Prerequisites

To run the project locally, ensure you have the following prerequisites installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 5.0 or higher)
- [Visual Studio](https://visualstudio.microsoft.com/) or any other code editor of your choice

## Getting Started

1. Clone the repository to your local machine.
2. Open the project in Visual Studio or your preferred code editor.
3. Build the project to restore the NuGet packages and compile the code.
4. Update the database connection string in the `appsettings.json` file with your own database credentials.
5. Run the database migrations by opening a terminal in the project root directory and executing the following command:
   ```
   dotnet ef database update
   ```
   This will create the necessary database schema.
6. Start the application by running the following command in the terminal:
   ```
   dotnet run
   ```
   The application will start running on `https://localhost:5001`.

## Usage

### User Registration

1. Open your web browser and navigate to `https://localhost:5001/User/Register`.
2. Fill in the required fields: username, email, and password.
3. Click the "Register" button to create a new user account.

### User Login

1. Open your web browser and navigate to `https://localhost:5001/User/Login`.
2. Enter your email and password.
3. Click the "Login" button to authenticate.

### Profile Management

#### Add Profile

1. After logging in, navigate to `https://localhost:5001/User/Profile/Add`.
2. Fill in the profile details: name, email, address, state, city, pin code, and telephone.
3. Click the "Add Profile" button to create a new profile.

#### Edit Profile

1. After logging in, navigate to `https://localhost:5001/User/Profile/Edit`.
2. Update the profile details as desired.
3. Click the "Update Profile" button to save the changes.

#### View Profile

1. After logging in, navigate to `https://localhost:5001/User/Profile`.
2. Your profile details will be displayed if available.
3. If no profile is found, you will have the option to add a profile.

#### Delete Profile

1. After logging in, navigate to `https://localhost:5001/User/Profile`.
2. Click the "Delete Profile" button to remove your profile.

## License

This project is licensed under the [MIT License](LICENSE).
