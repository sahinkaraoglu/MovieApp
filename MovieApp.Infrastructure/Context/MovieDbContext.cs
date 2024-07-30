using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entity;

namespace MovieApp.Infrastructure.Context
{
    public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {

    }
}
