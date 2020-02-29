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
    //[ClaimsAuthorize("DynamicClaim", "PostingAuth")]
    public class PostingAuthController : Controller
    {
        private AppContext db;

        CustomerAccountLogic custActLogic;
        TellerPostingLogic telPostLogic;
        FinancialReportLogic reportLogic;
        BusinessLogic busLogic;

        public PostingAuthController(AppContext dbParam, CustomerAccountLogic custActLogicParam, TellerPostingLogic telPostLogicParam, FinancialReportLogic reportLogicParam, BusinessLogic busLogicParam)
        {
            db = dbParam;
            custActLogic = custActLogicParam;
            telPostLogic = telPostLogicParam;
            reportLogic = reportLogicParam;
            busLogic = busLogicParam;
        }

        public ActionResult TellerPosts(string message)
        {
            if(message != null)
            {
                ViewBag.Msg = message;
            }
            var tellerPostings = db.TellerPostings.Include(t => t.CustomerAccount).Where(t => t.Status != PostStatus.Approved);
            return View(tellerPostings.ToList());
        }

        public ActionResult GlPosts(string message)
        {
            if(message != null)
            {
                ViewBag.Msg = message;
            }
            var glPostings = db.GlPostings.Include(g => g.CrGlAccount).Include(g => g.DrGlAccount).Where(g => g.Status != PostStatus.Approved);
            return View(glPostings.ToList());
        }

        public ActionResult ApproveTellerPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TellerPosting tellerPosting = db.TellerPostings.Find(id);
            if (tellerPosting == null)
            {
                return HttpNotFound();
            }

            var amt = tellerPosting.Amount;

            var tillAct = tellerPosting.TillAccount;
            var custAct = tellerPosting.CustomerAccount;

            if (tellerPosting.PostingType == TellerPostingType.Withdrawal)
            {
                if (custActLogic.CustomerAccountHasSufficientBalance(custAct, amt))
                {
                    if (!(tillAct.AccountBalance >= amt))
                    {
                        // i have no choice but to send the error messages with partial views.
                        return RedirectToAction("TellerPosts", new { message = "Insuficient funds in till account" });
                    }
                                        
                    //all this is happening after transaction is approved by checker.
                    string result = telPostLogic.PostTeller(custAct, tillAct, amt, TellerPostingType.Withdrawal);
                    if (!result.Equals("success"))
                    {
                        return RedirectToAction("TellerPosts", new { message = result });
                    }

                    tellerPosting.Status = PostStatus.Approved;
                    db.Entry(tellerPosting).State = EntityState.Modified;
                    db.Entry(custAct).State = EntityState.Modified;
                    db.Entry(tillAct).State = EntityState.Modified;

                    db.SaveChanges();

                    reportLogic.CreateTransaction(tillAct, amt, TransactionType.Credit);
                    reportLogic.CreateTransaction(custAct, amt, TransactionType.Debit);

                    return RedirectToAction("TellerPosts", new { message = "Transaction Approved!" });
                }
                else
                {
                    return RedirectToAction("TellerPosts", new { message = "Insufficient funds in customer account" });
                }
            }
            else
            {                
                string result = telPostLogic.PostTeller(custAct, tillAct, amt, TellerPostingType.Deposit);
                if (!result.Equals("success"))
                {
                    return RedirectToAction("TellerPosts", new { message = result });
                }

                tellerPosting.Status = PostStatus.Approved;
                db.Entry(tellerPosting).State = EntityState.Modified;
                db.Entry(custAct).State = EntityState.Modified;
                db.Entry(tillAct).State = EntityState.Modified;

                db.SaveChanges();

                reportLogic.CreateTransaction(tillAct, amt, TransactionType.Debit);
                reportLogic.CreateTransaction(custAct, amt, TransactionType.Credit);

                return RedirectToAction("TellerPosts", new { message = "Transaction Approved!" });
            }
        }

        public ActionResult ApproveGlPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlPosting glPosting = db.GlPostings.Find(id);
            if (glPosting == null)
            {
                return HttpNotFound();
            }

            var drAct = glPosting.DrGlAccount;
            var crAct = glPosting.CrGlAccount;

            if (crAct.AccountName.ToLower().Contains("till") || crAct.AccountName.ToLower().Contains("vault"))
            {
                if (crAct.AccountBalance < glPosting.CreditAmount)
                {// is it really better to return an insuficient balance partial view ?
                    return RedirectToAction("GlPosts", new { message = "Insufficient funds in asset account to be credited" });
                }
            }
           
            //all this is happening after transaction is approved by checker.
            decimal amt = glPosting.CreditAmount;

            busLogic.CreditGl(crAct, amt);
            busLogic.DebitGl(drAct, amt);

            glPosting.Status = PostStatus.Approved;
            db.Entry(glPosting).State = EntityState.Modified;
            db.Entry(drAct).State = EntityState.Modified;
            db.Entry(crAct).State = EntityState.Modified;

            db.SaveChanges();

            reportLogic.CreateTransaction(drAct, amt, TransactionType.Debit);
            reportLogic.CreateTransaction(crAct, amt, TransactionType.Credit);

            return RedirectToAction("GlPosts", new { message = "Transaction Approved!" });// success post partial view or pop-up on post page ?
        }

        public ActionResult DeclineGlPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlPosting glPosting = db.GlPostings.Find(id);
            if (glPosting == null)
            {
                return HttpNotFound();
            }
            glPosting.Status = PostStatus.Declined;
            db.Entry(glPosting).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("GlPosts");
        }

        public ActionResult DeclineTellerPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TellerPosting tellerPosting = db.TellerPostings.Find(id);
            if (tellerPosting == null)
            {
                return HttpNotFound();
            }
            tellerPosting.Status = PostStatus.Declined;
            db.Entry(tellerPosting).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("TellerPosts");
        }

        // GET: PostingAuth/Details/5
        public ActionResult TellerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TellerPosting tellerPosting = db.TellerPostings.Find(id);
            if (tellerPosting == null)
            {
                return HttpNotFound();
            }
            return View(tellerPosting);
        }

        // GET: PostingAuth/Details/5
        public ActionResult GlDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlPosting glPosting = db.GlPostings.Find(id);
            if (glPosting == null)
            {
                return HttpNotFound();
            }
            return View(glPosting);
        }

        // GET: PostingAuth/Delete/5
        public ActionResult TellerDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TellerPosting tellerPosting = db.TellerPostings.Find(id);
            if (tellerPosting == null)
            {
                return HttpNotFound();
            }
            return View(tellerPosting);
        }

        // GET: PostingAuth/Delete/5
        public ActionResult GlDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlPosting glPosting = db.GlPostings.Find(id);
            if (glPosting == null)
            {
                return HttpNotFound();
            }
            return View(glPosting);
        }

        // POST: PostingAuth/Delete/5
        [HttpPost, ActionName("TellerDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTellerConfirmed(int id)
        {
            TellerPosting tellerPosting = db.TellerPostings.Find(id);
            db.TellerPostings.Remove(tellerPosting);
            db.SaveChanges();
            return RedirectToAction("TellerPosts");
        }

        // POST: PostingAuth/Delete/5
        [HttpPost, ActionName("GlDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGlConfirmed(int id)
        {
            GlPosting glPosting = db.GlPostings.Find(id);
            db.GlPostings.Remove(glPosting);
            db.SaveChanges();
            return RedirectToAction("GlPosts");
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
