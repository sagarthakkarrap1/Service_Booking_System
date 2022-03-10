using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Services_Booking_System.Controllers
{
    
    public class BookingController : Controller
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ISubServiceRepository _subserviceRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IFlashMessage flashMessage;
        public BookingController(IBookingRepository bookingRepository, ISubServiceRepository _subserviceRepo, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IFlashMessage flashMessage)
        {
            this.bookingRepository = bookingRepository;
            this._subserviceRepo = _subserviceRepo;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.flashMessage = flashMessage;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                SubService Subservice = _subserviceRepo.GetSubService(Convert.ToInt32(id));
                Booking newBooking = new Booking{
                    ApplicationUserId = user.Id,
                    Date = DateTime.Now,
                    Price = Subservice.Price,
                    SubServiceId = Convert.ToInt32(id)
                };
                bookingRepository.Add(newBooking);
                flashMessage.Confirmation("Booked Successfully");
                return RedirectToAction("GetBookings", "Booking");
            }
            return RedirectToAction("GetBookings");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var user = await userManager.GetUserAsync(User);
            
            if (user != null)
            {
                var model = bookingRepository.GetAllBookingsOfCustomer(user.Id);

                return View(model);
            }
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetBookingsAdmin()
        {
            var model = bookingRepository.GetAllBookings();
            return View(model);  
        }
    }
}
