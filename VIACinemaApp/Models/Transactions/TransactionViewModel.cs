using System.ComponentModel.DataAnnotations;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Models.Transactions
{

    public class TransactionViewModel
    {
        public AvailableMovie Movie { get; set; }

        [Display(Name = "Seats")]
        public string SeatNumber { get; set; }

        public int Id { get; set; }

        public decimal Price { get; set; }
    }
}