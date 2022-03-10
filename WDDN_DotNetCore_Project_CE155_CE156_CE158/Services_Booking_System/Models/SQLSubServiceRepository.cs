using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class SQLSubServiceRepository : ISubServiceRepository
    {
        private readonly AppDbContext context;

        public SQLSubServiceRepository(AppDbContext context)
        {
            this.context = context;
        }
        SubService ISubServiceRepository.Add(SubService SubService)
        {
            context.SubServices.Add(SubService);
            context.SaveChanges();
            return SubService;
        }

        SubService ISubServiceRepository.Delete(int id)
        {
            SubService SubServices = context.SubServices.Find(id);
            if (SubServices != null)
            {
                context.SubServices.Remove(SubServices);
                context.SaveChanges();
            }
            return SubServices;
        }

        IEnumerable<SubService> ISubServiceRepository.GetSubServices()
        {
            return context.SubServices.Include(s => s.Service);
        }
        IEnumerable<SubService> ISubServiceRepository.GetSubServicesOfService(int serviceId)
        {
            return context.SubServices.Include(s => s.Service).Where(sub => sub.ServiceId == serviceId);
        }
        SubService ISubServiceRepository.GetSubService(int id)
        {
            return context.SubServices.Include(s => s.Service).FirstOrDefault(m => m.Id == id);
        }




        SubService ISubServiceRepository.Update(SubService SubServicesChanges)
        {
            var Subservices = context.SubServices.Attach(SubServicesChanges);
            Subservices.State = EntityState.Modified;
            //context.Service.Update(ServicesChanges);
            context.SaveChanges();
            return SubServicesChanges;
        }

        public IEnumerable<SubService> GetSubServices()
        {
            return context.SubServices;
        }
    }
}
