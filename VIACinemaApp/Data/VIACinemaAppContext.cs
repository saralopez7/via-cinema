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

        public DbSet<VIACinemaApp.Models.Movie> Movie { get; set; }

        public DbSet<VIACinemaApp.Models.AvailableMovies> AvailableMovies { get; set; }
    }
}