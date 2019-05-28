using Library.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Filters;
using NamTanUyen.Models;
using NamTanUyen.ViewModels;

namespace NamTanUyen.Areas.cms.Controllers
{
    [CustomAuthorizer]
    public class AdminUsersController : BaseController
    {
        private JewelryContext db = new JewelryContext();

        public ActionResult Index()
        {
            return View(db.AdminUsers.ToList());
        }

        // GET: Cms/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = db.AdminUsers.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Cms/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cms/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminUser users)
        {
            if (ModelState.IsValid)
            {
                var createuser = GetCurrentUser();
                users.CreaterUserId = createuser == null ? 0 : createuser.AdminUserId;
                users.PassWord = CryptoHelper.Encrypt(users.PassWord, users.Email);
                users.IsActive = true;
                db.AdminUsers.Add(users);
                db.SaveChanges();
                return ReturnJson(result: 1);
            }

            string errors = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + Environment.NewLine;
                }
            }
            return ReturnJson(errors);
        }

        // GET: Cms/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = db.AdminUsers.Find(id);
            users.PassWord = CryptoHelper.Decrypt(users.PassWord, users.Email);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Cms/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminUser users)
        {
            if (ModelState.IsValid)
            {
                users.PassWord = CryptoHelper.Encrypt(users.PassWord, users.Email);
                var modifieduser = GetCurrentUser();

                users.ModifiedUserId = modifieduser.AdminUserId;
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return ReturnJson(result: 1);
            }
            string errors = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + Environment.NewLine;
                }
            }
            return ReturnJson(errors);

        }

        // GET: Cms/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = db.AdminUsers.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Cms/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var users = db.AdminUsers.Find(id);
            db.AdminUsers.Remove(users);
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
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //get user in db
                var password = CryptoHelper.Encrypt(model.Password, model.Email);
                var user = db.AdminUsers.SingleOrDefault(x => x.Email == model.Email && x.PassWord == password && x.IsActive == true);
                if (user == null)
                {
                    return ReturnJson("Email đăng nhập hoặc mật khẩu không đúng");
                }
                Session[ConfigHelper.SessionAdminUser] = user;
                if (model.IsRemember)
                {
                    var userviewmodel = new LoginViewModel { Email = model.Email, Password = password, IsRemember = true };
                    if (Request.Cookies[ConfigHelper.CookieAdmin] != null)
                    {
                        CookieHelper.EditCookie(ConfigHelper.CookieAdmin, JsonConvert.SerializeObject(userviewmodel), DateTime.Now.AddMonths(1));
                    }
                    else
                    {
                        CookieHelper.AddCookie(ConfigHelper.CookieAdmin, JsonConvert.SerializeObject(userviewmodel), DateTime.Now.AddMonths(1));
                    }

                }
                return ReturnJson(result: 1);
            }
            return ReturnJson(html: SystemErrorMessage, error: GetModelError(ViewData.ModelState.Values));
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Signout()
        {
            Session.Abandon();
            CookieHelper.DeleteCookie(ConfigHelper.CookieAdmin, null);
            return Redirect("/cms");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UpdatePassViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = GetCurrentUser();
                    if (user == null)
                    {
                        return ReturnJson("Session timeout, please login back");
                    }
                    else if (CryptoHelper.Encrypt(model.CurrentPass, user.Email) != user.PassWord)
                        return ReturnJson("Password incorrect");
                    else if (model.NewsPass != model.NewPassConfirm)
                        return ReturnJson("Confirm password not match");
                    var currentUserdb = db.AdminUsers.SingleOrDefault(x => x.Email == user.Email);
                    currentUserdb.PassWord = CryptoHelper.Encrypt(model.NewsPass, user.Email);
                    db.Entry(currentUserdb).State = EntityState.Modified;
                    db.SaveChanges();
                    return ReturnJson(result: 1);
                }
                string errors = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errors += error.ErrorMessage + Environment.NewLine;
                    }
                }
                return ReturnJson(errors);
            }
            catch (Exception ex)
            {
                return ReturnJson(ex.Message);
            }
        }
    }
}
