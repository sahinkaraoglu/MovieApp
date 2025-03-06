<div  align="center">
<h1> 🎬 MovieApp  </h1>
</div>

<div align="center">
    <h3>A Modern Movie and TV Series Discovery Platform</h3>
    <p>Built with .NET Core 8.0 following Clean Architecture principles</p>

[![.NET Core](https://img.shields.io/badge/-.NET%20Core%208.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/-EF%20Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://docs.microsoft.com/ef/core/)
[![Docker](https://img.shields.io/badge/-Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/)
[![RabbitMQ](https://img.shields.io/badge/-RabbitMQ-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)](https://www.rabbitmq.com/)


</div>

<div align="center">
    <table>
        <tr>
            <td>
                <img src="https://github.com/user-attachments/assets/a81beef8-19d1-4785-a061-7579d70c71b8" alt="HomePage" width="400"/>
            </td>
            <td>
                <img src="https://github.com/user-attachments/assets/80b7b03d-6aa4-4b3e-9fe4-6a02dd9dbd52" alt="HomePage2" width="400"/>
            </td>
        </tr>
        <tr>
            <td>
                <img src="https://github.com/user-attachments/assets/8ec583f6-d831-4baf-bb80-8bfbebf1a52a" alt="Diziler" width="400"/>
            </td>
            <td>
                <img src="https://github.com/user-attachments/assets/c4abc1cd-1f76-4003-b170-1e49cac34988" alt="Filmler" width="400"/>
            </td>
        </tr>
    </table>
</div>







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
  - Dockerized RabbitMQ for message queuing
  - Dockerized Redis for caching
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

## Setup

1. **Prerequisites**
   - Visual Studio 2022 or newer
   - SQL Server
   - Docker (for RabbitMQ and Redis)
     
2. **Database Configuration**

   - Update connection string in `MovieApp.Api/appsettings.json`:

   ```json

   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=MovieApp;Trusted_Connection=True;TrustServerCertificate=True"

3. **Access**
   - Web: `https://localhost:7242`
   - API Docs: `https://localhost:7063/swagger`
  
