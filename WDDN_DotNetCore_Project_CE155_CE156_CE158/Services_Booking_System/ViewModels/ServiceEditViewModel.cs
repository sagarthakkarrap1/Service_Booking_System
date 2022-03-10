using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.ViewModels
{
    public class ServiceEditViewModel
    {
        [Required]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }
        [Display(Name = "Service Image")]
        public IFormFile ServiceImage { get; set; }
        public int Id { get; set; }

        public string ExitstingPhotoPath { get; set; }
    }
}
