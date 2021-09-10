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
    public class coordinatorsController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: coordinators
        public ActionResult Index()
        {
            ViewBag.title = "Coordinator | LMS";
            return View(db.coordinator.ToList());
        }

        // POST: coordinators/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string delete)
        {
            coordinator coordinator = db.coordinator.Find(delete);
            db.coordinator.Remove(coordinator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: coordinators/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coordinator coordinator = db.coordinator.Find(id);
            if (coordinator == null)
            {
                return HttpNotFound();
            }
            return View(coordinator);
        }

        // GET: coordinators/Create
        public ActionResult Create()
        {
            ViewBag.title = "Coordinator | LMS";
            return View();
        }

        // POST: coordinators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "coord_id,coord_name,coord_email,coord_address,coord_contact,coord_salary")] coordinator coordinator)
        {
            if (ModelState.IsValid)
            {
                db.coordinator.Add(coordinator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coordinator);
        }

        // GET: coordinators/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.title = "Coordinator | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coordinator coordinator = db.coordinator.Find(id);
            if (coordinator == null)
            {
                return HttpNotFound();
            }
            return View(coordinator);
        }

        // POST: coordinators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "coord_id,coord_name,coord_email,coord_address,coord_contact,coord_salary")] coordinator coordinator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordinator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coordinator);
        }

        // GET: coordinators/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coordinator coordinator = db.coordinator.Find(id);
            if (coordinator == null)
            {
                return HttpNotFound();
            }
            return View(coordinator);
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
