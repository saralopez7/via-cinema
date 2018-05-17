using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VIACinemaApp.Models;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentsController(IPaymentsRepository paymentsRepository, UserManager<ApplicationUser> userManager)
        {
            _paymentsRepository = paymentsRepository;
            _userManager = userManager;
        }

        // GET: Movies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = _paymentsRepository.PaymentDetails(id);

            var userId = _userManager.GetUserId(User);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Id,CredictCard,OwnerName,OwnerSurname,SecurityCode,ExpiryMonth,ExpiryYear")]
            Payment payment)
        {
            var userId = _userManager.GetUserId(User);

            if (!ModelState.IsValid) return View(payment);

            var paymentId = _paymentsRepository.CreatePayment(userId, payment);

            return RedirectToAction("Details", "Payments", new { id = paymentId });
        }
    }
}