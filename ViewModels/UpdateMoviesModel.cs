using System;
using MovieList.Models.Enum;

namespace MovieList.ViewModels
{
    public class UpdateMoviesModel
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public string Synopsis { get; set; }
        public Genre Genre { get; set; }
    }
}