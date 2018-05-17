using System.Collections.Generic;
using System.Threading.Tasks;
using VIACinemaApp.Models.Movies;

namespace VIACinemaApp.Repositories.Interfaces
{
    /// <summary>
    ///     ISeatsRepository is used to make domain object persistance more generic and make it
    ///     easier to manage objects.
    /// </summary>
    public interface ISeatsRepository
    {
        /// <summary>
        ///     Get seat records by available movie Id.
        /// </summary>
        /// <param name="id"> id of the available movie record to the seats belong to.</param>
        /// <returns>Collection of seats object associated to the the given available movie id.</returns>
        Task<IEnumerable<Seat>> GetSeats(string id);
    }
}