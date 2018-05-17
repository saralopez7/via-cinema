namespace VIACinemaApp.Models.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public string CredictCard { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public int SecurityCode { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
    }
}