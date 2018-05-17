using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Payments;

namespace VIACinemaApp.Repositories.Interfaces
{
    /// <summary>
    ///     IPaymentsRepository is used to make domain object persistance more generic and make it
    ///     easier to manage objects.
    /// </summary>
    public interface IPaymentsRepository
    {
        /// <summary>
        ///     Insert payment record in the database.
        /// </summary>
        /// <param name="userId"> User id of the current user. Used to associate a user to a payment.</param>
        /// <param name="payment">Payment object to be inserted in the database.</param>
        /// <returns>Id of the created payment record.</returns>
        int CreatePayment(string userId, Payment payment);

        /// <summary>
        ///     Get Payment details in the form of a PaymentViewModel.
        ///     The PaymentViewModel is used to return all the transactions associated to a payment.
        /// </summary>
        /// <param name="id">Id of the payment object to be found</param>
        /// <returns>Payment view model containing the transactions for a given payment.</returns>
        PaymentViewModel PaymentDetails(int? id);
    }
}