using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Data;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Repositories
{
    public class AvailableMoviesRepository : IAvailableMoviesRepository
    {
        private readonly ApplicationDbContext _context;
        private const int TotalNumberOfSeats = 75; // total number of seats in each movie room

        public AvailableMoviesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MovieGenreViewModel> GetMovies()
        {
            // Use LINQ to get list of movie titles.
            var titleQuery = from m in _context.Movies
                             orderby m.MovieTitle
                             select m.MovieTitle;

            var availableMovies = Mapper.Map<ICollection<AvailableMovie>>(_context.AvailableMovies.ToList());

            var movieGenreVm =
                new MovieGenreViewModel
                {
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Movies = availableMovies.ToList()
                };

            return movieGenreVm;
        }

        public async Task<MovieGenreViewModel> GetMovies(string id)
        {
            // Use LINQ to get list of movie titles.
            var titleQuery = from m in _context.Movies
                             orderby m.MovieTitle
                             select m.MovieTitle;

            ICollection<AvailableMovie> availableMovies = _context.AvailableMovies.ToList();
            foreach (var availableMovie in availableMovies)
            {
                availableMovie.Movie = _context.Movies.FirstOrDefault(x => x.Id == availableMovie.MovieId);
                availableMovie.AvailableSeats =
                    TotalNumberOfSeats - _context.Seats.Count(x => x.MovieId == availableMovie.Id);
            }

            if (!string.IsNullOrEmpty(id))
            {
                var date = Convert.ToDateTime(id);
                availableMovies = availableMovies.Where(s => s.AvailableDate.Day == date.Day
                                                             && s.AvailableDate.Month == date.Month
                                                             && s.AvailableDate.Year == date.Year).ToList();
            }

            var movieGenreVm =
                new MovieGenreViewModel
                {
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Movies = availableMovies.ToList()
                };

            return movieGenreVm;
        }

        public async Task<AvailableMovie> EditAvailableMovie(int? id)
        {
            var movie = await _context.AvailableMovies
                .SingleOrDefaultAsync(m => m.Id == id);

            movie.Movie = _context.Movies.FirstOrDefault(x => x.Id == movie.MovieId);

            return movie;
        }
    }
}