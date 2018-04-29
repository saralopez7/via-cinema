using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Data;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Repositories.Interfaces;

namespace VIACinemaApp.Repositories
{
    public class SeatsRepository : ISeatsRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetSeats(string id)
        {
            if (string.IsNullOrEmpty(id))
                return await _context.Seats.ToListAsync();
            return await _context.Seats.Where(x => x.MovieId == int.Parse(id)).ToListAsync();
        }

        public async Task<Seat> GetSeat(int? id)
        {
            return await _context.Seats
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Seat> EditSeat(int? id)
        {
            return await _context.Seats.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async void EditSeat(int id, Seat seat)
        {
            _context.Update(seat);
            await _context.SaveChangesAsync();
        }

        public bool SeatExists(int id)
        {
            return _context.Seats.Any(e => e.Id == id);
        }
    }
}