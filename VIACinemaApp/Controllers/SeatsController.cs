using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    public class SeatsController : Controller
    {
        private readonly ISeatsRepository _seatsRepository;

        public SeatsController(ISeatsRepository seatsRepository)
        {
            _seatsRepository = seatsRepository;
        }

        // GET: Seats
        public async Task<IActionResult> Index(string id)
        {
            return View(await _seatsRepository.GetSeats(id));
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _seatsRepository.GetSeat(id);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // GET: Seats/EditSeat/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _seatsRepository.EditSeat(id);

            if (seat == null)
            {
                return NotFound();
            }
            return View(seat);
        }

        // POST: Seats/EditSeat/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Row,Column")] Seat seat)
        {
            if (id != seat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _seatsRepository.EditSeat(id, seat));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatExists(seat.Id))
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
            return View(seat);
        }

        private bool SeatExists(int id)
        {
            return _seatsRepository.SeatExists(id);
        }
    }
}