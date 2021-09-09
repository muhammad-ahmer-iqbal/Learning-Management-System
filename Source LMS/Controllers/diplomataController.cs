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
    public class diplomataController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: diplomata
        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        public ActionResult Index()
        {
            ViewBag.title = "Diploma | LMS";
            return View(db.diploma.ToList());
        }

        // POST: diplomata/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string delete)
        {
            diploma diploma = db.diploma.Find(delete);
            db.diploma.Remove(diploma);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: diplomata/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diploma diploma = db.diploma.Find(id);
            if (diploma == null)
            {
                return HttpNotFound();
            }
            return View(diploma);
        }

        // GET: diplomata/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.title = "Diploma | LMS";
            return View();
        }

        // POST: diplomata/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dip_name,dip_duration,dip_package")] diploma diploma)
        {
            if (ModelState.IsValid)
            {
                db.diploma.Add(diploma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diploma);
        }

        // GET: diplomata/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            ViewBag.title = "Diploma | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diploma diploma = db.diploma.Find(id);
            if (diploma == null)
            {
                return HttpNotFound();
            }
            return View(diploma);
        }

        // POST: diplomata/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dip_name,dip_duration,dip_package")] diploma diploma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diploma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diploma);
        }

        // GET: diplomata/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diploma diploma = db.diploma.Find(id);
            if (diploma == null)
            {
                return HttpNotFound();
            }
            return View(diploma);
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
