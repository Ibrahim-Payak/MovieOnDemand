using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        //order can have multiple order items

        public List<OrderItem> OrderItems { get; set; }
    }
}
