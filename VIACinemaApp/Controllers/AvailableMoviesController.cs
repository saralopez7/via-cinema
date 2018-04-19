﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Data;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Controllers
{
    public class AvailableMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _totalNumberOfSeats = 75;

        public AvailableMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: AvailableMovies
        public async Task<IActionResult> Index()
        {
            // Use LINQ to get list of movie titles.
            IQueryable<string> titleQuery = from m in _context.Movie
                                            orderby m.MovieTitle
                                            select m.MovieTitle;

            ICollection<AvailableMovies> availableMovies = _context.AvailableMovies.ToList();
            foreach (var availableMovie in availableMovies)
            {
                availableMovie.Movie = _context.Movie.FirstOrDefault(x => x.Id == availableMovie.MovieId);
                availableMovie.AvailableSeats = _context.Seat.Count(x => x.MovieId == availableMovie.Id);
            }

            var movieGenreVm =
                new MovieGenreViewModel
                {
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Movies = availableMovies.ToList()
                };

            return View(movieGenreVm);
        }

        // GET: AvailableMovies/GetMovies?id
        public async Task<IActionResult> GetMovies(string id)
        {
            // Use LINQ to get list of movie titles.
            IQueryable<string> titleQuery = from m in _context.Movie
                                            orderby m.MovieTitle
                                            select m.MovieTitle;

            ICollection<AvailableMovies> availableMovies = _context.AvailableMovies.ToList();
            foreach (var availableMovie in availableMovies)
            {
                availableMovie.Movie = _context.Movie.FirstOrDefault(x => x.Id == availableMovie.MovieId);
                availableMovie.AvailableSeats =
                    _totalNumberOfSeats - _context.Seat.Count(x => x.MovieId == availableMovie.Id);
            }

            if (!String.IsNullOrEmpty(id))
            {
                DateTime date = Convert.ToDateTime(id);
                availableMovies = availableMovies.Where(s => s.AvailableDate.Day == date.Day
                                                             && s.AvailableDate.Month == date.Month
                                                             && s.AvailableDate.Year == date.Year).ToList();
            }

            var movieGenreVm =
                new MovieGenreViewModel
                {
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Movies = availableMovies.ToList()
                };

            ViewBag.AvailableMovies = movieGenreVm;
            return PartialView("GetMovies");
        }
    }
}