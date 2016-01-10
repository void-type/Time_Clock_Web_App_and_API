using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Time_Clock_Web.Models;
using Time_Clock_Web.Repositories.Core;

namespace Time_Clock_Web.Repositories.TimeClock
{
    class ShiftRepository : Repository<Shift>, IShiftRepository
    {
        public ShiftRepository(DbContext context) : base(context)
        {
        }

        public override Shift Get(string id)
        {

            return GetContext.Shifts.Include(s => s.Employee).SingleOrDefault(s => s.Id.Equals(id));

        }

        public IEnumerable<Shift> GetRange(DateTime startDateTime, DateTime endDateTime)
        {
            return GetContext.Shifts.Where(
                s => DateTime.Compare(s.TimeStampIn, startDateTime) >= 0
                && DateTime.Compare(s.TimeStampIn, endDateTime) < 0)
                .Include(s => s.Employee).OrderBy(s => s.TimeStampIn).ToList();
        }

        public IEnumerable<Shift> GetAllWithEmployees()
        {
            return GetContext.Shifts.Include(s => s.Employee).OrderByDescending(s => s.TimeStampIn).ToList();
        }

        public IEnumerable<Shift> GetForEmployee(string id)
        {
            return GetContext.Shifts.Where(x => x.EmployeeId.Equals(id)).Include(x => x.Employee).OrderByDescending(x => x.TimeStampIn).ToList();
        }


    }
}
