using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class SubService
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "SubService Name")]
        public string SubServiceName { get; set; }
        [Required]
        [Display(Name = "SubService Image")]
        public string SubServiceImage { get; set; }
        [Required]
        public int Price { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
