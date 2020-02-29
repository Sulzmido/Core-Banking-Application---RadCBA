using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RadCBA.Core.Models;
using RadCBA.Core.ViewModels.UserViewModels;
using RadCBA.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RadCBA.Controllers
{
    //[ClaimsAuthorize("DynamicClaim", "UserMgt")]
    public class UserController : Controller
    {
        #region managers
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        private AppContext db = new AppContext();
        //private ApplicationDbContext userdb = new ApplicationDbContext();
        UserLogic userLogic = new UserLogic();

        // GET: User
        public ActionResult Index()
        {
            var applicationUsers = UserManager.Users.Include(a => a.Branch).Include(a => a.Role);
            //var applicationUsers = userdb.Users.Include(a => a.Branch).Include(a => a.Role);
            return View(applicationUsers.ToList());
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            //ApplicationUser applicationUser = userdb.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(db.Roles, "ID", "Name");
            ViewBag.Branches = new SelectList(db.Branches, "ID", "Name");
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            ViewBag.Roles = new SelectList(db.Roles, "ID", "Name");
            ViewBag.Branches = new SelectList(db.Branches, "ID", "Name");

            if (ModelState.IsValid)
            {
                try
                {
                    if (!userLogic.IsUniqueUsername(model.Username))
                    {
                        AddError("Username must be unique");
                        return View(model);
                    }

                    if (!userLogic.IsUniqueEmail(model.Email))
                    {
                        AddError("Email must be unique");
                        return View(model);
                    }
                    // create user
                    var user = new ApplicationUser { UserName = model.Username, Email = model.Email, BranchID = model.BranchID, RoleID = model.RoleID, FullName = model.FullName, PhoneNumber = model.PhoneNumber };
                    // autogen password
                    // save to database
                    var result = await UserManager.CreateAsync(user, "Password1234!");
                    // if result.suceded , send mail(s), for now go index
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    AddErrors(result);
                    return View(model);
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

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            //ApplicationUser applicationUser = userdb.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            EditUserViewModel model = new EditUserViewModel { Id = applicationUser.Id, FullName = applicationUser.FullName, Username = applicationUser.UserName, Email = applicationUser.Email, PhoneNumber = applicationUser.PhoneNumber};

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", applicationUser.BranchID);
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "Name", applicationUser.RoleID);
            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", model.BranchID);
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "Name", model.RoleID);

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser applicationUser = await UserManager.FindByIdAsync(model.Id);
                    //ApplicationUser applicationUser = userdb.Users.Find(model.Id);
                    applicationUser.FullName = model.FullName;
                    applicationUser.Email = model.Email;
                    applicationUser.UserName = model.Username;
                    applicationUser.PhoneNumber = model.PhoneNumber;
                    applicationUser.RoleID = model.RoleID;
                    applicationUser.BranchID = model.BranchID;

                    await UserManager.UpdateAsync(applicationUser);
                    //userdb.Entry(applicationUser).State = EntityState.Modified;
                    //userdb.SaveChanges();
                    return RedirectToAction("Index");
                    //ModelState.AddModelError("", "");
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

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            //ApplicationUser applicationUser = userdb.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            //ApplicationUser applicationUser = userdb.Users.Find(id);

            await UserManager.DeleteAsync(applicationUser);
            //userdb.Users.Remove(applicationUser);
            //userdb.SaveChanges();
            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddError(string error)
        {
            ModelState.AddModelError("", error);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                //userdb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
