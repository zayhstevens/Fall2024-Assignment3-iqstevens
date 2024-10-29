using System;
namespace Fall2024_Assignment3_iqstevens.Models
{
	public class MovieDVM
	{
		public Movie Movie { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public List<string>? Reviews { get; set; }


        public MovieDVM(Movie movie, IEnumerable<Actor> actors, List<string> reviews)
		{
			Movie = movie;
			Actors = actors;
			Reviews = reviews;
		}

		

    }
}

