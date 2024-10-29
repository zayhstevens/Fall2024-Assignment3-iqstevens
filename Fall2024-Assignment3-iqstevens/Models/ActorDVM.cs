using System;
namespace Fall2024_Assignment3_iqstevens.Models
{
	public class ActorDVM
	{
        public Actor Actor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public List<string>? Tweets { get; set; }



        public ActorDVM(Actor actor, IEnumerable<Movie> movies, List<string> tweets)
        {
            Actor = actor;
            Movies = movies;
            Tweets = tweets;
        }

        
    }
}

