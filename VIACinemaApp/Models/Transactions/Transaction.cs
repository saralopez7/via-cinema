﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using VIACinemaApp.Models.Movies;

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

        public decimal Price { get; set; }

        public Movie Movie { get; set; }
    }
}