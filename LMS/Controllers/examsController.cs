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
    public class examsController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: exams
        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        public ActionResult Index()
        {
            ViewBag.title = "Exam | LMS";
            var exam = db.exam.Include(e => e.course);
            return View(exam.ToList());
        }

        // POST: exams/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int delete)
        {
            exam exam = db.exam.Find(delete);
            db.exam.Remove(exam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: exams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exam exam = db.exam.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: exams/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.title = "Exam | LMS";
            ViewBag.ex_exam = new SelectList(db.course, "cour_name", "cour_diploma");
            return View();
        }

        // POST: exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ex_id,ex_exam")] exam exam)
        {
            if (ModelState.IsValid)
            {
                db.exam.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ex_exam = new SelectList(db.course, "cour_name", "cour_diploma", exam.ex_exam);
            return View(exam);
        }

        // GET: exams/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            ViewBag.title = "Exam | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exam exam = db.exam.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.ex_exam = new SelectList(db.course, null, "cour_name", exam.ex_exam);
            return View(exam);
        }

        // POST: exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ex_id,ex_exam")] exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ex_exam = new SelectList(db.course, "cour_name", "cour_diploma", exam.ex_exam);
            return View(exam);
        }

        // GET: exams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exam exam = db.exam.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
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
