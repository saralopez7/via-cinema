using System.Threading.Tasks;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Repositories.Interfaces
{
    /// <summary>
    ///     IAvailableMoviesRepository is used to make domain object persistance more generic and make it
    ///     easier to manage objects.
    /// </summary>
    public interface IAvailableMoviesRepository
    {
        /// <summary>
        ///     Get all available movie records in  the database.
        /// </summary>
        /// <returns> Collection of available movie objects stored in the database.</returns>
        Task<MovieGenreViewModel> GetMovies();

        /// <summary>
        ///     Get available movie record by id.
        /// </summary>
        /// <param name="id"> id of the available movie record to be found.</param>
        /// <returns>Available movie object with the given id</returns>
        Task<MovieGenreViewModel> GetMovies(string id);

        /// <summary>
        ///     Get available movie record to be edited without editing it.
        ///     NOTE: the id is nullable so we can edit the available movie with id = 0
        /// </summary>
        /// <param name="id"> Id of the available movie to be edited.</param>
        /// <returns>AvailableMovie object to be edited.</returns>
        Task<AvailableMovie> EditAvailableMovie(int? id);
    }
}