using System.Collections.Generic;

namespace ControllersAndActions.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DirectorName { get; set; }

        public static Movie[] GetMovies()
        {
            return new List<Movie>
                {
                    new Movie {Title = "Blade runner", Description = "Robots...", DirectorName = "Some director 1"},
                    new Movie {Title = "Matrix", Description = "Red or blue pill...", DirectorName = "Matrix director"},
                    new Movie {Title = "The big boss", Description = "Bruce Lee kicks ass", DirectorName = "Bruce Lee"},

                }.ToArray();
        }

    }

}