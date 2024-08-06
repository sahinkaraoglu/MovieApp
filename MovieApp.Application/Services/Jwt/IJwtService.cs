using MovieApp.Domain.Entity;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Application.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}
