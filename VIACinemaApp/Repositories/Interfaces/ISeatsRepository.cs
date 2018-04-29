using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Repositories.Interfaces
{
    public interface ISeatsRepository
    {
        Task<IEnumerable<Seat>> GetSeats(string id);

        Task<Seat> GetSeat(int? id);

        Task<Seat> EditSeat(int? id);

        void EditSeat(int id, Seat seat);

        bool SeatExists(int id);
    }
}