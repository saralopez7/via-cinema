using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Data;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovie(int? id)
        {
            var movie = await _context.Movies
                .SingleOrDefaultAsync(m => m.Id == id);

            return movie;
        }

        public async void CreateMovie(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> EditSeat(int? id)
        {
            return await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async void EditSeat(int id, Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> Delete(int? id)
        {
            return await _context.Movies
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}