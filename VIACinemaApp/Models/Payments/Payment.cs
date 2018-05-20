using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VIACinemaApp.Models.Payments
{
    public class Payment
    {
        public int Id { get; set; }

        [DisplayName("Credit Card Number")]
        [Required]
        [CreditCard]
        public string CredictCard { get; set; }

        [DisplayName("Name")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$",
            ErrorMessage = "Please enter a valid Name. No special characters are allowed.")]
        public string OwnerName { get; set; }

        [DisplayName("Surname")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$",
            ErrorMessage = "Please enter a valid Surname. No special characters are allowed.")]
        public string OwnerSurname { get; set; }

        [DisplayName("Cvv")]
        [Required]
        [MinLength(3, ErrorMessage = "Please enter valid Security Code")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please enter valid Security Code")]
        public string SecurityCode { get; set; }

        [DisplayName("Expiry Month")]
        [Required]
        public string ExpiryMonth { get; set; }

        [DisplayName("Expiry Year")]
        [Required]
        public string ExpiryYear { get; set; }
    }
}