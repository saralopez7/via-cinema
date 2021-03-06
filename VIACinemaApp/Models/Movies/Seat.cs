﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VIACinemaApp.Models.Movies
{
    public class Seat
    {
        public int Id { get; set; }

        [ForeignKey("Id")]
        public int MovieId { get; set; }

        public int SeatNumber { get; set; }
    }
}