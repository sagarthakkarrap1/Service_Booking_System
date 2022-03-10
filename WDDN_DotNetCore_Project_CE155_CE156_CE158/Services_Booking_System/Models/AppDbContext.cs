using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<SubService> SubServices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
