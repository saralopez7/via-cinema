using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VIACinemaApp.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }

        [EmailAddress]
        public override string Email { get; set; }

        [Required]
        public string Username { get; set; }

        public string Password { get; set; }

        [Phone]
        public override string PhoneNumber { get; set; }

        public string Name { get; set; }
    }
}