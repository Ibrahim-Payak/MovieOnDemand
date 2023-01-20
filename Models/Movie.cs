using MovieOnDemand.Data;
using MovieOnDemand.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public string ImageUrl { get; set; }

        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
        //Cinema Relationships
        public Cinema Cinema { get; set; }
        //EF is consider it as an foreign key
        [ForeignKey("CinemaId")]
        public int CinemaId { get; set; }

        //Producer Relationships
        public Producer Producer { get; set; }
        //EF is consider it as an foreign key
        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }
    }
}
