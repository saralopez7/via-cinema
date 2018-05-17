using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Transactions;
using VIACinemaApp.Repositories.Interfaces;
using Transaction = VIACinemaApp.Models.Transactions.Transaction;

namespace VIACinemaApp.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     PaymentsController handles browser requests under the url: https://localhost:44387/Transactions
    ///     Allow Authorized users (users logged in) to make transactions and later on payments for given transactions.
    ///     The user will not be able to make a transaction unless he is logged it.
    ///     If the user is not logged in he will be redirected to the Login controlelr.
    ///     Once logged in, he will be redirected again to the payment Create action method in this controller.
    /// </summary>
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository _transactionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private const decimal TicketPrice = 89; // default ticket price per ticket.

        public TransactionsController(ITransactionsRepository transactionRepository, UserManager<ApplicationUser> userManager)
        {
            _transactionRepository = transactionRepository;
            _userManager = userManager;
        }

        /// <summary>
        ///     Get all seats.
        ///     Returns a View Result object rendereing the model received by the GetTransactions action method.
        ///     (Model returned by GetTransactions action method contains only current user transactions)
        ///     GET: Transactions
        /// </summary>
        /// <returns>View result for the Index view on the available transactions objects to be rendered.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(await _transactionRepository.GetTransactions(user.Id));
        }

        /// <summary>
        ///     Register Seats to be bought and create transaction for the current user.
        ///     POST: Transactions/RegisterSeats
        /// </summary>
        /// <param name="seats">String with the seats to be included in the transaction.</param>
        /// <returns>Json result with the transaction created. </returns>
        [HttpPost]
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
                UserId = user.Id,
                Price = TicketPrice * json["seats"].ToObject<List<int>>().Count
            };

            return Json(await Task.Run(() => _transactionRepository.RegisterSeats(transaction)));
        }

        /// <summary>
        ///     Get information about a transaction by id passed as a route parameter.
        ///     GET: Transactions/Details/5
        ///     Used when checking out and redirecting the user to payment so he can review
        ///     the transaction before completing the payment.
        /// </summary>
        /// <param name="id">id of the transaction which information is to be returned.</param>
        /// <returns>View result for the Details view on a transaction object to be rendered.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return PartialView(await _transactionRepository.GetTransaction(id));
        }

        // POST: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     Create a transaction record in the database.
        ///     POST: Transactions/Create
        /// </summary>
        /// <param name="transaction">transaction object specified in the body of the POST request message.</param>
        /// <returns>Index View after creating a new transaction record in the database.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SeatNumber,MovieId,UserId,StartTime,Status")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _transactionRepository.CreateTransaction(transaction));
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        /// <summary>
        ///     Delete transaction with the id specified in the route parameter.
        ///     Use to delete a transaction from the "shopping cart"
        ///     Transactions/Delete/5
        /// </summary>
        /// <param name="id">id of the transaction to be deleted.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionRepository.Delete(id);

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
            await _transactionRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
    }
}