using System.Collections.Generic;
using Time_Clock_Web.Models;
using Time_Clock_Web.Repositories.Core;

namespace Time_Clock_Web.Repositories.TimeClock
{
    public interface IIncompleteShiftRepository : IRepository<IncompleteShift>
    {
        IEnumerable<IncompleteShift> GetAllWithEmployees();

    }
}
