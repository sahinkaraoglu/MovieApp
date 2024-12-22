using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entity;
using System.Reflection.Metadata;

namespace MovieApp.Infrastructure.Context
{
    public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<ListMovie> ListMovie { get; set; }
        public DbSet<UserList> UserLists{ get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.UserLists)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }

    }


}
