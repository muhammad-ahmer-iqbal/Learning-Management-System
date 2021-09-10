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
    public class coursesController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: courses
        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        public ActionResult Index()
        {
            ViewBag.title = "Course | LMS";
            return View(db.course.ToList());
        }

        // POST: courses/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string delete)
        {
            course course = db.course.Find(delete);
            db.course.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: courses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: courses/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.title = "Course | LMS";
            ViewBag.cour_diploma = new SelectList(db.diploma, "dip_id", "dip_name");
            return View();
        }

        // POST: courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cour_name,cour_sessions,cour_diploma")] course course)
        {
            if (ModelState.IsValid)
            {
                db.course.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cour_diploma = new SelectList(db.diploma, "dip_id", "dip_name", course.cour_diploma);
            return View(course);
        }

        // GET: courses/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            ViewBag.title = "Course | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.cour_diploma = new SelectList(db.diploma, "dip_name", "dip_duration", course.cour_diploma);
            return View(course);
        }

        // POST: courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cour_name,cour_sessions,cour_diploma")] course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cour_diploma = new SelectList(db.diploma, "dip_name", "dip_duration", course.cour_diploma);
            return View(course);
        }

        // GET: courses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
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
