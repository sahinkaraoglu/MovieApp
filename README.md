
![HomePageFirst](https://github.com/user-attachments/assets/b50a42b0-a37f-432f-b936-c5f6426fc7c7)

![HomePage](https://github.com/user-attachments/assets/a81beef8-19d1-4785-a061-7579d70c71b8)

![Diziler](https://github.com/user-attachments/assets/8ec583f6-d831-4baf-bb80-8bfbebf1a52a)

![Filmler](https://github.com/user-attachments/assets/c4abc1cd-1f76-4003-b170-1e49cac34988)

![Login](https://github.com/user-attachments/assets/1260385e-c0df-4ce5-ba84-8c1173d16bd8)

![Swagger](https://github.com/user-attachments/assets/2c84e97e-447c-4a0e-b400-1bf26e5bc5b9)

# MovieApp - Modern Movie & TV Series Platform

MovieApp is a contemporary web application where users can discover movies and TV series, write comments, and interact with content. The project is built on the .NET 8.0 framework and developed following N-tier Architecture principles.

## Technologies Used

### Backend
- **.NET 8.0**: Core framework
- **Entity Framework Core**: ORM and database operations
- **SQL Server**: Database
- **JWT Authentication**: User authentication
- **RabbitMQ**: Message queue system
- **Redis**: Caching mechanism
- **Identity Framework**: User management

### Frontend
- **ASP.NET MVC**: View structure
- **Bootstrap**: UI framework
- **jQuery**: JavaScript library
- **AJAX**: Asynchronous operations

### API Integrations
- **TMDB API**: Fetching movie and TV series data

## Project Structure
The project embraces Domain Driven Design approach and consists of the following layers:

- **MovieApp.Api**: REST API endpoints
- **MovieApp.Web**: MVC web application
- **MovieApp.Domain**: Domain entities
- **MovieApp.Infrastructure**: Database and external service integrations
- **MovieApp.Application**: Business logic

## Features
- Movie and TV series listing
- User authentication/authorization
- Comment system
- Real-time messaging (RabbitMQ)
- Caching mechanism (Redis)
- Responsive design

## Technical Highlights
- Clean Architecture implementation
- Message queue system for asynchronous operations
- Distributed caching with Redis
- RESTful API design
- Secure authentication using JWT tokens
- Modern UI with responsive design

## Development Practices
- SOLID principles
- Repository pattern
- Dependency injection
- Asynchronous programming
- Clean code principles
