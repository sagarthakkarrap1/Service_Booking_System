using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceRepository _serviceRepo;

        public HomeController(IServiceRepository ServiceRepo)
        {
            _serviceRepo = ServiceRepo;
        }

        public IActionResult Index()
        {
            var model = _serviceRepo.GetServices();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
