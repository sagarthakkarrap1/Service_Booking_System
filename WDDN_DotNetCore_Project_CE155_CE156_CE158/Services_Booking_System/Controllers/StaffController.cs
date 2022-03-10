using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Services_Booking_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepo;
        private readonly IServiceRepository _serviceRepo;
        private readonly IFlashMessage flashMessage;
        public StaffController(IStaffRepository staffRepo, IServiceRepository serviceRepo, IFlashMessage flashMessage)
        {
            _staffRepo = staffRepo;
            _serviceRepo = serviceRepo;
            this.flashMessage = flashMessage;
        }
        public IActionResult Index()
        {
            var model = _staffRepo.GetAllStaffs();
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public ViewResult Create()
        {
            var services = _serviceRepo.GetServices();
            ViewData["ServiceId"] = new SelectList(services, "Id", "ServiceName");
            return View();
        }
        [AllowAnonymous]

        [HttpPost]
        public IActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                Staff newDpt = _staffRepo.Add(staff);
                flashMessage.Confirmation("We will get back to you !");
                return RedirectToAction("index","home");
            }
            flashMessage.Danger("Please apply again!");
            return View();
        }

        public ViewResult Details(int id)
        {
            Staff staff = _staffRepo.GetStaff(id);
            if (staff == null)
            {
                Response.StatusCode = 404;
                return View("StaffNotFound", id);
            }
            return View(staff);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Staff staff = _staffRepo.GetStaff(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var staff = _staffRepo.GetStaff(id);
            _staffRepo.Delete(staff.Id);
            flashMessage.Danger("Deleted successfully");
            return RedirectToAction("Index");
        }
    }
}
