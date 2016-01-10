using System.Web.Mvc;
using Time_Clock_Web.Logic;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Controllers
{
    public class PayrollController : Controller
    {
        TimeClockUnitOfWork db = TimeClockUnitOfWork.Create();

        // GET: Payroll
        public ActionResult Index()
        {
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "StartDate, NumberOfWeeks")] PayrollViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ViewPayroll", viewModel);
            }

            return View();
        }

        public ActionResult ViewPayroll(PayrollViewModel viewModel)
        {
            var pc = new PayrollCalculator(db);
            var stubs = pc.GetPayroll(viewModel.StartDate, viewModel.NumberOfWeeks);
            return View(stubs);
        }
    }
}