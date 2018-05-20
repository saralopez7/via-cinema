using AutoMapper;
using VIACinemaApp.Models.Movies;
using VIACinemaApp.Models.Transactions;

namespace VIACinemaApp
{
    public class Mapper
    {
        private static readonly IMapper Instance;

        static Mapper()
        {
            var config = new MapperConfiguration(cfg =>

             {
                 cfg.CreateMap<AvailableMovie, Movie>()
                     .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.MovieTitle))
                     .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Movie.Director))
                     .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Movie.Duration))
                     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Movie.Id))
                     .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Movie.Genre))
                     .ForMember(dest => dest.Plot, opt => opt.MapFrom(src => src.Movie.Plot))
                     .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Movie.ReleaseDate))
                     .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Movie.Rating)).ReverseMap();

                 cfg.CreateMap<Transaction, TransactionViewModel>()
                     .ForPath(dest => dest.Movie.Id, opt => opt.MapFrom(src => src.MovieId));
             });

            Instance = config.CreateMapper();
        }

        public static T1 Map<T1>(object o) => Instance.Map<T1>(o);
    }
}