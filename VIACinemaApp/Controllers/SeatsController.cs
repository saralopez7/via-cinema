using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     SeatsController handles browser requests under the url: https://localhost:44387/Seats
    ///     Retrieves model data from the Seats class and calls views that return a response.
    ///     Handles route data and query-string values, and passes these values to the Seats model.
    ///     Allows the users to select seats for a given available movie.
    /// </summary>
    public class SeatsController : Controller
    {
        private readonly ISeatsRepository _seatsRepository;

        public SeatsController(ISeatsRepository seatsRepository)
        {
            _seatsRepository = seatsRepository;
        }

        /// <summary>
        ///     Get all seats.
        ///     Returns a View Result object rendereing the model received by the GetSeats action method.
        ///     GET: Seats or GET: Seats/5
        /// </summary>
        /// <param name="id">id of the seat to be redered. If no seat is specified all seats will be returned.</param>
        /// <returns>View result for the Index view on the seats objects to be rendered.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            return View(await _seatsRepository.GetSeats(id));
        }
    }
}