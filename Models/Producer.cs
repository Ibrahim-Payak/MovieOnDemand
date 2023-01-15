using MovieOnDemand.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Profile Picture")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "Actor Name")]
        public string FullName { get; set; }
        public string Bio { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
