using System;
namespace Fall2024_Assignment3_iqstevens.Models
{
	public class MovieDVM
	{
		public Movie Movie { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public List<ReviewSent>? Reviewsents { get; set; }

        public class ReviewSent
        {
            public string? ReviewText { get; set; }
            public double SentScore { get; set; }
        }


        public MovieDVM(Movie movie, IEnumerable<Actor> actors, List<ReviewSent> reviewsents)
		{
			Movie = movie;
			Actors = actors;
			Reviewsents = reviewsents;
		}

		

    }
}

