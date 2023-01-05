using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    //many to many
    public class Actor_Movie
    {
        public int MovieId { get; set; }
        //Actor_Movie have single movie
        public Movie Movie { get; set; }
        public int ActorId { get; set; }
        //Actor_Movie have single Actor
        public Actor Actor { get; set; }
    }
}
