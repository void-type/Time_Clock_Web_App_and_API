using System;
using System.Collections.Generic;
using Time_Clock_Web.Models;
using Time_Clock_Web.Repositories.Core;

namespace Time_Clock_Web.Repositories.TimeClock
{
    public interface IShiftRepository : IRepository<Shift>
    {
        IEnumerable<Shift> GetRange(DateTime startDateTime, DateTime endDateTime);

        IEnumerable<Shift> GetAllWithEmployees();

        IEnumerable<Shift> GetForEmployee(string id);
    }
}
