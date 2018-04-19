using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Data
{
    public class ViaCinemaAppContext : IdentityDbContext<User>
    {
        public ViaCinemaAppContext(DbContextOptions<ViaCinemaAppContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<AvailableMovies> AvailableMovies { get; set; }

        public DbSet<Seat> Seat { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<User> User { get; set; }
    }
}