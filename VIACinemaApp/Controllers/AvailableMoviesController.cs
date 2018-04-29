using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    public class AvailableMoviesController : Controller
    {
        private readonly IAvailableMoviesRepository _availableMoviesRepository;

        public AvailableMoviesController(IAvailableMoviesRepository availableMoviesRepository)
        {
            _availableMoviesRepository = availableMoviesRepository;
        }

        [HttpGet]
        // GET: AvailableMovies
        public async Task<IActionResult> Index()
        {
            return View(await _availableMoviesRepository.GetMovies());
        }

        [HttpPost]
        // GET: AvailableMovies/GetMovies?id=...
        public async Task<IActionResult> GetMovies(string id)
        {
            ViewBag.AvailableMovies = await _availableMoviesRepository.GetMovies(id);
            return PartialView("GetMovies");
        }

        [HttpGet]
        // GET: AvailableMovies/Details/5
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