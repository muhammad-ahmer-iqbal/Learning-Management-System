using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LMS.Controllers
{
    public class DefaultController : Controller
    {
        private LMSEntities db = new LMSEntities();
        [AllowAnonymous]
        // GET: Default
        public ActionResult SignIn()
        {
            ViewBag.title = "Sign In | LMS";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "u_id,u_password")] login model)
        {
            bool isValid = db.userDetails.Any(x => x.u_id == model.u_id && x.u_password == model.u_password);

            try
            {
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.u_id, false);
                    if (User.IsInRole("Student"))
                    {
                        return RedirectToAction("Index", "attendances");
                    }
                    else
                    {
                        return RedirectToAction("Index", "batches");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User ID and Password");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View();
        }
        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }
    }
}
