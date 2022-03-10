using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public interface ISubServiceRepository
    {
        SubService GetSubService(int id);

        IEnumerable<SubService> GetSubServices();

        SubService Add(SubService SubService);

        SubService Update(SubService SubServicesChanges);

        SubService Delete(int id);
        IEnumerable<SubService> GetSubServicesOfService(int serviceId);
    }
}
