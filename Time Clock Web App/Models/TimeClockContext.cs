using MySql.Data.Entity;
using System.Data.Entity;

namespace Time_Clock_Web.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class TimeClockContext : ApplicationDbContext
    {
        public static string Db => "CloudConnection";

        public TimeClockContext() : base(Db)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext CreateBase()
        {
            return new TimeClockContext();
        }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Shift> Shifts { get; set; }

        public DbSet<IncompleteShift> IncompleteShifts { get; set; }
    }
}
