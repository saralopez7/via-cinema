using System.ComponentModel.DataAnnotations;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Models.Transactions
{
    /// <summary>
    ///     The PaymentViewModel class is used to ease getting all the transactions associated to a payment.
    /// </summary>
    public class TransactionViewModel
    {
        public AvailableMovie Movie { get; set; }

        [Display(Name = "Seats")]
        public string SeatNumber { get; set; }

        public int Id { get; set; }

        public decimal Price { get; set; }
    }
}