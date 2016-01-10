using Time_Clock_Web.Models;

namespace Time_Clock_Web.Logic
{
    public class PayStubFactory
    {
        public PayStub NewPayStub(Employee employee, double normalHours, double holidayHours, decimal earnings = 0)
        {
            return new PayStub
            {

                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.FullName,
                Wage = employee.Wage,
                NormalHours = normalHours,
                HolidayHours = holidayHours,
                Earnings = earnings
            };


        }
    }
}