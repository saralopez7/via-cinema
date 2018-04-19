using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Data;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Controllers
{
    public class AvailableMoviesController : Controller
    {
        private readonly ViaCinemaAppContext _context;
        private readonly int _totalNumberOfSeats = 75;

        public AvailableMoviesController(ViaCinemaAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: AvailableMovies
        public async Task<IActionResult> Index()
        {
            // Use LINQ to get list of movie titles.
            IQueryable<string> titleQuery = from m in _context.Movie
                                            orderby m.MovieTitle
                                            select m.MovieTitle;

            ICollection<AvailableMovies> availableMovies = _context.AvailableMovies.ToList();
            foreach (var availableMovie in availableMovies)
            {
                availableMovie.Movie = _context.Movie.FirstOrDefault(x => x.Id == availableMovie.MovieId);
                availableMovie.AvailableSeats = _context.Seat.Count(x => x.MovieId == availableMovie.Id);
            }

            var movieGenreVm =
                new MovieGenreViewModel
                {
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Movies = availableMovies.ToList()
                };

            return View(movieGenreVm);
        }

        // GET: AvailableMovies/GetMovies?id
        public async Task<IActionResult> GetMovies(string id)
        {
            // Use LINQ to get list of movie titles.
            IQueryable<string> titleQuery = from m in _context.Movie
                                            orderby m.MovieTitle
                                            select m.MovieTitle;

            ICollection<AvailableMovies> availableMovies = _context.AvailableMovies.ToList();
            foreach (var availableMovie in availableMovies)
            {
                availableMovie.Movie = _context.Movie.FirstOrDefault(x => x.Id == availableMovie.MovieId);
                availableMovie.AvailableSeats = _totalNumberOfSeats - _context.Seat.Count(x => x.MovieId == availableMovie.Id);
            }

            if (!String.IsNullOrEmpty(id))
            {
                DateTime date = Convert.ToDateTime(id);
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

            ViewBag.AvailableMovies = movieGenreVm;
            return PartialView("GetMovies");
        }

        // GET: AvailableMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableMovies = await _context.AvailableMovies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (availableMovies == null)
            {
                return NotFound();
            }

            availableMovies.Movie = _context.Movie.FirstOrDefault(x => x.Id == availableMovies.MovieId);

            return View(availableMovies);
        }

        // GET: AvailableMovies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvailableMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,AvailableDate,AvailableSeats")] AvailableMovies availableMovies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availableMovies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(availableMovies);
        }

        // GET: AvailableMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableMovies = await _context.AvailableMovies.SingleOrDefaultAsync(m => m.Id == id);
            if (availableMovies == null)
            {
                return NotFound();
            }
            return View(availableMovies);
        }

        // POST: AvailableMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,AvailableDate,AvailableSeats")] AvailableMovies availableMovies)
        {
            if (id != availableMovies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availableMovies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailableMoviesExists(availableMovies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(availableMovies);
        }

        // GET: AvailableMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableMovies = await _context.AvailableMovies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (availableMovies == null)
            {
                return NotFound();
            }

            return View(availableMovies);
        }

        // POST: AvailableMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availableMovies = await _context.AvailableMovies.SingleOrDefaultAsync(m => m.Id == id);
            _context.AvailableMovies.Remove(availableMovies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableMoviesExists(int id)
        {
            return _context.AvailableMovies.Any(e => e.Id == id);
        }
    }
}