using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Payments;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     PaymentsController handles browser requests under the url: https://localhost:44387/Payments
    ///     Allow Authorized users (users logged in) to make payments for given transactions.
    ///     The user will not be able to make a payment unless he is logged it.
    ///     If the user is not logged in he will be redirected to the Login controlelr.
    ///     Once logged in, he will be redirected again to the payment Create action method in this controller.
    /// </summary>
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

        /// <summary>
        ///     Get information about a complete payment by id passed as a route parameter.
        ///     GET: Payments/Details/5
        /// </summary>
        /// <param name="id">id of the payment object to be found. </param>
        /// <returns>View result for the Details view on a payment object to be rendered.</returns>
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = _paymentsRepository.PaymentDetails(id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        /// <summary>
        ///     Return Create payment view result with the neccessary fields to create/make a payment.
        ///     GET: Payments/Create
        /// </summary>
        /// <returns>View result rendering the Create view.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     Create/make a payment for the current user transactions.
        ///     POST: Payments/Create
        /// </summary>
        /// <param name="payment">payment object specified in the body of the POST request message.</param>
        /// <returns>Details View after creating a new payment record in the database.</returns>
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