using System;
using System.Collections.Generic;
using System.Linq;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Logic
{
    /// <summary>
    ///     PayRollCalculator processes the shifts and employee information to return PayStubs
    ///     Business Logic
    /// </summary>
    class PayrollCalculator
    {
        private TimeClockUnitOfWork db;

        public PayrollCalculator(TimeClockUnitOfWork uow)
        {
            db = uow;
        }

        public List<PayStub> GetPayroll(DateTime startOfWeek, int numberOfWeeks)
        {
            var factory = new PayStubFactory();
            var wholePayroll = new List<List<PayStub>>();

            for (var i = 0; i < numberOfWeeks; i++)
            {
                var endOfWeek = startOfWeek.AddDays(7);

                var weekSchedule = db.Shifts.GetRange(startOfWeek, endOfWeek);

                var weekPayroll = new List<PayStub>();

                foreach (var shift in weekSchedule)
                {
                    var timeSpan = shift.TimeStampOut.Subtract(shift.TimeStampIn);

                    if (shift.IsHolidayPay)
                    {
                        AddToPayroll(factory.NewPayStub(shift.Employee, 0, timeSpan.TotalHours), weekPayroll);

                    } else
                    {
                        AddToPayroll(factory.NewPayStub(shift.Employee, timeSpan.TotalHours, 0), weekPayroll);

                    }
                }
                foreach (var payStub in weekPayroll)
                {
                    CalculateEarnings(payStub);
                }

                wholePayroll.Add(weekPayroll);

                startOfWeek = endOfWeek;
            }

            var payroll = CombinePayroll(wholePayroll);

            return payroll.OrderBy(p => p.EmployeeName).ToList();
        }

        private List<PayStub> CombinePayroll(List<List<PayStub>> wholePayroll)
        {
            var finishedPayroll = new List<PayStub>();
            foreach (var weekPayroll in wholePayroll)
            {
                foreach (var payStub in weekPayroll)
                {
                    AddToPayroll(payStub, finishedPayroll);
                }
            }
            return finishedPayroll;
        }


        private void AddToPayroll(PayStub payStub, List<PayStub> payroll)
        {

            var existing = payroll.Find(x => x.EmployeeId.Equals(payStub.EmployeeId));

            if (existing == null)
            {
                payroll.Add(payStub);
            } else
            {
                existing.HolidayHours += payStub.HolidayHours;
                existing.NormalHours += payStub.NormalHours;
                existing.Earnings += payStub.Earnings;
            }
        }

        private void CalculateEarnings(PayStub payStub)
        {
            var wage = payStub.Wage;

            payStub.NormalHours = Math.Round(payStub.NormalHours, 2);
            payStub.HolidayHours = Math.Round(payStub.HolidayHours, 2);

            var normalHours = payStub.NormalHours;

            if (normalHours > 40)
            {
                normalHours = (1.5 * (payStub.NormalHours - 40)) + 40;
            }

            var earnings = Convert.ToDecimal(normalHours + 2 * payStub.HolidayHours) * wage;
            payStub.Earnings = Math.Round(earnings, 2);
        }
    }
}
