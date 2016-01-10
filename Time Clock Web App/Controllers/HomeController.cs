using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About JAD Systems";

            return View();
        }

        public Employee GetEmployee()
        {
            using (var db = TimeClockUnitOfWork.Create())
            {
                return db.Employees.SearchByUserIdentity(User.Identity.GetUserId());
            }
        }

        public void GetEmployeeRole()
        {
            // Todo: Visitor pattern from IDCardReader to verify manager
        }
    }
}