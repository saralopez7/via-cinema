using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VIACinemaApp.Models;
using VIACinemaApp.Models.Payments;

namespace VIACinemaApp.Repositories.Interfaces
{
    public interface IPaymentsRepository
    {
        int CreatePayment(string userId, Payment payment);

        PaymentViewModel PaymentDetails(int? id);
    }
}