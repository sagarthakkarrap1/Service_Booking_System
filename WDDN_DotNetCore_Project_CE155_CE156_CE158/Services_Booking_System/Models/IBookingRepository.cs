using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAllBookingsOfCustomer(string id);
        IEnumerable<Booking> GetAllBookings();
        Booking Add(Booking Booking);
    }
}
