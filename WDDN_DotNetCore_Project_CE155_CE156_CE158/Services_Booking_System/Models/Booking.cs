using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int SubServiceId { get; set; }

        public SubService SubService { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
