using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PayPal;
using VIACinemaApp.Data;
using VIACinemaApp.Models.Transactions;
using VIACinemaApp.Repositories.Interfaces;
using PayPal.Api;
using VIACinemaApp.Models.Movies;
using Transaction = PayPal.Api.Transaction;

namespace VIACinemaApp.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;
        private Payment _payment;
        private readonly APIContext _apiContext;
        private readonly IOptions<Configuration> _config;

        public TransactionsRepository(ApplicationDbContext context, IOptions<Configuration> config)
        {
            _config = config;
            _context = context;
            var clientId = _config.Value.ClientId;
            var clientSecret = _config.Value.ClientSecret;
            _apiContext = new APIContext(new OAuthTokenCredential(clientId, clientSecret).GetAccessToken());
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactions(string userId)
        {
            var transactions = Mapper.Map<IEnumerable<TransactionViewModel>>(
                await _context.Transactions.Where(x => x.UserId == userId
                                                       && x.Status == TransactionStatus.InProcess).ToListAsync());

            var transactionViewModels = transactions as TransactionViewModel[] ?? transactions.ToArray();

            foreach (var transaction in transactionViewModels)
            {
                var availableMovie = transaction.Movie = _context.AvailableMovies.FirstOrDefault(x => x.Id == transaction.Movie.Id);
                if (availableMovie != null)
                    availableMovie.Movie = _context.Movies.FirstOrDefault(x => x.Id == availableMovie.MovieId);
            }

            return transactionViewModels;
        }

        public async Task<TransactionViewModel> GetTransaction(int? id)
        {
            var transaction = await _context.Transactions
                .SingleOrDefaultAsync(m => m.Id == id);

            var availableMovie =
                _context.AvailableMovies.FirstOrDefault(x => x.Id == transaction.MovieId);

            if (availableMovie != null)
                availableMovie.Movie = _context.Movies.FirstOrDefault(x => x.Id == availableMovie.MovieId);

            var transactionVm = Mapper.Map<TransactionViewModel>(transaction);
            transactionVm.Movie = availableMovie;

            return transactionVm;
        }

        public async Task<Models.Transactions.Transaction> RegisterSeats(Models.Transactions.Transaction transaction)
        {
            if (TransactionExists(transaction.SeatNumber, transaction.UserId, transaction.MovieId))
            {
                return _context.Transactions.FirstOrDefault(x =>
                    x.MovieId == transaction.MovieId && x.UserId == transaction.UserId &&
                    string.Equals(transaction.SeatNumber, transaction.SeatNumber, StringComparison.Ordinal));
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async void CreateTransaction(Models.Transactions.Transaction transaction)
        {
            _context.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async void EditTransaction(int id, Models.Transactions.Transaction transaction)
        {
            _context.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<TransactionViewModel> Delete(int? id)
        {
            var transaction = await GetTransaction(id);

            return transaction;
        }

        public async Task DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

        public bool TransactionExists(string seatNumbers, string userId, int movieId)
        {
            return _context.Transactions.Any(e => e.UserId == userId && e.SeatNumber == seatNumbers && e.MovieId == movieId);
        }
    }
}