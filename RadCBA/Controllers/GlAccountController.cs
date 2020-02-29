using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RadCBA.Core.Models;
using RadCBA.Logic;

namespace RadCBA.Controllers
{
    //[ClaimsAuthorize("DynamicClaim", "GLMgt")]
    public class GlAccountController : Controller
    {
        private AppContext db = new AppContext();

        GlAccountLogic glActLogic = new GlAccountLogic();

        // GET: GlAccount
        public ActionResult Index()
        {
            var glAccounts = db.GlAccounts.Include(g => g.Branch).Include(g => g.GlCategory);
            return View(glAccounts.ToList());
        }

        // GET: GlAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlAccount glAccount = db.GlAccounts.Find(id);
            if (glAccount == null)
            {
                return HttpNotFound();
            }
            return View(glAccount);
        }

        // GET: GlAccount/Create
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name");
            ViewBag.GlCategoryID = new SelectList(db.GlCategories, "ID", "Name");
            return View();
        }

        // POST: GlAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AccountName,GlCategoryID,BranchID")] GlAccount glAccount)
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", glAccount.BranchID);
            ViewBag.GlCategoryID = new SelectList(db.GlCategories, "ID", "Name", glAccount.GlCategoryID);

            if (ModelState.IsValid)
            {
                try
                {
                    if (!glActLogic.IsUniqueName(glAccount.AccountName))
                    {
                        AddError("Account name must be unique");
                        return View(glAccount);
                    }

                    GlCategory glCategory = db.GlCategories.Find(glAccount.GlCategoryID);

                    glAccount.CodeNumber = glActLogic.GenerateGLAccountNumber(glCategory.MainCategory);
                    glAccount.AccountBalance = 0;
                    db.GlAccounts.Add(glAccount);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    AddError(ex.ToString());
                    return View(glAccount);
                }               
            }
            AddError("Please enter valid data");     
            return View(glAccount);
        }

        // GET: GlAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlAccount glAccount = db.GlAccounts.Find(id);
            if (glAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", glAccount.BranchID);
            //ViewBag.GlCategoryID = new SelectList(db.GlCategories, "ID", "Name", glAccount.GlCategoryID);
            return View(glAccount);
        }

        // POST: GlAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccountName,CodeNumber,AccountBalance,GlCategoryID,BranchID")] GlAccount glAccount)
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", glAccount.BranchID);
            //ViewBag.GlCategoryID = new SelectList(db.GlCategories, "ID", "Name", glAccount.GlCategoryID);

            if (ModelState.IsValid)
            {
                try
                {
                    GlAccount originalAccount = db.GlAccounts.Find(glAccount.ID);
                    db.Entry(originalAccount).State = EntityState.Detached;

                    string originalName = originalAccount.AccountName;
                    if (!glAccount.AccountName.ToLower().Equals(originalName.ToLower()))
                    {
                        if (!glActLogic.IsUniqueName(glAccount.AccountName))
                        {
                            AddError("Please select another name");
                            return View(glAccount);
                        }
                    }

                    db.Entry(glAccount).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    AddError(ex.ToString());
                    return View(glAccount);
                }                
            }
            AddError("Please enter valid data");            
            return View(glAccount);
        }

        // GET: GlAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlAccount glAccount = db.GlAccounts.Find(id);
            if (glAccount == null)
            {
                return HttpNotFound();
            }
            return View(glAccount);
        }

        // POST: GlAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GlAccount glAccount = db.GlAccounts.Find(id);
            db.GlAccounts.Remove(glAccount);
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

        private void AddError(string error)
        {
            ModelState.AddModelError("", error);
        }
    }
}
