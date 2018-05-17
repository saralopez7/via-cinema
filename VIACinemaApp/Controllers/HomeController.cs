using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VIACinemaApp.Models;

namespace VIACinemaApp.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     HomeController handles browser requests under the url: https://localhost:44387
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        ///     Return Home page view.
        /// </summary>
        /// <returns>Index View</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Return About page view.
        /// </summary>
        /// <returns>About View</returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Get to know VIA Cinema.";

            return View();
        }

        /// <summary>
        ///     Return Contact page view.
        /// </summary>
        /// <returns>Contact View</returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact VIA Cinema.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}