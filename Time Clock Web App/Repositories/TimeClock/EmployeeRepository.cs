using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Time_Clock_Web.Models;
using Time_Clock_Web.Repositories.Core;

namespace Time_Clock_Web.Repositories.TimeClock
{
    internal class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Employee> GetAll()
        {
            return base.GetAll().OrderBy(e => e.Disabled).ThenBy(e => e.LastName);
        }

        public Employee GetWithShifts(string id)
        {
            return GetContext.Employees.Include(e => e.Shifts).SingleOrDefault(e => e.EmployeeId.Equals(id));
        }

        public IEnumerable<Employee> GetAllWithShifts()
        {
            return GetContext.Employees.Include(e => e.Shifts).OrderBy(e => e.LastName).ToList();
        }

        public Employee SearchByUserIdentity(string id)
        {
            return SingleOrDefault(e => e.AspIdentity.Equals(id));
        }

        public Employee SearchByCardNumberOrId(string id)
        {
            var emp = GetContext.Employees.SingleOrDefault(x => x.CardNumber.Equals(id));
            if (emp == null || emp.EmployeeId.IsNullOrWhiteSpace())
            {
                emp = GetContext.Employees.Find(id);
            }
            return emp;
        }

        public void Disable(string id)
        {
            Get(id).Disabled = true;
        }

    }
}