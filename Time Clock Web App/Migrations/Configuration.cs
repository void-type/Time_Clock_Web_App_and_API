using System;
using System.Data.Entity.Migrations;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TimeClockContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(TimeClockContext context)
        {
            context.Employees.Add(new Manager("1qaz", "Jeff", "Schreiner", "0005083803", 13.50m, "jeffrocks@gmail.com", "970-555-5555"));
            context.Employees.Add(new Employee("2wsx", "David", "Fitch", "0005081535", 11.50m, "davidrocks@gmail.com", "970-555-5555"));
            context.Employees.Add(new Employee("3edc", "Amelia", "Mawlawi", "0004603304", 11.50m, "ameliarocks@gmail.com", "970-555-5555"));
            context.Employees.Add(new Manager("4rfv", "Jeff", "Derman", "0004618409", 13.50m, "jeffrocks2@gmail.com", "970-555-5555"));
            context.Employees.Add(new Employee("5tgb", "Joelle", "Holst", "23452345234", 11.50m, "joellerocks@gmail.com", "970-555-5555"));


            // Week 1
            var start = new DateTime(2015, 11, 22);
            var stampIn = start;

            var stampOut = stampIn.AddHours(40);
            context.Shifts.Add(new Shift("1qaz", stampIn, stampOut));

            stampOut = stampIn.AddHours(8.125);
            context.Shifts.Add(new Shift("2wsx", stampIn, stampOut));

            stampOut = stampIn.AddHours(6.5);
            context.Shifts.Add(new Shift("3edc", stampIn, stampOut));

            stampOut = stampIn.AddHours(4.25);
            context.Shifts.Add(new Shift("1qaz", stampIn, stampOut));

            context.Shifts.Add(new Shift("4rfv", stampIn, stampOut));

            context.Shifts.Add(new Shift("5tgb", stampIn, stampOut));

            // Test holiday and flow into next week
            stampIn = stampIn.Add(new TimeSpan(6, 23, 59, 59));

            stampOut = stampIn.AddHours(4);
            context.Shifts.Add(new Shift("3edc", stampIn, stampOut, true));


            // Week 2, these should be excluded when making a week long schedule
            // This is the same as last week, so pay should double.
            stampIn = start.AddDays(7);

            stampOut = stampIn.AddHours(40);
            context.Shifts.Add(new Shift("1qaz", stampIn, stampOut));

            stampOut = stampIn.AddHours(8.125);
            context.Shifts.Add(new Shift("2wsx", stampIn, stampOut));

            stampOut = stampIn.AddHours(6.5);
            context.Shifts.Add(new Shift("3edc", stampIn, stampOut));

            stampOut = stampIn.AddHours(4.25);
            context.Shifts.Add(new Shift("1qaz", stampIn, stampOut));

            context.Shifts.Add(new Shift("4rfv", stampIn, stampOut));

            context.Shifts.Add(new Shift("5tgb", stampIn, stampOut));

            stampIn = stampIn.Add(new TimeSpan(6, 23, 59, 59));

            stampOut = stampIn.AddHours(4);
            context.Shifts.Add(new Shift("3edc", stampIn, stampOut, true));

            context.SaveChanges();


        }
    }
}
