using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.ViewModel
{
    public class MovieVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Movie Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Movie Description is required")]
        public string Description { get; set; }

        [Display(Name = "Select a Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Select a End Date")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Select a Movie Category")]
        [Required(ErrorMessage = "Movie Category is required")]
        public MovieCategory MovieCategory { get; set; }

        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Movie Poster is required")]
        public string ImageUrl { get; set; }

        //Relationships
        [Display(Name = "Select a Actor(s)")]
        [Required(ErrorMessage = "Actor(s) is required")]
        public List<int> ActorIds { get; set; }

        [Display(Name = "Select a Cinema")]
        [Required(ErrorMessage = "Cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a Producer")]
        [Required(ErrorMessage = "Producer is required")]
        public int ProducerId { get; set; }
    }
}
