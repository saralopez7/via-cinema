﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Models.Transactions;

namespace VIACinemaApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<AvailableMovie> AvailableMovies { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}