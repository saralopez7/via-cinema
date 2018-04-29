using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Transactions;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository _transactionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly decimal _TICKET_PRICE = 89;

        public TransactionsController(ITransactionsRepository transactionRepository, UserManager<ApplicationUser> userManager)
        {
            _transactionRepository = transactionRepository;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(await _transactionRepository.GetTransactions(user.Id));
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return PartialView(await _transactionRepository.GetTransaction(id));
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/RegisterSeats
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
                Price = _TICKET_PRICE * json["seats"].ToObject<List<int>>().Count
            };

            return Json(await Task.Run(() => _transactionRepository.RegisterSeats(transaction)));
        }

        // POST: Transactions/Create
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

        // POST: Transactions/EditSeat/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeatNumber,MovieId,UserId,StartTime,Status")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            try
            {
                await Task.Run(() => _transactionRepository.EditTransaction(id, transaction));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.Id))
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Transactions/Delete/5
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

        private bool TransactionExists(int id)
        {
            return _transactionRepository.TransactionExists(id);
        }

        public async Task<IActionResult> CompleteTransaction()
        {
            //TODO: change transaction status when payment has been completed.
            var user = await _userManager.GetUserAsync(User);

            await Task.Run(() => _transactionRepository.CompleteTransactions(user.Id));

            return RedirectToAction(nameof(Index)); // TODO: redirect to payment window.
        }
    }
}