using System.Collections.Generic;
using System.Threading.Tasks;
using VIACinemaApp.Models.Transactions;
using Transaction = VIACinemaApp.Models.Transactions.Transaction;

namespace VIACinemaApp.Repositories.Interfaces
{
    /// <summary>
    ///     ITransactionsRepository is used to make domain object persistance more generic and make it
    ///     easier to manage objects.
    /// </summary>
    public interface ITransactionsRepository
    {
        /// <summary>
        ///     Get transaction records for the current user.
        ///     All transactions for the current user will be handled at once.
        ///     Once the user adds an item to the shopping cart he creates a transaction.
        /// </summary>
        /// <param name="userId">User id of the current user.
        /// Used to asssociate a user to a transaction and later on to a payment.</param>
        /// <returns>Transactions associated with the current user.</returns>
        Task<IEnumerable<TransactionViewModel>> GetTransactions(string userId);

        /// <summary>
        ///     Get transaction record by id.
        ///     NOTE: the id is nullable so we can query for a transaction with id = 0
        /// </summary>
        /// <param name="id"> id of the transaction record to be found.</param>
        /// <returns>Transaction object with the given id.</returns>
        Task<TransactionViewModel> GetTransaction(int? id);

        /// <summary>
        ///     Associate Seats to a transaction and create transaction record for the current user.
        /// </summary>
        /// <param name="transaction">Transaction object to be added to the database.</param>
        /// <returns>Transaction object created.</returns>
        Task<Transaction> RegisterSeats(Transaction transaction);

        /// <summary>
        ///     Insert transaction record in the database.
        /// </summary>
        /// <param name="transaction">Transaction object to be inserted in the database.</param>
        void CreateTransaction(Transaction transaction);

        /// <summary>
        ///     Get transaction record with the given id from the database to be deleted without deleting it.
        ///     Allow the user to decide if he really wants to delete the record.
        /// </summary>
        /// <param name="id"> Id of the transaction to be deleted.</param>
        /// <returns>Transaction view model containing the deleted transaction. </returns>
        Task<TransactionViewModel> Delete(int? id);

        /// <summary>
        ///    Delete transaction record with the given id.
        /// </summary>
        /// <param name="id">Id of the deleted </param>
        /// <returns>Asynchronous operation</returns>
        Task DeleteConfirmed(int id);
    }
}