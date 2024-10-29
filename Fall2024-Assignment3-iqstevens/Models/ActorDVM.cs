using System;
namespace Fall2024_Assignment3_iqstevens.Models
{
	public class ActorDVM
	{
        public Actor Actor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }


        public ActorDVM(Actor actor, IEnumerable<Movie> movies)
        {
            Actor = actor;
            Movies = movies;
        }
    }
}

