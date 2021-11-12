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
    [Authorize(Roles = "Admin")]
    public class teachersController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: teachers
        public ActionResult Index()
        {
            ViewBag.Title = "Teachers | LMS";
            return View(db.teacher.ToList());
        }

        // POST: teachers/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string delete)
        {
            teacher teacher = db.teacher.Find(delete);
            db.teacher.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: teachers/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Teachers | LMS";
            return View();
        }

        // POST: teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "teach_id,teach_name,teach_email,teach_address,teach_contact,teach_salary")] teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.teacher.Add(teacher);
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

            return View(teacher);
        }

        // GET: teachers/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Teachers | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacher = db.teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "teach_id,teach_name,teach_email,teach_address,teach_contact,teach_salary")] teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(teacher).State = EntityState.Modified;
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
            return View(teacher);
        }

        // GET: teachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacher = db.teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            teacher teacher = db.teacher.Find(id);
            db.teacher.Remove(teacher);
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
