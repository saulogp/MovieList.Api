using System.ComponentModel.DataAnnotations;

namespace MovieList.ViewModels
{
    public class CreateMoviesModel
    {
            [Required]
            public string Title { get; set; }
    }
}