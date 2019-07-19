using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Select your favorite park:")]
        public string ParkCode { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Not a valid email address.")]
        [Display(Name = "Enter your email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "What state are you from?")]
        public string State { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "How active are you?")]
        public string ActivityLevel { get; set; }

    }
}
