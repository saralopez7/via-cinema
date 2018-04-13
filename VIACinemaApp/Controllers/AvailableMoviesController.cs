﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Models;

namespace VIACinemaApp.Controllers
{
    public class AvailableMoviesController : Controller
    {
        private readonly VIACinemaAppContext _context;

        public AvailableMoviesController(VIACinemaAppContext context)
        {
            _context = context;
        }

        // GET: AvailableMovies
        public async Task<IActionResult> Index(string id, string movieTitle)
        {
            // Use LINQ to get list of movie titles.
            IQueryable<string> titleQuery = from m in _context.Movie
                                            orderby m.MovieTitle
                                            select m.MovieTitle;

            ICollection<AvailableMovies> availableMovies = _context.AvailableMovies.ToList();
            foreach (var movie in availableMovies)
            {
                movie.Movie = _context.Movie.FirstOrDefault(x => x.Id == movie.MovieId);
            }

            if (!String.IsNullOrEmpty(id))
            {
                DateTime oDate = Convert.ToDateTime(id);
                availableMovies = availableMovies.Where(s => s.AvailableDate.Day == oDate.Day
                                                             && s.AvailableDate.Month == oDate.Month
                                                             && s.AvailableDate.Year == oDate.Year).ToList();
            }

            if (!String.IsNullOrEmpty(movieTitle))
            {
                Movie movie = _context.Movie.FirstOrDefault(m => m.MovieTitle.Contains(movieTitle));
                if (movie != null)
                    availableMovies = availableMovies.Where(m => m.MovieId == movie.Id).ToList();
            }

            var movieGenreVm =
                new MovieGenreViewModel
                {
                    Titles = new SelectList(await titleQuery.Distinct().ToListAsync()),
                    Movies = availableMovies.ToList()
                };

            return View(movieGenreVm);
        }

        // GET: AvailableMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableMovies = await _context.AvailableMovies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (availableMovies == null)
            {
                return NotFound();
            }

            availableMovies.Movie = _context.Movie.FirstOrDefault(x => x.Id == availableMovies.MovieId);

            return View(availableMovies);
        }

        // GET: AvailableMovies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvailableMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,AvailableDate,AvailableSeats")] AvailableMovies availableMovies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availableMovies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(availableMovies);
        }

        // GET: AvailableMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableMovies = await _context.AvailableMovies.SingleOrDefaultAsync(m => m.Id == id);
            if (availableMovies == null)
            {
                return NotFound();
            }
            return View(availableMovies);
        }

        // POST: AvailableMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,AvailableDate,AvailableSeats")] AvailableMovies availableMovies)
        {
            if (id != availableMovies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availableMovies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailableMoviesExists(availableMovies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(availableMovies);
        }

        // GET: AvailableMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableMovies = await _context.AvailableMovies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (availableMovies == null)
            {
                return NotFound();
            }

            return View(availableMovies);
        }

        // POST: AvailableMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availableMovies = await _context.AvailableMovies.SingleOrDefaultAsync(m => m.Id == id);
            _context.AvailableMovies.Remove(availableMovies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableMoviesExists(int id)
        {
            return _context.AvailableMovies.Any(e => e.Id == id);
        }
    }
}