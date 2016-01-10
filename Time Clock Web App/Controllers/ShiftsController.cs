using System.Linq;
using System.Net;
using System.Web.Mvc;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Controllers
{
    public class ShiftsController : Controller
    {
        private TimeClockUnitOfWork db = TimeClockUnitOfWork.Create();

        // GET: Shifts For All Employees
        public ActionResult Index()
        {
            var shifts = db.Shifts.GetAllWithEmployees();
            return View(shifts);
        }

        // GET: Shifts For One Employee
        public ActionResult PerEmployee(string Id)
        {
            var shifts = db.Shifts.GetForEmployee(Id);
            ViewBag.EmployeeName = shifts.First().Employee.FullName;
            return View(shifts);
        }


        // GET: Shifts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Get(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // GET: Shifts/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName");
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeStampIn,TimeStampOut,IsHolidayPay,EmployeeId")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Shifts.Add(shift);
                db.Save();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName", shift.EmployeeId);
            return View(shift);
        }

        // GET: Shifts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Get(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName", shift.EmployeeId);
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeStampIn,TimeStampOut,IsHolidayPay,EmployeeId")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Shifts.AddOrUpdate(shift);
                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName", shift.EmployeeId);
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Get(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Shift shift = db.Shifts.Get(id);
            db.Shifts.Remove(shift);
            db.Save();
            return RedirectToAction("Index");
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
