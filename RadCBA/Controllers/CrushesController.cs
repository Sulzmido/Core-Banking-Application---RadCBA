using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RadCBA.Core.Models;

namespace RadCBA.Controllers
{
    public class CrushesController : Controller
    {
        private NewDbContext db = new NewDbContext();

        public ActionResult Bootlearn()
        {
            return View(db.Crushes.First());
        }

        public ActionResult Startboot()
        {
            return View();
        }

        // GET: Crushes
        public ActionResult Index()
        {
            return View(db.Crushes.ToList());
        }

        // GET: Crushes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crush crush = db.Crushes.Find(id);
            if (crush == null)
            {
                return HttpNotFound();
            }
            return View(crush);
        }

        // GET: Crushes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Crushes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Crush crush)
        {
            if (ModelState.IsValid)
            {
                db.Crushes.Add(crush);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crush);
        }

        // GET: Crushes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crush crush = db.Crushes.Find(id);
            if (crush == null)
            {
                return HttpNotFound();
            }
            return View(crush);
        }

        // POST: Crushes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Crush crush)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crush).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crush);
        }

        // GET: Crushes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crush crush = db.Crushes.Find(id);
            if (crush == null)
            {
                return HttpNotFound();
            }
            return View(crush);
        }

        // POST: Crushes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crush crush = db.Crushes.Find(id);
            db.Crushes.Remove(crush);
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
