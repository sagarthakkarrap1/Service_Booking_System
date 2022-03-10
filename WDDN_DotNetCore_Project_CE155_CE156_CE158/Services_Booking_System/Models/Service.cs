using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }
        [Required]
        [Display(Name = "Service Image")]
        public string ServiceImage { get; set; }

        public ICollection<Staff> Staffs { get; set; }

        public ICollection<SubService> SubServices { get; set; }

    }
}
