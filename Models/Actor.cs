
using MovieOnDemand.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Profile Picture")]
        [Required(ErrorMessage = "Provide Image URL")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Actor Name")]
        [Required(ErrorMessage = "Provide Actor Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Name must be b/w 3 to 50")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Provide Bio")]
        public string Bio { get; set; }

        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
