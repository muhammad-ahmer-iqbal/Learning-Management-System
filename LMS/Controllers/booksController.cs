using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS;

namespace LMS.Controllers
{
    public class booksController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: books
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Books | LMS";
            return View(db.book.ToList());
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int delete)
        {
            book book = db.book.Find(delete);
            db.book.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Books | LMS";
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "b_id,b_name,b_write,b_topic,b_book,bookFile")] book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string filename = Path.GetFileNameWithoutExtension(book.bookFile.FileName),
                            extension = Path.GetExtension(book.bookFile.FileName);
                    if ((extension == ".pdf") || (extension == ".doc") || (extension == ".docx"))
                    {
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        book.b_book = "/Book/" + filename;
                        filename = Path.Combine(Server.MapPath("~/Book/"), filename);
                        book.bookFile.SaveAs(filename);

                        db.book.Add(book);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("", "Enter any PDF, DOC or DOCX file only");
                }
                else
                    ModelState.AddModelError("", "Enter a valid data");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(book);
        }

        [Authorize(Roles = "Admin,Teacher,Coordinator")]
        // GET: books/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "Books | LMS";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "b_id,b_name,b_write,b_topic,b_book")] book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(book).State = EntityState.Modified;
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
            return View(book);
        }

        // GET: books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
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
