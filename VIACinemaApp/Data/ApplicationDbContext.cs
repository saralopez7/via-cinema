using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Models.Payments;
using VIACinemaApp.Models.Transactions;

namespace VIACinemaApp.Data
{
    /// <inheritdoc />
    /// <summary>
    ///         The ApplicationDbContext object handles the task of connecting to the database and mapping objects
    ///         (Movies, AvailableMovies, Payments, Transactions, Seats...) to database records.
    ///         The database context is registered with the Dependency Injection container in the ConfigureServices
    ///         method in the Startup.cs file.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<AvailableMovie> AvailableMovies { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<TransactionsInPayments> TransactionsInPayment { get; set; }
    }
}