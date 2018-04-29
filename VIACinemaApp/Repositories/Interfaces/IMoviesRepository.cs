using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetMovies();

        Task<Movie> GetMovie(int? id);

        void CreateMovie(Movie movie);

        Task<Movie> EditSeat(int? id);

        void EditSeat(int id, Movie movie);

        Task<Movie> Delete(int? id);

        Task<Movie> DeleteConfirmed(int id);

        bool MovieExists(int id);
    }
}