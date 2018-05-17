using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     AvailableMoviesController handles browser requests under the url: https://localhost:44387/AvailableMovies
    ///     Retrieves model data from the AvailableMovies class and calls views that return a response.
    ///     Handles route data and query-string values, and passes these values to the AvailableMovies model
    /// </summary>
    public class AvailableMoviesController : Controller
    {
        private readonly IAvailableMoviesRepository _availableMoviesRepository;

        public AvailableMoviesController(IAvailableMoviesRepository availableMoviesRepository)
        {
            _availableMoviesRepository = availableMoviesRepository;
        }

        /// <summary>
        ///     Get all available movies.
        ///     Returns a View Result object rendereing the model received by the GetMovies action method
        ///     GET: AvailableMovies
        /// </summary>
        /// <returns>View result for the Index view on the available movie objects to be rendered.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _availableMoviesRepository.GetMovies());
        }

        /// <summary>
        ///     Get movie by id passed as a route parameter.
        ///     Get movie by id passed as a route parameter.
        ///     POST: AvailableMovies/GetMovies?id=...
        /// </summary>
        /// <param name="id">id route parameter soecifying the the movie to be found. </param>
        /// <returns>Partial View object of the GetMovies view.</returns>
        [HttpPost]
        public async Task<IActionResult> GetMovies(string id)
        {
            ViewBag.AvailableMovies = await _availableMoviesRepository.GetMovies(id);
            return PartialView("GetMovies");
        }

        /// <summary>
        ///     Get information about an available movie by id passed as a route parameter.
        ///     GET: AvailableMovies/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View result for the Details view on an available movie object to be rendered.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _availableMoviesRepository.EditAvailableMovie(id));
        }
    }
}