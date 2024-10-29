using System;
namespace Fall2024_Assignment3_iqstevens.Models
{
	public class MovieDVM
	{
		public Movie Movie { get; set; }
        public IEnumerable<Actor> Actors { get; set; }


        public MovieDVM(Movie movie, IEnumerable<Actor> actors)
		{
			Movie = movie;
			Actors = actors;
		}
	}
}

