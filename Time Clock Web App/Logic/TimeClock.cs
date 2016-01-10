using System;
using System.Collections.Generic;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Logic
{
    public class TimeClock
    {
        private TimeClockUnitOfWork db;

        public TimeClock(TimeClockUnitOfWork uow)
        {
            db = uow;
        }

        public bool ClockInOrOut(string id)
        {
            var incomplete = db.IncompleteShifts.Get(id);
            if (incomplete != null)
            {
                ClockOut(incomplete);
                return false;
            }
            ClockIn(id);
            return true;
        }

        private void ClockOut(IncompleteShift i)
        {
            db.Shifts.Add(new Shift(i.EmployeeId, i.TimeStampIn, DateTime.Now, i.IsHolidayPay));
            db.IncompleteShifts.Remove(i);
            db.Save();
        }

        // Todo: should perhaps build a builder
        private void ClockIn(string id)
        {
            var now = DateTime.Now;

            db.IncompleteShifts.Add(new IncompleteShift(id, now, GetIfHoliday(now)));
            db.Save();
        }

        // Todo: these would be better if mapped on the DB and the manager could change them
        private bool GetIfHoliday(DateTime day)
        {
            var holidays = new List<DateTime>()
            {
                new DateTime(2015, 11, 11),
                new DateTime(2015, 11, 26),
                new DateTime(2015, 12, 25),
                new DateTime(2015, 1, 1)
            };

            return holidays.Contains(day.Date);
        }
    }
}