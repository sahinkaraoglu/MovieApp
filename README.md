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

1. Ensure you have all prerequisites installed
2. Configure SQL Server connection string in `appsettings.json`
3. Configure RabbitMQ connection settings
4. Run database migrations
5. Build and run the application
