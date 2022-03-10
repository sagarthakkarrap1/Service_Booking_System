using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services_Booking_System.Models;
using Services_Booking_System.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Services_Booking_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubServicesController : Controller
    {
        private readonly ISubServiceRepository _subserviceRepo;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IServiceRepository _serviceRepo;
        private readonly IFlashMessage flashMessage;
        public SubServicesController(ISubServiceRepository subServiceRepo, IHostingEnvironment hostingEnvironment, IServiceRepository serviceRepo, IFlashMessage flashMessage)
        {

            _subserviceRepo = subServiceRepo;
            this.hostingEnvironment = hostingEnvironment;
            _serviceRepo = serviceRepo;
            this.flashMessage = flashMessage;
        }
        public IActionResult Index()
        {
            var services = _serviceRepo.GetServices();
            ViewData["ServiceId"] = new SelectList(services, "Id", "ServiceName");
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult List(int Serviceid)
        {
            var model = _subserviceRepo.GetSubServicesOfService(Serviceid);
            ViewData["SelectedService"] = _serviceRepo.GetService(Convert.ToInt32(Serviceid)).ServiceName;
            return View(model);
        }
        [HttpPost]
        public IActionResult List(IFormCollection form)
        {

            //return Convert.ToInt32(form["SelectedService"]);
            if (form != null)
            {
                var model = _subserviceRepo.GetSubServicesOfService(Convert.ToInt32(form["SelectedService"]));
                ViewData["SelectedService"] = _serviceRepo.GetService(Convert.ToInt32(form["SelectedService"])).ServiceName;
                return View(model);
            }

            return View();
        }
        [HttpGet]
        public ViewResult Create()
        {
            var services = _serviceRepo.GetServices();
            ViewData["ServiceId"] = new SelectList(services, "Id", "ServiceName");
            return View();
        }

        [HttpPost]

        public IActionResult Create(SubServiceCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.SubServiceImage != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "SubServiceImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SubServiceImage.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.SubServiceImage.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                SubService newSubService = new SubService
                {
                    SubServiceName = model.SubServiceName,
                    SubServiceImage = uniqueFileName,
                    Price = model.Price,
                    ServiceId = model.ServiceId
                };
                _subserviceRepo.Add(newSubService);
                flashMessage.Confirmation("SubService Added Successfully");
                return RedirectToAction("Details", new { id = newSubService.Id });
            }
            return View();
        }

        public ViewResult Details(int id)
        {
            SubService Subservice = _subserviceRepo.GetSubService(id);
            if (Subservice == null)
            {
                Response.StatusCode = 404;
                return View("SubServiceNotFound", id);
            }
            return View(Subservice);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            SubService Subservice = _subserviceRepo.GetSubService(id);
            if (Subservice == null)
            {
                return NotFound();
            }
            return View(Subservice);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var Subservice = _subserviceRepo.GetSubService(id);
            _subserviceRepo.Delete(Subservice.Id);
            flashMessage.Danger("SubService Deleted Successfully");
            return RedirectToAction("Index");
        }

        [HttpGet]

        public ViewResult Edit(int id)
        {
            SubService Subservice = _subserviceRepo.GetSubService(id);
            SubServiceEditViewModel subServiceEditViewModel = new SubServiceEditViewModel
            {
                ExitstingPhotoPath = Subservice.SubServiceImage,
                Id = Subservice.Id,
                SubServiceName = Subservice.SubServiceName,
                Price = Subservice.Price,
                ServiceId = Subservice.ServiceId

            };
            var services = _serviceRepo.GetServices();
            ViewData["ServiceId"] = new SelectList(services, "Id", "ServiceName");
            //SubService subService = _subserviceRepo.GetSubService(id);
            return View(subServiceEditViewModel);
        }

        [HttpPost]

        public IActionResult Edit(SubServiceEditViewModel model)
        {
            //ViewBag.IsPostBack = IsPost();
            var services = _serviceRepo.GetServices();
            ViewData["ServiceId"] = new SelectList(services, "Id", "ServiceName");
            if (ModelState.IsValid)
            {
                SubService subService = _subserviceRepo.GetSubService(model.Id);
                subService.SubServiceName = model.SubServiceName;
                subService.Price = model.Price;
                subService.ServiceId = model.ServiceId;

                if (model.SubServiceImage != null)
                {
                    if (model.ExitstingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "SubServiceImages", model.ExitstingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    subService.SubServiceImage = ProcessUploadedFile(model);

                }
                SubService updatedService = _subserviceRepo.Update(subService);
                flashMessage.Confirmation("SubService Updated Successfully");
                //ModelState.AddModelError("success", "Updated sucessfully");
                return RedirectToAction("Index");
            }
            //else
            //{
            //    ModelState.AddModelError("success", string.Empty);
            //}

            return View(model);
        }
        private bool IsPost()
        {
            return HttpContext.Request.Method == "POST";
        }
        private string ProcessUploadedFile(SubServiceEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.SubServiceImage != null)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "SubServiceImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SubServiceImage.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.SubServiceImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
