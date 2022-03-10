using Microsoft.AspNetCore.Http;
using Services_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.ViewModels
{
    public class SubServiceCreateViewModel
    {
        [Required]
        [Display(Name = "SubService Name")]
        public string SubServiceName { get; set; }
        [Required]
        [Display(Name = "SubService Image")]
        public IFormFile SubServiceImage { get; set; }
        [Required]
        public int Price { get; set; }
        [Display(Name = "Service Name")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
