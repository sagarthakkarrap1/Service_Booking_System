using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services_Booking_System.Models
{
    public interface IStaffRepository
    {
        Staff GetStaff(int id);

        IEnumerable<Staff> GetAllStaffs();

        Staff Add(Staff Staffs);

        Staff Update(Staff StaffsChanges);

        Staff Delete(int id);
    }
}
