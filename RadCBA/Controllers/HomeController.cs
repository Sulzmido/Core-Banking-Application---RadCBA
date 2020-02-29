using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RadCBA.Controllers
{
    public class HomeController : Controller
    {
        private AppContext db;
        private ApplicationDbContext appdb;

        public HomeController(AppContext dbParam, ApplicationDbContext appdbParam)
        {
            db = dbParam;
            appdb = appdbParam;
        }

        public ActionResult Index()
        {
            ViewBag.UserCount = appdb.Users.Count();

            ViewBag.CustomerCount = db.Customers.Count();
            ViewBag.CustomerAccountCount = db.CustomerAccounts.Count();

            ViewBag.BranchCount = db.Branches.Count();

            ViewBag.GlAccountCount = db.GlAccounts.Count();

            int glPostApprovedCount = db.GlPostings.Where(g => g.Status == PostStatus.Approved).Count();
            int glPostPendingCount = db.GlPostings.Where(g => g.Status == PostStatus.Pending).Count();
            int glPostDeclinedCount = db.GlPostings.Where(g => g.Status == PostStatus.Declined).Count();
            ViewBag.GlPostApprovedCount = glPostApprovedCount;
            ViewBag.GlPostPendingCount = glPostPendingCount;
            
            int tellerPostApprovedCount = db.TellerPostings.Where(t => t.Status == PostStatus.Approved).Count();
            int tellerPostPendingCount = db.TellerPostings.Where(t => t.Status == PostStatus.Pending).Count();
            int tellerPostDeclinedCount = db.TellerPostings.Where(t => t.Status == PostStatus.Declined).Count();
            ViewBag.TellerPostApprovedCount = tellerPostApprovedCount;
            ViewBag.TellerPostPendingCount = tellerPostPendingCount;

            int allPostCount = (glPostApprovedCount + glPostDeclinedCount + glPostPendingCount + tellerPostApprovedCount + tellerPostPendingCount + tellerPostDeclinedCount);

            var pendingPercent = (glPostPendingCount + tellerPostPendingCount) * 100 / allPostCount;
            var approvedPercent = (glPostApprovedCount + tellerPostApprovedCount) * 100 / allPostCount;
            var declinedPercent = (glPostDeclinedCount + tellerPostDeclinedCount) * 100 / allPostCount;
            ViewBag.PendingPercent = pendingPercent;
            ViewBag.ApprovedPercent = approvedPercent;
            ViewBag.DeclinedPercent = declinedPercent;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}