using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class SQLBookingRepository :IBookingRepository
    {
        private readonly AppDbContext context;

        public SQLBookingRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Booking Add(Booking Booking)
        {
            context.Bookings.Add(Booking);
            context.SaveChanges();
            return Booking;
        }
        public IEnumerable<Booking> GetAllBookingsOfCustomer(string id)
        {
            return context.Bookings.Include(s => s.SubService).Where(c => c.ApplicationUserId == id).OrderByDescending(d=>d.Date);
        }
        public IEnumerable<Booking> GetAllBookings()
        {
            return context.Bookings.Include(s => s.SubService).Include(a=> a.ApplicationUser).OrderByDescending(d => d.Date);
        }
    }
}
