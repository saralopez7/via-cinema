using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VIACinemaApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int SeatId { get; set; }

        public int UserId { get; set; }
    }
}