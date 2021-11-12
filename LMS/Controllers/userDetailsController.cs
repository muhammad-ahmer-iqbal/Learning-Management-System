using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS;
using WebMatrix.WebData;

namespace LMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class userDetailsController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: userDetails
        public ActionResult Index()
        {
            ViewBag.Title = "User | LMS";
            return View(db.userDetails.ToList());
        }
        // POST: user/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string delete)
        {
            userDetails userDetails = db.userDetails.Find(delete);
            db.userDetails.Remove(userDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: userDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // GET: userDetails/Create
        public ActionResult Create()
        {
            ViewBag.Title = "User | LMS";
            return View();
        }

        // POST: userDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "u_id,u_password")] userDetails userDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userDetails.u_role = "Admin";
                    db.userDetails.Add(userDetails);
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

            return View(userDetails);
        }

        // GET: userDetails/ChangePassword
        [Authorize(Roles = "Admin,Teacher,Coordinator,Student")]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "Change Setting | LMS";
            var id = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: userDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "u_id,u_password,u_role,oldPassword,newPassword,confirmPassword")] userDetails userDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userDetails.u_password == userDetails.oldPassword)
                    {
                        if (userDetails.confirmPassword == userDetails.newPassword)
                        {
                            string newPass = userDetails.confirmPassword;
                            userDetails.u_password = newPass;
                            db.Entry(userDetails).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("ChangePassword");
                        }
                        else
                            ModelState.AddModelError("confirmPassword", "Confirm password does match");
                    }
                    else
                        ModelState.AddModelError("oldPassword", "Old password is not correct");
                }
                else
                    ModelState.AddModelError("", "Enter a valid data");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(userDetails);
        }

        // GET: userDetails/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "User | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: userDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "u_id,u_password,u_role")] userDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetails);
        }

        // GET: userDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: userDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            userDetails userDetails = db.userDetails.Find(id);
            db.userDetails.Remove(userDetails);
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
