using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Models
{
    public class TransactionViewModel
    {
        public AvailableMovie Movie { get; set; }
        public string SeatNumber { get; set; }

        public int Id { get; set; }
    }
}