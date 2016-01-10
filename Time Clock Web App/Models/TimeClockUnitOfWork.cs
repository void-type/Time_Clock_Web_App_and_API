using System;
using Time_Clock_Web.Repositories.TimeClock;

namespace Time_Clock_Web.Models
{
    public class TimeClockUnitOfWork : IDisposable
    {
        public IEmployeeRepository Employees { get; private set; }
        public IShiftRepository Shifts { get; private set; }
        public IIncompleteShiftRepository IncompleteShifts { get; private set; }

        private readonly TimeClockContext _context;

        public static TimeClockUnitOfWork Create()
        {
            return new TimeClockUnitOfWork(new TimeClockContext());
        }

        public TimeClockUnitOfWork(TimeClockContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Shifts = new ShiftRepository(_context);
            IncompleteShifts = new IncompleteShiftRepository(_context);
        }


        public int Save()
        {
            return _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
