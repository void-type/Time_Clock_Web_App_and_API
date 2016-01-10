using System.Net;
using System.Web.Mvc;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Controllers
{
    public class IncompleteShiftsController : Controller
    {
        // Todo: Make the rfid reader input here to create a new incomplete shift
        private TimeClockUnitOfWork db = TimeClockUnitOfWork.Create();

        // GET: IncompleteShifts
        public ActionResult Index()
        {
            var incompleteShifts = db.IncompleteShifts.GetAllWithEmployees();
            return View(incompleteShifts);
        }

        // GET: IncompleteShifts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncompleteShift incompleteShift = db.IncompleteShifts.Get(id);
            if (incompleteShift == null)
            {
                return HttpNotFound();
            }
            return View(incompleteShift);
        }

        // GET: IncompleteShifts/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName");
            return View();
        }

        // POST: IncompleteShifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeStampIn,IsHolidayPay,EmployeeId")] IncompleteShift incompleteShift)
        {
            if (ModelState.IsValid)
            {
                db.IncompleteShifts.Add(incompleteShift);
                db.Save();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName", incompleteShift.EmployeeId);
            return View(incompleteShift);
        }

        // GET: IncompleteShifts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncompleteShift incompleteShift = db.IncompleteShifts.Get(id);
            if (incompleteShift == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName", incompleteShift.EmployeeId);
            return View(incompleteShift);
        }

        // POST: IncompleteShifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeStampIn,IsHolidayPay,EmployeeId")] IncompleteShift incompleteShift)
        {
            if (ModelState.IsValid)
            {
                db.IncompleteShifts.AddOrUpdate(incompleteShift);
                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.GetAll(), "EmployeeId", "FullName", incompleteShift.EmployeeId);
            return View(incompleteShift);
        }

        // GET: IncompleteShifts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncompleteShift incompleteShift = db.IncompleteShifts.Get(id);
            if (incompleteShift == null)
            {
                return HttpNotFound();
            }
            return View(incompleteShift);
        }

        // POST: IncompleteShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            IncompleteShift incompleteShift = db.IncompleteShifts.Get(id);
            db.IncompleteShifts.Remove(incompleteShift);
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
