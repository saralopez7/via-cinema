﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Data;
using VIACinemaApp.Models.Transactions;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Transaction> RegisterSeats(Transaction transaction)
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

        public async void CreateTransaction(Transaction transaction)
        {
            _context.Add(transaction);
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

        public bool TransactionExists(string seatNumbers, string userId, int movieId)
        {
            return _context.Transactions.Any(e => e.UserId == userId && e.SeatNumber == seatNumbers && e.MovieId == movieId);
        }
    }
}