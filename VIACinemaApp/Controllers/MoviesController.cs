using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///    MoviesController handles browser requests under the url: https://localhost:44387/Movies
    ///     Retrieves model data from the Movies class and calls views that return a response.
    ///     Handles route data and query-string values, and passes these values to the Movies model
    /// </summary>
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        /// <summary>
        ///     Get all movies.
        ///     GET: Movies
        /// </summary>
        /// <returns>View result for the Index view on the movie objects to be rendered.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _moviesRepository.GetMovies());
        }

        /// <summary>
        ///     Get information about a movie by id passed as a route parameter.
        ///     GET: Movies/Details/5
        /// </summary>
        /// <param name="id">id of the movie object  to be found. </param>
        /// <returns>View result for the Details view on a movie object to be rendered.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesRepository.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        /// <summary>
        ///     Return Create movie view result with the neccessary fields to create a movie.
        ///     GET: Movies/Create
        /// </summary>
        /// <returns>View result rendering the Create view.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     Create a movie record in the database.
        ///     POST: Movies/Create
        /// </summary>
        /// <param name="movie">movie object specified in the body of the POST request message.</param>
        /// <returns>Index View after creating a new movie record in the database.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieTitle,Duration,Genre,Director,ReleaseDate,Rating,Plot")] Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);

            await Task.Run(() => _moviesRepository.CreateMovie(movie));
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        ///     Edit information about a movie by id passed as a route parameter.
        ///     GET: Movies/EditSeat/5
        /// </summary>
        /// <param name="id">Id of the movie to be edited.</param>
        /// <returns>Edit view with the movie object record to be edited.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesRepository.EditSeat(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        /// <summary>
        ///     Edit information about a movie by id passed as a route parameter.
        ///     POST: Movies/EditSeat/5
        /// </summary>
        /// <param name="id">Id of the movie to be edited.</param>
        /// <param name="movie">Movie record with the columns to be updated</param>
        /// <returns>View with all the movies in the database after editing the given movie.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieTitle,Duration,Genre,Director,ReleaseDate,Rating,Plot")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(movie);

            try
            {
                await Task.Run(() => _moviesRepository.EditSeat(id, movie));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        ///     Delete movie by id specified in the route parameter.
        ///     GET: Movies/Delete/5
        /// </summary>
        /// <param name="id">id of the movie to be deleted</param>
        /// <returns>View result for the Dekete view on a movie object to be rendered.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesRepository.Delete(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _moviesRepository.DeleteConfirmed(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        ///     Check if movie record with a given id exists in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true is movie object exists, false if it does not exist.</returns>
        private bool MovieExists(int id)
        {
            return _moviesRepository.MovieExists(id);
        }
    }
}