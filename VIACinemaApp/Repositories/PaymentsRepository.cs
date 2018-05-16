using System;
using System.Collections.Generic;
using System.Linq;
using VIACinemaApp.Data;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Models.Payments;
using VIACinemaApp.Repositories.Interfaces;
using Transaction = VIACinemaApp.Models.Transactions.Transaction;
using TransactionStatus = VIACinemaApp.Models.Transactions.TransactionStatus;

namespace VIACinemaApp.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CreatePayment(string userId, Payment payment)
        {
            _context.Add(payment);
            _context.SaveChanges();

            IEnumerable<Transaction> transactions =
                _context.Transactions.Where(x => x.UserId == userId && x.Status != TransactionStatus.Completed).ToList();

            foreach (var transaction in transactions)
            {
                transaction.Status = TransactionStatus.Completed;

                var transactionInPayment = new TransactionsInPayments()
                {
                    TransactionId = transaction.Id,
                    PaymentId = payment.Id
                };

                _context.TransactionsInPayment.Add(transactionInPayment);
                var seats = transaction.SeatNumber.Split(new char[] { ',' }).ToList();

                foreach (var seat in seats)
                {
                    _context.Seats.Add(new Seat()
                    {
                        MovieId = transaction.MovieId,
                        SeatNumber = int.Parse(seat)
                    });
                }
                _context.SaveChanges();
            }

            return payment.Id;
        }

        public PaymentViewModel PaymentDetails(int? id)
        {
            var payments = _context.TransactionsInPayment.Where(m => m.PaymentId == id);

            var transactions = new List<Transaction>();

            foreach (var payment in payments)
            {
                var transaction = _context.Transactions.FirstOrDefault(x => x.Id == payment.TransactionId);

                var availableMovie = _context.AvailableMovies.FirstOrDefault(x => x.Id == transaction.MovieId);
                if (transaction != null)
                    transaction.Movie = _context.Movies.FirstOrDefault(x => x.Id == availableMovie.MovieId);
                transactions.Add(transaction);
            }

            var paymentViewModel = new PaymentViewModel()
            {
                Payment = _context.Payments.FirstOrDefault(x => x.Id == id),
                Transactions = transactions
            };

            return paymentViewModel;
        }
    }
}