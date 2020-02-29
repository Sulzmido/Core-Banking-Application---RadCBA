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
using RadCBA.Core.ViewModels.TellerManagementViewModel;

namespace RadCBA.Controllers
{
    //[ClaimsAuthorize("DynamicClaim", "TellerMgt")]
    public class TellerManagementController : Controller
    {
        private AppContext db = new AppContext();
        private ApplicationDbContext appdb = new ApplicationDbContext();

        TellerMgtLogic tellerMgtLogic = new TellerMgtLogic();

        // GET: TellerManagement
        public ActionResult Index()
        {
            List<TillToUser> allInfo = tellerMgtLogic.ExtractAllTellerInfo();

            List<TillToUserViewModel> models = new List<TillToUserViewModel>();

            foreach(var info in allInfo)
            {
                TillToUserViewModel entry;

                if (info.GlAccountID == 0)
                {
                    entry = new TillToUserViewModel { Username = appdb.Users.Find(info.UserId).UserName, GLAccountName = "NIL", AccountBalance = "NIL", HasDetails = false, IsDeletable = false};
                }
                else
                {
                    var applicationUser = appdb.Users.Find(info.UserId);
                    // we want to be able to delete association if an associated user is no longer authorized to do teller postings.
                    entry = new TillToUserViewModel { Id = info.ID, Username = applicationUser.UserName, GLAccountName = info.GlAccount.AccountName, AccountBalance = info.GlAccount.AccountBalance.ToString(), HasDetails = true, IsDeletable = !applicationUser.Role.RoleClaims.Any(rc => rc.Name.Equals("TellerPosting"))};
                }

                models.Add(entry);
            }
            return View(models);
        }

        // GET: TellerManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TillToUser tillToUser = db.TillToUsers.Find(id);
            if (tillToUser == null)
            {
                return HttpNotFound();
            }
            TillToUserViewModel model = new TillToUserViewModel { GLAccountName = tillToUser.GlAccount.AccountName, AccountBalance = tillToUser.GlAccount.AccountBalance.ToString(), Username = appdb.Users.Find(tillToUser.UserId).UserName };
            return View(model);
        }

        // GET: TellerManagement/Create
        public ActionResult Create()
        {
            // get all users that have the tellerposting claim without till
            // get all till's without a user.
            ViewBag.Users = new SelectList(tellerMgtLogic.ExtractTellersWithoutTill(), "Id", "UserName");
            ViewBag.GlAccountID = new SelectList(tellerMgtLogic.ExtractTillsWithoutTeller(), "ID", "AccountName");
            return View();
        }

        // POST: TellerManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,GlAccountID")] TillToUser tillToUser)
        {
            if (ModelState.IsValid)
            {
                db.TillToUsers.Add(tillToUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Users = new SelectList(tellerMgtLogic.ExtractTellersWithoutTill(), "Id", "UserName", tillToUser.UserId);
            ViewBag.GlAccountID = new SelectList(tellerMgtLogic.ExtractTillsWithoutTeller(), "ID", "AccountName", tillToUser.GlAccountID);
            return View(tillToUser);
        }

        // GET: TellerManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TillToUser tillToUser = db.TillToUsers.Find(id);
            if (tillToUser == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Users = new SelectList(UserManager.Users, "Id", "UserName", tillToUser.UserId);
            ViewBag.GlAccountID = new SelectList(db.GlAccounts, "ID", "AccountName", tillToUser.GlAccountID);
            return View(tillToUser);
        }

        // POST: TellerManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,GlAccountID")] TillToUser tillToUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tillToUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Users = new SelectList(UserManager.Users, "Id", "UserName", tillToUser.UserId);
            ViewBag.GlAccountID = new SelectList(db.GlAccounts, "ID", "AccountName", tillToUser.GlAccountID);
            return View(tillToUser);
        }

        // GET: TellerManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TillToUser tillToUser = db.TillToUsers.Find(id);
            if (tillToUser == null)
            {
                return HttpNotFound();
            }
            TillToUserViewModel model = new TillToUserViewModel { GLAccountName = tillToUser.GlAccount.AccountName, Username = appdb.Users.Find(tillToUser.UserId).UserName };
            return View(model);
        }

        // POST: TellerManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TillToUser tillToUser = db.TillToUsers.Find(id);
            db.TillToUsers.Remove(tillToUser);
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
