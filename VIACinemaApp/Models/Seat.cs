using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VIACinemaApp.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        [ForeignKey("Id")]
        public int MovieId { get; set; }

        public int SeatNumber { get; set; }
    }
}