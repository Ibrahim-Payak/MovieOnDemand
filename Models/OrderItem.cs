using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }


        //relation with other model
        public Movie Movie { get; set; }

        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        
        public Order Order { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
    }
}
