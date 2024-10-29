using System;
namespace Fall2024_Assignment3_iqstevens.Models
{
	public class ActorDVM
	{
        public Actor Actor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public List<TweetSent>? TweetSents { get; set; }

        public class TweetSent
        {
            public string? Username { get; set; }
            public string? Tweet { get; set; }
            public double SentScore { get; set; }
        }

        public ActorDVM(Actor actor, IEnumerable<Movie> movies, List<TweetSent> tweets)
        {
            Actor = actor;
            Movies = movies;
            TweetSents = tweets;
        }

        
    }
}

