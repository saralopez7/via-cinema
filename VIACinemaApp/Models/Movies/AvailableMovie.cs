using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIACinemaApp.Models.Movies
{
    public class AvailableMovie
    {
        public int Id { get; set; }

        [ForeignKey("Id")]
        public int MovieId { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime AvailableDate { get; set; }

        public int? AvailableSeats { get; set; }

        public Movie Movie { get; set; }
    }
}