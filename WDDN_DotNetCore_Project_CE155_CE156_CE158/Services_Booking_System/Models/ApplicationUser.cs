using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
