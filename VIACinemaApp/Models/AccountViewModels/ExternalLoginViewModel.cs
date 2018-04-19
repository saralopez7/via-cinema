using System.ComponentModel.DataAnnotations;

namespace VIACinemaApp.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}