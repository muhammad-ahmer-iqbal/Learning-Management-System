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
    public class batchesController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: batches
        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        public ActionResult Index()
        {
            ViewBag.title = "Batch | LMS";
            var batch = db.batch.Include(b => b.coordinator).Include(b => b.teacher);
            return View(batch.ToList());
        }

        // POST: batches/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string delete)
        {
            batch batch = db.batch.Find(delete);
            db.batch.Remove(batch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: batches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch batch = db.batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // GET: batches/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.title = "Batch | LMS";
            ViewBag.bat_coordinator = new SelectList(db.coordinator, "coord_id", "coord_name");
            ViewBag.bat_teacher = new SelectList(db.teacher, "teach_id", "teach_name");
            return View();
        }

        // POST: batches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bat_id,bat_slot,bat_days,bat_teacher,bat_coordinator,bat_lab,bat_sem,bat_status")] batch batch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.batch.Add(batch);
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

            ViewBag.bat_coordinator = new SelectList(db.coordinator, "coord_id", "coord_name", batch.bat_coordinator);
            ViewBag.bat_teacher = new SelectList(db.teacher, "teach_id", "teach_name", batch.bat_teacher);
            return View(batch);
        }

        // GET: batches/Edit/5
        [Authorize(Roles = "Admin,Coordinator")]
        public ActionResult Edit(string id)
        {
            ViewBag.title = "Batch | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch batch = db.batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.bat_coordinator = new SelectList(db.coordinator, "coord_id", "coord_name", batch.bat_coordinator);
            ViewBag.bat_teacher = new SelectList(db.teacher, "teach_id", "teach_name", batch.bat_teacher);
            return View(batch);
        }

        // POST: batches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bat_id,bat_slot,bat_days,bat_teacher,bat_coordinator,bat_lab,bat_sem,bat_status")] batch batch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(batch).State = EntityState.Modified;
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
            ViewBag.bat_coordinator = new SelectList(db.coordinator, "coord_id", "coord_name", batch.bat_coordinator);
            ViewBag.bat_teacher = new SelectList(db.teacher, "teach_id", "teach_name", batch.bat_teacher);
            return View(batch);
        }

        // GET: batches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch batch = db.batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
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
