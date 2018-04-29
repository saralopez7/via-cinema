using System.Threading.Tasks;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Repositories.Interfaces
{
    public interface IAvailableMoviesRepository
    {
        Task<MovieGenreViewModel> GetMovies();

        Task<MovieGenreViewModel> GetMovies(string id);

        Task<AvailableMovie> EditAvailableMovie(int? id);
    }
}