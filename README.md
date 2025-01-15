# MovieApp
<div align="center">
    <p align="center">
        <h3>🎬 Modern Movie & TV Series Explorer</h3>
        <p>A modern web application built with .NET Core 8.0 using clean architecture principles.</p>
    </p>
</div>

## 📸 Screenshots
<div>
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
  - RabbitMQ for message queuing
  - Redis for caching
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
   - RabbitMQ Server
   - Redis Server
     
2. **Database Configuration**

   - Update connection string in `MovieApp.Api/appsettings.json`:

   ```json

   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=MovieApp;Trusted_Connection=True;TrustServerCertificate=True"

3. **Access**
   - Web: `https://localhost:7242`
   - API Docs: `https://localhost:7063/swagger`
  



## 🛠 Technology Stack

<details>
<summary><b>Backend Technologies</b></summary>
- **Framework:** ASP.NET Core 8.0
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Caching:** Redis
- **Message Queue:** RabbitMQ
- **Authentication:** JWT
</details>
<details>
<summary><b>Frontend Technologies</b></summary>
- **Framework:** ASP.NET Core MVC
- **UI Framework:** Bootstrap 5
- **JavaScript:** jQuery
- **Styling:** Modern CSS with responsive design
</details>




### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/MovieApp.git
cd MovieApp
```

### 2. Start Docker Containers
```bash
# Start Redis
docker run --name redis-movieapp -p 6379:6379 -d redis

# Start RabbitMQ
docker run --name rabbitmq-movieapp -p 5672:5672 -p 15672:15672 -d rabbitmq:management
```

### 3. Database Configuration
Update connection string in `MovieApp.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MovieApp;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 4. Run the Application
- Web Interface: `https://localhost:7242`
- API Documentation: `https://localhost:7063/swagger`
- RabbitMQ Management: `http://localhost:15672` (guest/guest)

### Docker Commands Reference
```bash
# View running containers
docker ps
# Stop containers
docker stop redis-movieapp rabbitmq-movieapp
# Remove containers
docker rm redis-movieapp rabbitmq-movieapp
# View container logs
docker logs redis-movieapp
docker logs rabbitmq-movieapp
```
