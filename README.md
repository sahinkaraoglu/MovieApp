# MovieApp

A modern web application for exploring movies and TV series, built with .NET Core 8.0 using clean architecture principles.

![HomePageFirst](https://github.com/user-attachments/assets/b50a42b0-a37f-432f-b936-c5f6426fc7c7)

![HomePage](https://github.com/user-attachments/assets/a81beef8-19d1-4785-a061-7579d70c71b8)

![Diziler](https://github.com/user-attachments/assets/8ec583f6-d831-4baf-bb80-8bfbebf1a52a)

![Filmler](https://github.com/user-attachments/assets/c4abc1cd-1f76-4003-b170-1e49cac34988)

![Login](https://github.com/user-attachments/assets/1260385e-c0df-4ce5-ba84-8c1173d16bd8)

![Swagger](https://github.com/user-attachments/assets/2c84e97e-447c-4a0e-b400-1bf26e5bc5b9)


## Features

- 🎬 Browse popular movies and TV series
- 👥 User authentication and authorization
- 💬 Real-time commenting system using RabbitMQ
- 🎯 Clean Architecture implementation
- 🔒 JWT-based authentication
- 🎨 Modern and responsive UI design
- 📱 Mobile-friendly interface

## Technology Stack

- **Backend:**
  - ASP.NET Core 8.0
  - Entity Framework Core
  - SQL Server
  - RabbitMQ for message queuing
  - JWT Authentication

- **Frontend:**
  - ASP.NET Core MVC
  - Bootstrap 5
  - jQuery
  - Modern CSS with responsive design

## Architecture

The project follows Clean Architecture principles with the following layers:
- MovieApp.Api
- MovieApp.Application
- MovieApp.Domain
- MovieApp.Infrastructure
- MovieApp.Web

## Prerequisites

- .NET 8.0 SDK
- SQL Server
- RabbitMQ
- Visual Studio 2022 or VS Code

## Setup

1. **Prerequisites**

   - Visual Studio 2022 or newer

   - SQL Server

   - RabbitMQ Server
2. **Open Project in Visual Studio**

   - Open MovieApp.sln with Visual Studio

   - Right click on Solution and select "Restore NuGet Packages"

   - Rebuild the Solution
3. **Database Configuration**

   - Update connection string in `MovieApp.Api/appsettings.json`:

   ```json

   "ConnectionStrings": {

     "DefaultConnection": "Server=YOUR_SERVER;Database=MovieApp;Trusted_Connection=True;TrustServerCertificate=True"

4. **Run the Project**

   - Right click on Solution and select "Set Startup Projects"

   - Choose "Multiple startup projects"

   - Set Action to "Start" for both MovieApp.Api and MovieApp.Web

   - Press F5 to start the application



5. **Access**

   - Web Interface: `https://localhost:7242`

   - API Documentation: `https://localhost:7063/swagger`
