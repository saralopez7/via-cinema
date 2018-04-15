using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VIACinemaApp.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Display(Name = "Seat Satus")]
        public String Status { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

    }
}
