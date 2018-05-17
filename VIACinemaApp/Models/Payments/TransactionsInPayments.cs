using System.ComponentModel.DataAnnotations.Schema;

namespace VIACinemaApp.Models.Payments
{
    /// <summary>
    ///     The PaymentViewModel class is used to ease getting all the transactions associated to a payment.
    /// </summary>
    public class TransactionsInPayments
    {
        public int Id { get; set; }

        [ForeignKey("id")]
        public int TransactionId { get; set; }

        [ForeignKey("id")]
        public int PaymentId { get; set; }
    }
}