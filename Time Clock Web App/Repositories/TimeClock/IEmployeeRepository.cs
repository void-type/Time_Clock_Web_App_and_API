using System.Collections.Generic;
using Time_Clock_Web.Models;
using Time_Clock_Web.Repositories.Core;

namespace Time_Clock_Web.Repositories.TimeClock
{
    public interface IEmployeeRepository : IRepository<Employee>

    {
        Employee GetWithShifts(string id);

        IEnumerable<Employee> GetAllWithShifts();

        Employee SearchByUserIdentity(string id);

        Employee SearchByCardNumberOrId(string id);

        void Disable(string id);
    }
}
