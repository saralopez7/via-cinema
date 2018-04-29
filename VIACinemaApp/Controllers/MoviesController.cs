using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Data;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _moviesRepository.GetMovies());
        }

        // GET: Movies/Details/5
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

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieTitle,Duration,Genre,Director,ReleaseDate,Rating,Plot")] Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);

            await Task.Run(() => _moviesRepository.CreateMovie(movie));
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/EditSeat/5
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

        // POST: Movies/EditSeat/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieTitle,Duration,Genre,Director,ReleaseDate,Rating,Plot")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(movie);
        }

        // GET: Movies/Delete/5
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

        private bool MovieExists(int id)
        {
            return _moviesRepository.MovieExists(id);
        }
    }
}