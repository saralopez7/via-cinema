using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VIACinemaApp.Models.Movies
{
    public class MovieGenreViewModel
    {
        public List<AvailableMovie> Movies;
        public SelectList Titles; // allow the user to select a genre from the list.
        public string MovieTitle { get; set; } // selected genre
    }
}