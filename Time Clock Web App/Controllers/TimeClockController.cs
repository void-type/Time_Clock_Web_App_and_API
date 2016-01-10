using System;
using System.Web.Mvc;
using Time_Clock_Web.Logic;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Controllers
{
    public class TimeClockController : Controller
    {
        private TimeClockUnitOfWork db = TimeClockUnitOfWork.Create();

        // GET: ClockInOut
        [AllowAnonymous]
        public ActionResult Clock()
        {
            return View();
        }

        // POST: ClockInOut
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Clock(ClockInViewModel viewModel)
        {
            try
            {
                ViewBag.ClockInResult = false;

                var emp = db.Employees.SearchByCardNumberOrId(viewModel.Credentials);

                if (emp.Disabled)
                {
                    ViewBag.ClockMessage = "This isn't an active employee.";
                    return View();
                }

                var empId = emp.EmployeeId;

                var tc = new TimeClock(db);

                if (tc.ClockInOrOut(empId))
                {
                    ViewBag.ClockMessage = "Welcome, " + emp.FirstName + "! You have clocked in.";
                    ViewBag.ClockInResult = true;
                } else
                {
                    ViewBag.ClockMessage = "You have clocked out, " + emp.FirstName + ". Have a nice day!";
                }
            } catch (NullReferenceException)
            {
                ViewBag.ClockMessage = "There's no one assigned to that number.";
            } catch (Exception)
            {
                ViewBag.ClockMessage = "Something happened! 8(";
            }

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}