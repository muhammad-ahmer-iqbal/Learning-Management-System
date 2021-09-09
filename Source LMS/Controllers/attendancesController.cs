using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class attendancesController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: attendances
        [Authorize(Roles = "Admin,Teacher,Coordinator,Student")]
        public ActionResult Index(string id)
        {
            ViewBag.title = "Attendance | LMS";
            ViewBag.studID = id;
            var attendance = db.attendance.Include(a => a.student).Include(a => a.teacher);
            return View(attendance.ToList());
        }

        // GET: attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: attendances/Create     
        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        public ActionResult Create(string id)
        {
            ViewBag.title = "Attendance | LMS";
            ViewBag.att_teacher = User.Identity.Name;
            ViewBag.students = db.student.ToList();
            ViewBag.batch =  id;
            return View();
        }

        // POST: attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "att_id,att_name,att_teacher,att_session,att_attendance")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                attendance.att_teacher = User.Identity.Name;
                attendance.att_session = DateTime.Now.Day;
                attendance.att_month = DateTime.Now.ToString("MMMM");
                attendance.att_year = DateTime.Now.Year;
                db.attendance.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(attendance);
        }

        // GET: attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.att_name = new SelectList(db.student, "stud_enrollment", "stud_name", attendance.att_name);
            ViewBag.att_teacher = new SelectList(db.teacher, "teach_id", "teach_name", attendance.att_teacher);
            return View(attendance);
        }

        // POST: attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "att_id,att_name,att_teacher,att_session,att_month,att_year")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.att_name = new SelectList(db.student, "stud_enrollment", "stud_name", attendance.att_name);
            ViewBag.att_teacher = new SelectList(db.teacher, "teach_id", "teach_name", attendance.att_teacher);
            return View(attendance);
        }

        // GET: attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            attendance attendance = db.attendance.Find(id);
            db.attendance.Remove(attendance);
            db.SaveChanges();
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
