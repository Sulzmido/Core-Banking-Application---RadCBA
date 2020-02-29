using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RadCBA.Core.Models;
using RadCBA.Core.ViewModels.RoleViewModels;
using RadCBA.Logic;

namespace RadCBA.Controllers
{
    // the visual for selecting roles should look nicer
    // the coding style for role creation looks redundant , there should be a way to use a for loop

    // implement everything redundantly for now.

    //[ClaimsAuthorize("DynamicClaim", "RoleMgt")]
    public class RoleController : Controller
    {
        private AppContext db = new AppContext();
        RoleLogic roleLogic = new RoleLogic();

        public ActionResult RoleClaims()
        {
            List<Role> roles = db.Roles.ToList();
            List<RoleClaimsViewModel> model = new List<RoleClaimsViewModel>();

            foreach (var role in roles)
            {
                model.Add(new RoleClaimsViewModel { RoleId = role.ID, RoleName = role.Name, RoleClaims = role.RoleClaims });
            }

            return View(model);
        }

        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleClaimsViewModel model = new RoleClaimsViewModel { RoleId = role.ID, RoleName = role.Name, RoleClaims = role.RoleClaims };
            return View(model);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (roleLogic.isRoleNameExists(model.Name))
                    {
                        ModelState.AddModelError("", "Role name must be unique");
                        return View(model);
                    }
                    Role role = new Role { Name = model.Name };
                    db.Roles.Add(role);
                    db.SaveChanges();

                    int roleid = db.Roles.OrderByDescending(r => r.ID).FirstOrDefault().ID;

                    #region addingRoleClaimsToDatabase
                    if (model.BranchMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "BranchMgt", RoleID = roleid });
                    }

                    if (model.RoleMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "RoleMgt", RoleID = roleid });
                    }

                    if (model.CustomerMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "CustomerMgt", RoleID = roleid });
                    }

                    if (model.CustomerAccountMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "CustomerAccountMgt", RoleID = roleid });
                    }

                    if (model.FinancialReport)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "FinancialReport", RoleID = roleid });
                    }

                    if (model.GLMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "GLMgt", RoleID = roleid });
                    }

                    if (model.GLPosting)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "GLPosting", RoleID = roleid });
                    }

                    if (model.PostingAuth)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "PostingAuth", RoleID = roleid });
                    }

                    if (model.AccountConfigMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "AccountConfigMgt", RoleID = roleid });
                    }

                    if (model.RunEOD)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "RunEOD", RoleID = roleid });
                    }

                    if (model.TellerMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "TellerMgt", RoleID = roleid });
                    }

                    if (model.TellerPosting)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "TellerPosting", RoleID = roleid });
                    }

                    if (model.UserMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "UserMgt", RoleID = roleid });
                    }
                    #endregion

                    db.SaveChanges();
                    return RedirectToAction("RoleClaims");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                    return View(model);
                }                                             
            }
            ModelState.AddModelError("", "Please enter valid data");
            return View(model);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            EditRoleViewModel model = new EditRoleViewModel { RoleId = role.ID, Name = role.Name };

            #region setEditCheckBoxes
            var roleClaims = role.RoleClaims;
            foreach (var roleclaim in roleClaims)
            {
                if (roleclaim.Name.Equals("BranchMgt")){
                    model.BranchMgt = true;
                }
                if (roleclaim.Name.Equals("RoleMgt"))
                {
                    model.RoleMgt = true;
                }
                if (roleclaim.Name.Equals("CustomerMgt"))
                {
                    model.CustomerMgt = true;
                }
                if (roleclaim.Name.Equals("CustomerAccountMgt"))
                {
                    model.CustomerAccountMgt = true;
                }
                if (roleclaim.Name.Equals("FinanciaReport"))
                {
                    model.FinancialReport = true;
                }
                if (roleclaim.Name.Equals("GLMgt"))
                {
                    model.GLMgt = true;
                }
                if (roleclaim.Name.Equals("GLPosting"))
                {
                    model.GLPosting = true;
                }
                if (roleclaim.Name.Equals("PostingAuth"))
                {
                    model.PostingAuth = true;
                }
                if (roleclaim.Name.Equals("AccountConfigMgt"))
                {
                    model.AccountConfigMgt = true;
                }
                if (roleclaim.Name.Equals("RunEOD"))
                {
                    model.RunEOD = true;
                }
                if (roleclaim.Name.Equals("TellerMgt"))
                {
                    model.TellerMgt = true;
                }
                if (roleclaim.Name.Equals("TellerPosting"))
                {
                    model.TellerPosting = true;
                }
                if (roleclaim.Name.Equals("UserMgt"))
                {
                    model.UserMgt = true;
                }
            }
            #endregion

            return View(model);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        // model binding later
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Role role = db.Roles.Find(model.RoleId);
                    if (!model.Name.ToLower().Equals(role.Name.ToLower()))
                    {
                        if (roleLogic.isRoleNameExists(model.Name))
                        {
                            ModelState.AddModelError("", "Role name must be unique");
                            return View(model);
                        }
                    }
                    role.Name = model.Name;
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();

                    // first remove all role claims for role
                    db.RoleClaims.RemoveRange(role.RoleClaims);
                    // save new role claims to database for role
                    #region addingRoleClaimsToDatabase
                    if (model.BranchMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "BranchMgt", RoleID = model.RoleId });
                    }

                    if (model.RoleMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "RoleMgt", RoleID = model.RoleId });
                    }

                    if (model.CustomerMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "CustomerMgt", RoleID = model.RoleId });
                    }

                    if (model.CustomerAccountMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "CustomerAccountMgt", RoleID = model.RoleId });
                    }

                    if (model.FinancialReport)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "FinancialReport", RoleID = model.RoleId });
                    }

                    if (model.GLMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "GLMgt", RoleID = model.RoleId });
                    }

                    if (model.GLPosting)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "GLPosting", RoleID = model.RoleId });
                    }

                    if (model.PostingAuth)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "PostingAuth", RoleID = model.RoleId });
                    }

                    if (model.AccountConfigMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "AccountConfigMgt", RoleID = model.RoleId });
                    }

                    if (model.RunEOD)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "RunEOD", RoleID = model.RoleId });
                    }

                    if (model.TellerMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "TellerMgt", RoleID = model.RoleId });
                    }

                    if (model.TellerPosting)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "TellerPosting", RoleID = model.RoleId });
                    }

                    if (model.UserMgt)
                    {
                        db.RoleClaims.Add(new RoleClaim { Name = "UserMgt", RoleID = model.RoleId });
                    }
                    #endregion

                    db.SaveChanges();
                    return RedirectToAction("RoleClaims");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                    return View(model);
                }                                              
            }
            ModelState.AddModelError("", "Please enter valid data");
            return View(model);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("RoleClaims");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddRoleClaimsToDatabaseWithoutSaving(EditRoleViewModel model, int roleid)
        {
            if (model.BranchMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "BranchMgt", RoleID = roleid });
            }

            if (model.RoleMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "RoleMgt", RoleID = roleid });
            }

            if (model.CustomerMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "CustomerMgt", RoleID = roleid });
            }

            if (model.CustomerAccountMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "CustomerAccountMgt", RoleID = roleid });
            }

            if (model.FinancialReport)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "FinancialReport", RoleID = roleid });
            }

            if (model.GLMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "GLMgt", RoleID = roleid });
            }

            if (model.GLPosting)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "GLPosting", RoleID = roleid });
            }

            if (model.PostingAuth)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "PostingAuth", RoleID = roleid });
            }

            if (model.AccountConfigMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "AccountConfigMgt", RoleID = roleid });
            }

            if (model.RunEOD)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "RunEOD", RoleID = roleid });
            }

            if (model.TellerMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "TellerMgt", RoleID = roleid });
            }

            if (model.TellerPosting)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "TellerPosting", RoleID = roleid });
            }

            if (model.UserMgt)
            {
                db.RoleClaims.Add(new RoleClaim { Name = "UserMgt", RoleID = roleid });
            }
        }
    }
}
