using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFlashMessage flashMessage;
        public ServicesController(IServiceRepository ServiceRepo, IHostingEnvironment hostingEnvironment, IFlashMessage flashMessage)
        {
            _serviceRepo = ServiceRepo;
            this.hostingEnvironment = hostingEnvironment;
            this.flashMessage = flashMessage;
        }

        public IActionResult Index()
        {
            var model = _serviceRepo.GetServices();
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.ServiceImage != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "ServiceImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ServiceImage.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.ServiceImage.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Service newService = new Service
                {
                    ServiceName = model.ServiceName,
                    ServiceImage = uniqueFileName
                };
                _serviceRepo.Add(newService);
                flashMessage.Confirmation("Service Added Successfully");
                return RedirectToAction("index");
            }
            return View();
        }
        [AllowAnonymous]
        public ViewResult Details(int id)
        {
            Service service = _serviceRepo.GetService(id);
            if (service == null)
            {
                Response.StatusCode = 404;
                return View("StaffNotFound", id);
            }
            return View(service);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Service service = _serviceRepo.GetService(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var service = _serviceRepo.GetService(id);
            _serviceRepo.Delete(service.Id);
            flashMessage.Danger("Service Deleted Successfully");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Service service = _serviceRepo.GetService(id);
            ServiceEditViewModel serviceEditViewModel = new ServiceEditViewModel
            {
                ExitstingPhotoPath = service.ServiceImage,
                Id = service.Id,
                ServiceName = service.ServiceName
            };
            return View(serviceEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ServiceEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Service service = _serviceRepo.GetService(model.Id);
                service.ServiceName = model.ServiceName;


                if (model.ServiceImage != null)
                {
                    if (model.ExitstingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "ServiceImages", model.ExitstingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    service.ServiceImage = ProcessUploadedFile(model);

                }
                Service updatedService = _serviceRepo.Update(service);
                flashMessage.Confirmation("Service Updated Successfully");
                return RedirectToAction("Index");
            }

            return View(model);
        }
        private string ProcessUploadedFile(ServiceEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.ServiceImage != null)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "ServiceImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ServiceImage.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ServiceImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
