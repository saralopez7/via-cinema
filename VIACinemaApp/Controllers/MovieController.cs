using Microsoft.AspNetCore.Mvc;

namespace VIACinemaApp.Controllers
{
    /// <summary>
    /// /[Controller]/[ActionName]/[Parameters]
    /// </summary>
    public class MovieController : Controller
    {
        // GET: /Movie/
        public IActionResult Index()
        {
            return View();
        }

        // GET: Movie/Welcome
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}