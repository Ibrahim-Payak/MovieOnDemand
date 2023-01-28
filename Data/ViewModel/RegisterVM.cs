using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should be match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
