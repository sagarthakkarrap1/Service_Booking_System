using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class Staff
    {
        public int Id { get; set; }

        [Display(Name = "Staff Name")]
        [Required]
        public string StaffName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        public string MobileNumber { get; set; }


        public int ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
