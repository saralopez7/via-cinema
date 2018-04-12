using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VIACinemaApp.Models
{
    public class MovieGenreViewModel
    {
        public List<AvailableMovies> Movies;
        public SelectList Titles; // allow the user to select a genre from the list.
        public string MovieTitle { get; set; } // selected genre
    }
}