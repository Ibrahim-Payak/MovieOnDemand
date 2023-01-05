using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    public class Cinema
    {
        [Key]
        public int CinemaId { get; set; }
        public string CinemaLogo { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
