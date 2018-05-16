using System.ComponentModel.DataAnnotations.Schema;

namespace VIACinemaApp.Models
{
    public class TransactionsInPayments
    {
        public int Id { get; set; }

        [ForeignKey("id")]
        public int TransactionId { get; set; }

        [ForeignKey("id")]
        public int PaymentId { get; set; }
    }
}