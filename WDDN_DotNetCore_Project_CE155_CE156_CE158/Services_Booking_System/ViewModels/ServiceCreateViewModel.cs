using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.ViewModels
{
    public class ServiceCreateViewModel
    {
        [Required]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }
        [Required]
        [Display(Name = "Service Image")]
        public IFormFile ServiceImage { get; set; }

    }
}
