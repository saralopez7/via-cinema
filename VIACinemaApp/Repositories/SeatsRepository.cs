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
    }
}