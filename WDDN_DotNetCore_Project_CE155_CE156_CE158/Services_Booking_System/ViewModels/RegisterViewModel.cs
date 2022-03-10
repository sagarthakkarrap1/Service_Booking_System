using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "phone number should be 10 digit"), MinLength(10, ErrorMessage = "phone number should be 10 digit")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",
            ErrorMessage = "password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
