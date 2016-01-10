using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Time_Clock_Web.Models;
using Time_Clock_Web.Repositories.Core;

namespace Time_Clock_Web.Repositories.TimeClock
{
    internal class IncompleteShiftRepository : Repository<IncompleteShift>, IIncompleteShiftRepository
    {
        public IncompleteShiftRepository(DbContext context) : base(context)
        {
        }

        public override IncompleteShift Get(string id)
        {
            return GetContext.IncompleteShifts.Include(s => s.Employee).SingleOrDefault(s => s.EmployeeId.Equals(id));
        }


        public IEnumerable<IncompleteShift> GetAllWithEmployees()
        {
            return GetContext.IncompleteShifts.Include(s => s.Employee).OrderByDescending(s => s.TimeStampIn).ToList();
        }


    }
}
