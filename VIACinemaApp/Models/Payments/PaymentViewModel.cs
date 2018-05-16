using System.Collections.Generic;
using VIACinemaApp.Models.Transactions;

namespace VIACinemaApp.Models.Payments
{
    public class PaymentViewModel
    {
        public List<Transaction> Transactions;
        public Payment Payment;
    }
}