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
    public class studentsController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: students
        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        public ActionResult Index(string id)
        {
            ViewBag.title = "Student | LMS";
            ViewBag.batchID = id;
            return View(db.student.ToList());
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string delete)
        {
            student student = db.student.Find(delete);
            db.student.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.title = "Student | LMS";
            ViewBag.stud_batch = new SelectList(db.batch, null, "bat_id");
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stud_enrollment,stud_name,stud_fatherName,stud_email,stud_contact,stud_fee,stud_attendExam,stud_batch,stud_address")] student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.student.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Enter a valid data");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(student);
        }

        // GET: students/Edit/5
        [Authorize(Roles = "Admin,Coordinator")]
        public ActionResult Edit(string id)
        {
            ViewBag.title = "Student | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.stud_batch = new SelectList(db.batch, "bat_id", "bat_id", student.stud_batch);
            return View(student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stud_enrollment,stud_name,stud_fatherName,stud_email,stud_contact,stud_fee,stud_address,stud_attendExam,stud_batch")] student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Enter a valid data");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            ViewBag.stud_batch = new SelectList(db.batch, "bat_id", "bat_id", student.stud_batch);
            return View(student);
        }

        // GET: students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
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
