using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class SQLServiceRepository : IServiceRepository
    {
        private readonly AppDbContext context;

        public SQLServiceRepository(AppDbContext context)
        {
            this.context = context;
        }
        Service IServiceRepository.Add(Service Service)
        {
            context.Services.Add(Service);
            context.SaveChanges();
            return Service;
        }

        Service IServiceRepository.Delete(int id)
        {
            Service Services = context.Services.Find(id);
            if (Services != null)
            {
                context.Services.Remove(Services);
                context.SaveChanges();
            }
            return Services;
        }

        IEnumerable<Service> IServiceRepository.GetServices()
        {
            return context.Services;
        }

        Service IServiceRepository.GetService(int id)
        {
            return context.Services.FirstOrDefault(m => m.Id == id);
        }


        IEnumerable<Staff> IServiceRepository.GetStaffs()
        {
            return context.Staffs;
        }

        Service IServiceRepository.Update(Service ServicesChanges)
        {
            var services = context.Services.Attach(ServicesChanges);
            services.State = EntityState.Modified;
            //context.Service.Update(ServicesChanges);
            context.SaveChanges();
            return ServicesChanges;
        }

        public IEnumerable<SubService> GetSubServices()
        {
            return context.SubServices;
        }
    }
}
