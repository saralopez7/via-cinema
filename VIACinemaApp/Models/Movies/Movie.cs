﻿using System;
using System.ComponentModel.DataAnnotations;

namespace VIACinemaApp.Models.Movies
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Movie")]
        public string MovieTitle { get; set; }

        public double Duration { get; set; }
        public Genre Genre { get; set; }
        public String Director { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }
        public String Plot { get; set; }
    }
}