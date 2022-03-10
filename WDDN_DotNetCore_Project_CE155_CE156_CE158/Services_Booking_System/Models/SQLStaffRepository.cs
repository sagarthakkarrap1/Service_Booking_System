using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public class SQLStaffRepository : IStaffRepository
    {
        private readonly AppDbContext context;

        public SQLStaffRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Staff Add(Staff Staffs)
        {
            context.Staffs.Add(Staffs);
            context.SaveChanges();
            return Staffs;
        }

        public Staff Delete(int id)
        {
            Staff Staffs = context.Staffs.Find(id);
            if (Staffs != null)
            {
                context.Staffs.Remove(Staffs);
                context.SaveChanges();
            }
            return Staffs;
        }

        public IEnumerable<Staff> GetAllStaffs()
        {
            return context.Staffs.Include(s => s.Service);
        }

        public Staff GetStaff(int id)
        {
            return context.Staffs.Include(s => s.Service).FirstOrDefault(staff => staff.Id == id);
        }

        public Staff Update(Staff StaffsChanges)
        {
            context.Staffs.Update(StaffsChanges);
            context.SaveChanges();
            return StaffsChanges;
        }
    }
}
