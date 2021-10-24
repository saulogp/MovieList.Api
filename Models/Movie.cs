using System;
using MovieList.Models.Enum;

namespace MovieList.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public string Synopsis { get; set; }
        public Genre Genre { get; set; }
    }   
}