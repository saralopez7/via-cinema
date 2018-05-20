using System.ComponentModel.DataAnnotations;

namespace VIACinemaApp.Models.Movies
{
    public enum MovieTitles
    {
        Deadpool,

        [Display(Name = "Deadpool 2")]
        Deadpool_2,

        Avengers,

        [Display(Name = "Avengers 2")]
        Avengers_2,

        [Display(Name = "Doctor Strange")]
        Doctor_Strange,

        Up,
        Entourage,
        //Flamekeeper,
        //The100,

        //[Display(Name = "They are billions")]
        //They_are_billions,

        //SOS,
        //Payday,
        MadMax,

        //SiSenor,
        //Google,

        //[Display(Name = "The wild 8")]
        //The_wild_8,

        //Hackerman,
        //CSI,
        Blindspot,

        [Display(Name = "Avengers Infinity War")]
        Avengers_infinity_war
    }
}