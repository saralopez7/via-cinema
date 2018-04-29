using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VIACinemaApp.Models.Transactions;

namespace VIACinemaApp.Models.Transactions
{
    public class Transaction
    {
        public int Id { get; set; }

        public string SeatNumber { get; set; }

        [ForeignKey("Id")]
        public int MovieId { get; set; }

        [ForeignKey("Id")]
        public string UserId { get; set; }

        public DateTime StartTime { get; set; }

        public TransactionStatus Status { get; set; }
    }
}