using System.Collections.Generic;
using System.Threading.Tasks;
using PayPal.Api;
using VIACinemaApp.Models.Transactions;
using Transaction = VIACinemaApp.Models.Transactions.Transaction;

namespace VIACinemaApp.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<TransactionViewModel>> GetTransactions(string userId);

        Task<TransactionViewModel> GetTransaction(int? id);

        Task<Transaction> RegisterSeats(Transaction transaction);

        void CreateTransaction(Transaction transaction);

        void EditTransaction(int id, Transaction transaction);

        Task<TransactionViewModel> Delete(int? id);

        Task DeleteConfirmed(int id);

        bool TransactionExists(int id);
    }
}