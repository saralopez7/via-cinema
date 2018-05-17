using System.Collections.Generic;
using System.Threading.Tasks;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Repositories.Interfaces
{
    /// <summary>
    ///     IMovieRepository is used to make domain object persistance more generic and make it
    ///     easier to manage objects.
    /// </summary>
    public interface IMoviesRepository
    {
        /// <summary>
        ///     Get all movie records in  the database.
        /// </summary>
        /// <returns> Collection of movie objects stored in the database.</returns>
        Task<IEnumerable<Movie>> GetMovies();

        /// <summary>
        ///     Get movie record by id.
        ///     NOTE: the id is nullable so we can query for movies with id = 0
        /// </summary>
        /// <param name="id"> id of the movie record to be found.</param>
        /// <returns>Movie object with the given id</returns>
        Task<Movie> GetMovie(int? id);

        /// <summary>
        ///     Insert movie record in the database.
        /// </summary>
        /// <param name="movie">Movie object to be inserted in the database.</param>
        void CreateMovie(Movie movie);

        /// <summary>
        ///     Get movie record to be edited without editing it.
        ///     NOTE: the id is nullable so we can edit the movie with id = 0
        /// </summary>
        /// <param name="id"> Id of the movie to be edited.</param>
        /// <returns>Movie object to be edited.</returns>
        Task<Movie> EditSeat(int? id);

        /// <summary>
        ///     Update movie columns with the given parameters.
        /// </summary>
        /// <param name="id">Id of the movie to be edited</param>
        /// <param name="movie">Movie  object containing the columns to be updated. </param>
        void EditSeat(int id, Movie movie);

        /// <summary>
        ///   Get movie record to be deleted without deleting it.
        ///   NOTE: the id is nullable so we can delete the movie with id = 0
        /// </summary>
        /// <param name="id">Id of the movie record to be deleted</param>
        /// <returns>Movie object to be deleted.</returns>
        Task<Movie> Delete(int? id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Movie> DeleteConfirmed(int id);

        /// <summary>
        ///     Check if movie record with the given id exists in the database.
        /// </summary>
        /// <param name="id">if of the movie to be found.</param>
        /// <returns>True if movie record exists.</returns>
        bool MovieExists(int id);
    }
}