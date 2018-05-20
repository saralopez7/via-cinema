using System.ComponentModel.DataAnnotations;

namespace VIACinemaApp.Models.Movies
{
    public enum Directors
    {
        [Display(Name = "Jon Olssen")]
        Jon_Olssen,

        [Display(Name = "Sara Lopez")]
        Sara_Lopez,

        [Display(Name = "Frank Daniels")]
        Frank_Daniels,

        [Display(Name = "Jon Snow")]
        Jon_Snow,

        [Display(Name = "Vladimir Rick")]
        Vladimir_Rick,

        [Display(Name = "Alexa Daniels")]
        Alexa_Daniels,

        [Display(Name = "Jack Daniels")]
        Jack_Daniels,

        [Display(Name = "Jack Morrison")]
        Jack_Morisson,

        [Display(Name = "Electra")]
        Electra,

        [Display(Name = "Stephan King")]
        Stephan_King
    }
}