using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using VIACinemaApp.Data;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .SingleOrDefaultAsync(m => m.Id == id);

            AvailableMovie availableMovie =
                _context.AvailableMovies.FirstOrDefault(x => x.Id == transaction.MovieId);

            if (availableMovie != null)
                availableMovie.Movie = _context.Movies.FirstOrDefault(x => x.Id == availableMovie.MovieId);
            var transactionVm = new TransactionViewModel
            {
                Movie = availableMovie,
                SeatNumber = transaction.SeatNumber,
                Id = transaction.Id
            };

            return PartialView(transactionVm);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/RegisterSeats
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterSeats(string seats)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            var user = await _userManager.GetUserAsync(User);

            var json = JObject.Parse(seats);

            var transaction = new Transaction
            {
                MovieId = json["movieId"].ToObject<int>(),
                SeatNumber = string.Join(",", json["seats"].ToObject<List<int>>().ToArray()),
                StartTime = DateTime.Now,
                Status = TransactionStatus.InProcess,
                UserId = user.Id
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var movie = _context.AvailableMovies.FirstOrDefault(x => x.Id == transaction.MovieId);
            if (movie != null) movie.AvailableSeats = 75 - json["seats"].ToObject<List<int>>().Capacity;

            await _context.SaveChangesAsync();

            return Json(transaction);
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SeatNumber,MovieId,UserId,StartTime,Status")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeatNumber,MovieId,UserId,StartTime,Status")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}