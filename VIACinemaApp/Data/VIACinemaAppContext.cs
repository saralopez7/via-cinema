using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Models;

namespace VIACinemaApp.Models
{
    public class VIACinemaAppContext : DbContext
    {
        public VIACinemaAppContext(DbContextOptions<VIACinemaAppContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<AvailableMovies> AvailableMovies { get; set; }

        public DbSet<Seat> Seat { get; set; }

        public DbSet<Transaction> Transaction { get; set; }
    }
}