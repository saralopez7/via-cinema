using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Transactions;

namespace VIACinemaApp.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<TransactionViewModel>> GetTransactions(string userId);

        Task<TransactionViewModel> GetTransaction(int? id);

        Task<Transaction> RegisterSeats(Transaction transaction, int numberOfSeats);

        void CreateTransaction(Transaction transaction);

        void EditTransaction(int id, Transaction transaction);

        Task<TransactionViewModel> Delete(int? id);

        Task DeleteConfirmed(int id);

        bool TransactionExists(int id);
    }
}