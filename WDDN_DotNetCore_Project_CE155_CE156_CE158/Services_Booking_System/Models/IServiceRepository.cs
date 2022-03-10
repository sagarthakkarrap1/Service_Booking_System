using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public interface IServiceRepository
    {
        Service GetService(int id);

        IEnumerable<Service> GetServices();

        IEnumerable<SubService> GetSubServices();

        Service Add(Service Service);

        Service Update(Service ServicesChanges);

        Service Delete(int id);

        IEnumerable<Staff> GetStaffs();

    }
}
