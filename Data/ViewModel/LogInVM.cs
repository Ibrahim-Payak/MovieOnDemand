using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.ViewModel
{
    public class LogInVM
    {
        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
