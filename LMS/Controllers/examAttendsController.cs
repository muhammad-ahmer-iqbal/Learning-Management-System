using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS;

namespace LMS.Controllers
{
    public class examAttendsController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: examAttends
        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        public ActionResult Index(string id)
        {
            ViewBag.title = "Attended Exam | LMS";
            ViewBag.studID = id;
            var examAttend = db.examAttend.Include(e => e.exam).Include(e => e.student);
            return View(examAttend.ToList());
        }

        // POST: examAttends/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int delete)
        {
            examAttend examAttend = db.examAttend.Find(delete);
            db.examAttend.Remove(examAttend);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: examAttends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            examAttend examAttend = db.examAttend.Find(id);
            if (examAttend == null)
            {
                return HttpNotFound();
            }
            return View(examAttend);
        }

        // GET: examAttends/Create
        [Authorize(Roles = "Admin,Coordinator")]
        public ActionResult Create()
        {
            ViewBag.title = "Attended Exam | LMS";
            ViewBag.exat_name = new SelectList(db.exam, "ex_id", "ex_exam");
            ViewBag.exat_enrollement = new SelectList(db.student, "stud_enrollment", "stud_name");
            return View();
        }

        // POST: examAttends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "exat_id,exat_name,exat_enrollement,exat_status,exat_date,exat_time,exat_lab")] examAttend examAttend)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.examAttend.Add(examAttend);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
                else
                    ModelState.AddModelError("", "Enter a valid data");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            ViewBag.exat_name = new SelectList(db.exam, "ex_id", "ex_exam", examAttend.exat_name);
            ViewBag.exat_enrollement = new SelectList(db.student, "stud_enrollment", "stud_name", examAttend.exat_enrollement);
            return View(examAttend);
        }

        // GET: examAttends/Edit/5
        [Authorize(Roles = "Admin,Coordinator")]
        public ActionResult Edit(int? id)
        {
            ViewBag.title = "Attended Exam | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            examAttend examAttend = db.examAttend.Find(id);
            if (examAttend == null)
            {
                return HttpNotFound();
            }
            ViewBag.exat_name = new SelectList(db.exam, "ex_id", "ex_exam", examAttend.exat_name);
            ViewBag.exat_enrollement = new SelectList(db.student, "stud_enrollment", "stud_name", examAttend.exat_enrollement);
            return View(examAttend);
        }

        // POST: examAttends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "exat_id,exat_name,exat_enrollement,exat_status,exat_date,exat_time,exat_lab")] examAttend examAttend)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(examAttend).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Enter a valid data");
            }
            catch (Exception e)
            {
                    ModelState.AddModelError("", e.Message);
            }
            ViewBag.exat_name = new SelectList(db.exam, "ex_id", "ex_exam", examAttend.exat_name);
            ViewBag.exat_enrollement = new SelectList(db.student, "stud_enrollment", "stud_name", examAttend.exat_enrollement);
            return View(examAttend);
        }

        // GET: examAttends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            examAttend examAttend = db.examAttend.Find(id);
            if (examAttend == null)
            {
                return HttpNotFound();
            }
            return View(examAttend);
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
